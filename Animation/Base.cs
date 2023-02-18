using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation
{
    public interface Base
    {
        public void InitAnimation(SingleBlockTriangleAnimation animation);

        public AnimationFrame GetFirstFrame(TimeSingle time);
    }
}
