using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;

namespace Triangle3DAnimation.Animation
{
    public abstract class Base
    {
        public const float SHADING_DEFAULT_INTENSITY = 0.3f;
        public float ShadingIntensity { get; set; }

        public Base(float shadinIntensity)
        {
            ShadingIntensity = shadinIntensity;
        }

        public Base()
        {
            ShadingIntensity = SHADING_DEFAULT_INTENSITY;
        }

        abstract public void InitAnimation(SingleBlockTriangleAnimation animation);

        abstract public AnimationFrame GetFirstFrame(TimeSingle time);
    }
}
