using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public class RotationFrame : FrameGenerator
    {

        public float Pitch { get; set; }
        public float Yaw { get; set; }
        public float Roll { get; set; }

        public RotationFrame(float pitch, float yaw, float roll)
        {
            Pitch = pitch;
            Yaw = yaw;
            Roll = roll;
        }

        public AnimationFrame GenerateFrame(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];

            // compute barycenter
            Vec3 barycenter = lastFrame.VerticesPositions.Aggregate(new Vec3(0, 0, 0), (barycenter, next) => barycenter + (next / lastFrame.VerticesPositions.Count));

            List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => (Rotate(vertexPosition - barycenter)) + barycenter);
            return new AnimationFrame(newPositions, time);
        }

        public Vec3 Rotate(Vec3 vertex)
        {
            var cosa = MathF.Cos(Yaw);
            var sina = MathF.Sin(Yaw);

            var cosb = MathF.Cos(Pitch);
            var sinb = MathF.Sin(Pitch);

            var cosc = MathF.Cos(Roll);
            var sinc = MathF.Sin(Roll);

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
