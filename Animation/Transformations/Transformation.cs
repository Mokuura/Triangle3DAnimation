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
            if (start > End)
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
                if (otherTransformation.End <= Start || otherTransformation.Start >= End)
                {
                    // no overlap
                    continue;
                }

                if (otherTransformation.End > Start && otherTransformation.Start < Start)
                {
                    // only end overlap
                    overlapingPoints.Add(otherTransformation.End);
                }

                if (otherTransformation.Start < End && otherTransformation.End > End)
                {
                    // only start overlap
                    overlapingPoints.Add(otherTransformation.Start);
                }

                if (otherTransformation.Start > Start && otherTransformation.End < End)
                {
                    // start and end overlap
                    overlapingPoints.Add(otherTransformation.Start);
                    overlapingPoints.Add(otherTransformation.End);
                }
            }
            
            // split on overlaping points
            return SplitOn(overlapingPoints);
        }

        public abstract List<Transformation> SplitOn(List<TimeSingle> points);

        public abstract List<AnimationFrame> GenerateFrames(TriangleAnimation current, TmEssentials.TimeSingle timeEnd);
    }
}
