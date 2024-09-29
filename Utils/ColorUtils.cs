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
            float randomShading = (1f - (random.NextSingle() * 2)) * shadingIntensity;
            float r = Math.Min(1, color.X * (1f - randomShading));
            float g = Math.Min(1, color.Y * (1f - randomShading));
            float b = Math.Min(1, color.Z * (1f - randomShading));
            return new Vec4(r, g, b, color.W);
        }
    }
}
