using GBX.NET;
using GBX.NET.Engines.Game;
using TmEssentials;

namespace Triangle3DAnimation.Animation
{
    public class AnimationFrame
    {
        public List<Vec3> VerticesPositions { get; set; }

        public TimeSingle Time { get; set; }

        public AnimationFrame(List<Vec3> verticesPositions, TimeSingle time)
        {
            VerticesPositions = verticesPositions;
            Time = time;
        }

        public CGameCtnMediaBlockTriangles.Key ToMediaTrackerKey(CGameCtnMediaBlockTriangles baseNode, Vec3 offset)
        {
            CGameCtnMediaBlockTriangles.Key key = new CGameCtnMediaBlockTriangles.Key(baseNode);
            key.Time = Time;
            key.Positions = VerticesPositions.ConvertAll(position => position + offset).ToArray();
            return key;
        }
    }
}
