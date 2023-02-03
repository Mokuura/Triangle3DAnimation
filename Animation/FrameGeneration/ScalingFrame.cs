using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public class ScalingFrame : FrameGenerator
    {
        
        public float ScaleFactor { get; set; }

        public ScalingFrame(float scaleFactor)
        {
            ScaleFactor = scaleFactor;
        }

        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];

            // compute barycenter
            Vec3 barycenter = lastFrame.VerticesPositions.Aggregate(new Vec3(0, 0, 0), (barycenter, next) => barycenter + (next / lastFrame.VerticesPositions.Count));

            List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => ((vertexPosition - barycenter) * ScaleFactor) + barycenter);
            return new List<AnimationFrame> { new AnimationFrame(newPositions, time) };
        }
    }
}
