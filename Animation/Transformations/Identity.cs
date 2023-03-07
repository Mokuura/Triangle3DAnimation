using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class Identity : Transformation
    {
        public Identity(TimeSingle start, TimeSingle end) : base(start, end)
        {
        }

        public override List<Vec3> apply(List<Vec3> oldPositions)
        {
            return oldPositions;
        }

        public override List<Transformation> SplitOn(List<TimeSingle> points)
        {
            points.Sort();
            List<Transformation> result = new List<Transformation>();
            TimeSingle totalDuration = this.End - this.Start;
            TimeSingle startOfSplit = this.Start;
            foreach (TimeSingle point in points)
            {
                TimeSingle durationOfSplit = point - startOfSplit;
                result.Add(new Identity(startOfSplit, point));
                startOfSplit = point;
            }

            return result;
        }
    }
}
