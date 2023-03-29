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
            float randomDhading = random.NextSingle() * shadingIntensity;
            float r = color.X * (1f - randomDhading);
            float g = color.Y * (1f - randomDhading);
            float b = color.Z * (1f - randomDhading);
            return new Vec4(r, g, b, color.W);
        }
    }
}
