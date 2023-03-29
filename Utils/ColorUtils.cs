using GBX.NET;

namespace Triangle3DAnimation.Utils
{
    public class ColorUtils
    {
        public static Vec4 AddRandomShades(Vec4 color, float shadingIntensity)
        {
            if (shadingIntensity > 1)
            {
                shadingIntensity = 1;
            }
            if (shadingIntensity < 0)
            {
                shadingIntensity = 0;
            }

            Random random = new Random();
            float randomShading = random.NextSingle() * SHADING_DEFAULT_MAX;
            float r = color.X * (1f - randomShading);
            float g = color.Y * (1f - randomShading);
            float b = color.Z * (1f - randomShading);
            return new Vec4(r, g, b, color.W);
        }
    }
}
