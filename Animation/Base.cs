using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.Animation
{
    public interface Base
    {
        public List<AnimationFrame> GenerateFrames(SingleBlockTriangleAnimation animation);
    }
}
