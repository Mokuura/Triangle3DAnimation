using GBX.NET;
using GBX.NET.Engines.Game;
using Triangle3DAnimation.Animation;
using Triangle3DAnimation.Animation.Transformations;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation
{
    public class Tests
    {
        public static void RunTests(string[] args)
        {
            ObjModel objModel = ObjLoader.ObjLoader.ParseObj("C:\\Users\\colin\\OneDrive\\Documents\\GBX.NET", "Lowpoly_tree_sample");
            ObjAnimation animationObj = ObjLoader.ObjLoader.ParseObjAnimation("C:\\Users\\colin\\OneDrive\\Documents\\GBX.NET\\AnimationBlender\\blender\\exportOBJ\\wolfNoTextures", "Wolf-Blender-2.82aNoTextures");

            SingleBlockTriangleAnimation animation = new SingleBlockTriangleAnimation(new BaseAnimation(animationObj, TmEssentials.TimeSingle.FromSeconds(0.2f)));
            animation.AddTransformation(new Scaling(30, TmEssentials.TimeSingle.FromSeconds(0), TmEssentials.TimeSingle.FromSeconds(0.0001f)));
            animation.AddTransformation(new Rotation(MathF.PI / 16, 0, 0, 1, TmEssentials.TimeSingle.FromSeconds(0), TmEssentials.TimeSingle.FromSeconds(0.0001f)));
            animation.AddTransformation(new Translation(new Vec3(50, 0, 200), TmEssentials.TimeSingle.FromSeconds(0), TmEssentials.TimeSingle.FromSeconds(6.40f)));
            //animation.AddTransformation(new Translation(new Vec3(0, 50, 0), TmEssentials.TimeSingle.FromSeconds(0.5f), TmEssentials.TimeSingle.FromSeconds(2)));
            //animation.AddTransformation(new Rotation(0, 0, 2 * MathF.PI, 32, TmEssentials.TimeSingle.FromSeconds(0), TmEssentials.TimeSingle.FromSeconds(3.20f)));
            //animation.AddTransformation(new Scaling(3, TmEssentials.TimeSingle.FromSeconds(1.5f), TmEssentials.TimeSingle.FromSeconds(3)));
            //animation.AddTransformation(new Translation(new Vec3(50, 0, 0), TmEssentials.TimeSingle.FromSeconds(5), TmEssentials.TimeSingle.FromSeconds(6)));
            animation.GenerateFrames();

            var map = GameBox.ParseNode<CGameCtnChallenge>("C:\\Users\\colin\\OneDrive\\Documents\\Trackmania\\Maps\\My Maps\\TestGBXExplorer.Map.Gbx");
            //var map = GameBox.ParseNode<CGameCtnChallenge>("C:\\Users\\colin\\OneDrive\\Documents\\Maniaplanet\\Maps\\My Maps\\TestTriangle3D.Map.Gbx");

            var trackMediaTracker = map.ClipGroupInGame.Clips[0].Clip.Tracks[0];
            trackMediaTracker.Blocks.Clear();
            //trackMediaTracker.Blocks.Add(animation.ToTriangle3DMediaTrackerBlock(new Vec3(950f, 17.9f, 920f)));
            trackMediaTracker.Blocks.Add(animation.ToTriangle3DMediaTrackerBlock(new Vec3(500, 10, 500)));
            map.Save();
        }
    }
}
