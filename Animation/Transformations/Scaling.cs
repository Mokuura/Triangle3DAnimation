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

        public override List<Transformation> SplitOn(List<TimeSingle> points)
        {
            points.Sort();
            List<Transformation> result = new List<Transformation>();
            TimeSingle totalDuration = this.End - this.Start;
            TimeSingle startOfSplit = this.Start;
            float scale = 1;
            foreach (TimeSingle point in points)
            {
                TimeSingle durationOfSplit = point - startOfSplit;
                float normalizedTime = durationOfSplit / (End - startOfSplit);
                float scaleAtPoint = scale + (ScaleFactor - scale) * normalizedTime;                
                float newScaleFactor = scaleAtPoint / scale;
                scale = scaleAtPoint;
                result.Add(new Scaling(newScaleFactor, startOfSplit, point));
                startOfSplit = point;
            }

            return result;
        }

        public override List<Vec3> apply(List<Vec3> oldPositions)
        {
            // compute barycenter
            Vec3 barycenter = oldPositions.Aggregate(new Vec3(0, 0, 0), (barycenter, next) => barycenter + (next / oldPositions.Count));

            return oldPositions.ConvertAll(vertexPosition => ((vertexPosition - barycenter) * ScaleFactor) + barycenter);
        }
    }
}
