using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public abstract class Transformation
    {
        public TimeSingle Start { get; set; }
        public TimeSingle End { get; set; }

        public Transformation(TimeSingle start, TimeSingle end) 
        {
            if (start > end)
            {
                throw new ArgumentException("error: start time should be lower than end time");
            }
            Start = start;
            End = end;
        }

        public List<Transformation> GetWithoutPartialOverlap(List<Transformation> transformations)
        {
            List<TimeSingle> overlapingPoints = new List<TimeSingle>();
            foreach (Transformation otherTransformation in transformations)
            {
                if (otherTransformation.Start > Start && otherTransformation.Start < End)
                {
                    overlapingPoints.Add(otherTransformation.Start);
                }
                if (otherTransformation.End > Start && otherTransformation.End < End)
                {
                    overlapingPoints.Add(otherTransformation.End);
                }
            }

            overlapingPoints.Add(End);
            
            // split on overlaping points
            return SplitOn(overlapingPoints.Distinct().ToList());            
        }

        public abstract List<Transformation> SplitOn(List<TimeSingle> points);

        public abstract List<Vec3> apply(List<Vec3> oldPositions);
    }
}
