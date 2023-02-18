using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class CustomTransformation : Transformation
    {
        public List<Vec3> Translations { get; set; }

        public CustomTransformation(List<Vec3> translations, TimeSingle start, TimeSingle end) : base(start, end)
        {
            Translations = translations;
        }

        public override List<Vec3> apply(List<Vec3> oldPositions)
        {
            List<Vec3> newPositions = new List<Vec3>();
            for (int i = 0; i < Translations.Count; i++) 
            {
                newPositions.Add(oldPositions[i] + Translations[i]);
            }

            return newPositions;
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

                List<Vec3> newTranslations = new List<Vec3>();
                for (int i = 0; i < Translations.Count; i++)
                {
                    Vec3 newTranslationVector = Translations[i] / (totalDuration / durationOfSplit);
                    newTranslations.Add(newTranslationVector);
                }
                
                result.Add(new CustomTransformation(newTranslations, startOfSplit, point));
                startOfSplit = point;
            }

            return result;
        }
    }
}
