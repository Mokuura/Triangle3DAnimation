using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public interface FrameGenerator
    {
        AnimationFrame GenerateFrame(TriangleAnimation current, TmEssentials.TimeSingle time);
    }
}
