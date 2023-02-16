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
                double prop = 0; // TODO compute with duration of split and total duration
                Vec3 newTranslationVector = TranslationVector / prop;
                result.Add(new Translation(newTranslationVector, startOfSplit, point));
                startOfSplit = point;
            }
        }

        public override List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];
            List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => vertexPosition + TranslationVector);
            return new List<AnimationFrame> { new AnimationFrame(newPositions, time) };
        }
    }
}
