using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.Utils
{
    public class ColorUtils
    {
        public const float SHADING_DEFAULT_MAX = 0.3f;

        public static Vec4 AddShades(Vec4 color)
        {
            Random random = new Random();
            float shading = random.NextSingle() * SHADING_DEFAULT_MAX;
            float r = color.X * (1f - shading);
            float g = color.Y * (1f - shading);
            float b = color.Z * (1f - shading);
            if (r < 0f) r = 0f; if (g < 0f) g = 0f; if (b < 0f) b = 0f;
            return new Vec4(r, g, b, color.W);
        }
    }
}
