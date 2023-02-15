using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public class Translation : FrameGenerator
    {

        public Vec3 TranslationVector { get; set; } 

        public Translation(Vec3 translationVector) 
        {
            TranslationVector = translationVector;
        }

        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];
            List<Vec3> newPositions = lastFrame.VerticesPositions.ConvertAll(vertexPosition => vertexPosition + TranslationVector);
            return new List<AnimationFrame> { new AnimationFrame(newPositions, time) };
        }
    }
}
