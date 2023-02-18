using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class Translation : Transformation
    {
        public Vec3 TranslationVector { get; set; } 

        public Translation(Vec3 translationVector, TimeSingle start, TimeSingle end) : base(start, end) 
        {
            TranslationVector = translationVector;
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
                Vec3 newTranslationVector = TranslationVector / (totalDuration / durationOfSplit);
                result.Add(new Translation(newTranslationVector, startOfSplit, point));
                startOfSplit = point;
            }

            return result;
        }

        public override List<Vec3> apply(List<Vec3> oldPositions)
        {
            return oldPositions.ConvertAll(vertexPosition => vertexPosition + TranslationVector);
        }
    }
}
