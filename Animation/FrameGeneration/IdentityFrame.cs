using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    /*
     * Generate a frame identical to the previous frame
     */
    public class IdentityFrame : FrameGenerator
    {
        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];
            return new List<AnimationFrame> { new AnimationFrame(lastFrame.VerticesPositions, time) };
        }
    }
}
