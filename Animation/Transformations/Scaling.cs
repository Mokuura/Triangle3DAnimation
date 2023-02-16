using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class Scaling : Transformation
    {
        
        public float ScaleFactor { get; set; }

        public Scaling(float scaleFactor, TimeSingle start, TimeSingle end) : base(start, end)
        {
            ScaleFactor = scaleFactor;
        }

        public override List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];

            // compute barycenter
            Vec3 barycenter = lastFrame.VerticesPositions.Aggregate(new Vec3(0, 0, 0), (barycenter, next) => barycenter + (next / lastFrame.VerticesPositions.Count));

            List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => ((vertexPosition - barycenter) * ScaleFactor) + barycenter);
            return new List<AnimationFrame> { new AnimationFrame(newPositions, time) };
        }
    }
}
