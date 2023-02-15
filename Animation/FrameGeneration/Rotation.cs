using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public class Rotation : FrameGenerator
    {

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Roll { get; set; }
        public int NbSteps { get; set; }

        public Rotation(float pitch, float yaw, float roll, int nbSteps)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
            NbSteps = nbSteps;
        }

        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];
            TimeSingle lastFrameTime = lastFrame.Time;
            TimeSingle stepTime = (time - lastFrameTime) / NbSteps;

            List<AnimationFrame> frames = new List<AnimationFrame>();
            for (int i = 0; i < NbSteps; i++)
            {
                // compute barycenter
                Vec3 barycenter = lastFrame.VerticesPositions.Aggregate(new Vec3(0, 0, 0), (barycenter, next) => barycenter + (next / lastFrame.VerticesPositions.Count));

                List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => (Rotate(vertexPosition - barycenter)) + barycenter);
                lastFrame = new AnimationFrame(newPositions, lastFrameTime + (stepTime * (i + 1)));
                frames.Add(lastFrame);
            }
            return frames;
        }

        public Vec3 Rotate(Vec3 vertex)
        {
            var cosa = MathF.Cos(Yaw / NbSteps);
            var sina = MathF.Sin(Yaw / NbSteps);

            var cosb = MathF.Cos(Pitch / NbSteps);
            var sinb = MathF.Sin(Pitch / NbSteps );

            var cosc = MathF.Cos(Roll / NbSteps);
            var sinc = MathF.Sin(Roll / NbSteps);

            var Axx = cosa * cosb;
            var Axy = cosa * sinb * sinc - sina * cosc;
            var Axz = cosa * sinb * cosc + sina * sinc;

            var Ayx = sina * cosb;
            var Ayy = sina * sinb * sinc + cosa * cosc;
            var Ayz = sina * sinb * cosc - cosa * sinc;

            var Azx = -sinb;
            var Azy = cosb * sinc;
            var Azz = cosb * cosc;

            return new Vec3(Axx * vertex.X + Axy * vertex.Y + Axz * vertex.Z, Ayx * vertex.X + Ayy * vertex.Y + Ayz * vertex.Z, Azx * vertex.X + Azy * vertex.Y + Azz * vertex.Z);
        } 
    }
}
