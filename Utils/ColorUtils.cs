using GBX.NET;

namespace Triangle3DAnimation.Utils
{
    public class ColorUtils
    {
        public const float SHADING_DEFAULT_MAX = 0.3f;

        public static Vec4 AddRandomShades(Vec4 color)
        {
            Random random = new Random();
            float randomDhading = random.NextSingle() * SHADING_DEFAULT_MAX;
            float r = color.X * (1f - randomDhading);
            float g = color.Y * (1f - randomDhading);
            float b = color.Z * (1f - randomDhading);
            return new Vec4(r, g, b, color.W);
        }
    }
}
