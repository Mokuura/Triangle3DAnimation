using GBX.NET;
using GBX.NET.Engines.Game;
using Triangle3DAnimation.Animation;
using Triangle3DAnimation.Animation.FrameGeneration;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation
{
    public class Tests
    {
        public static void RunTests(string[] args)
        {
            ObjModel model = ObjLoader.ObjLoader.ParseObj("C:\\Users\\colin\\OneDrive\\Documents\\GBX.NET", "Lowpoly_tree_sample");
            ObjAnimation animationObj = ObjLoader.ObjLoader.ParseObjAnimation("C:\\Users\\colin\\OneDrive\\Documents\\GBX.NET\\AnimationBlender\\blender\\exportOBJ\\wolfNoTextures", "Wolf-Blender-2.82aNoTextures");

            // exemple manipulation materials
            foreach (var obj in animationObj.Frames.Values) 
            {
                foreach (var mat in obj.Materials)
                {
                    mat.DiffuseR *= 10;
                    mat.DiffuseG *= 10;
                    mat.DiffuseB *= 10;
                }
            }

            SingleBlockTriangleAnimation animation = new SingleBlockTriangleAnimation();
            animation.AddTransformation(new BaseAnimation(animationObj, TmEssentials.TimeSingle.FromSeconds(0.1f)), TmEssentials.TimeSingle.FromSeconds(0));
            //animation.GenerateFrames(new BaseFrame(model), TmEssentials.TimeSingle.FromSeconds(0));
            //animation.GenerateFrames(new Identity(), TmEssentials.TimeSingle.FromSeconds(2));
            //animation.GenerateFrames(new Translation(new Vec3(100, 100, 100)), TmEssentials.TimeSingle.FromSeconds(2.5f));
            //animation.GenerateFrames(new Translation(new Vec3(-100, -100, -100)), TmEssentials.TimeSingle.FromSeconds(3f));
            //animation.GenerateFrames(new Translation(new Vec3(100, 300, -100)), TmEssentials.TimeSingle.FromSeconds(3.5f));
            //animation.GenerateFrames(new Translation(new Vec3(-100, -300, 100)), TmEssentials.TimeSingle.FromSeconds(3.8f));
            //animation.GenerateFrames(new Scaling(2f), TmEssentials.TimeSingle.FromSeconds(4f));
            //animation.GenerateFrames(new Scaling(0.5f), TmEssentials.TimeSingle.FromSeconds(4.5f));
            //animation.GenerateFrames(new Rotation(MathF.PI, MathF.PI, 0, 8), TmEssentials.TimeSingle.FromSeconds(5f));
            //animation.GenerateFrames(new Rotation(-MathF.PI, -MathF.PI, 0, 8), TmEssentials.TimeSingle.FromSeconds(5.5f));
            //animation.GenerateFrames(new Rotation(MathF.PI, 0, 0, 8), TmEssentials.TimeSingle.FromSeconds(6f));
            //animation.GenerateFrames(new Rotation(0, MathF.PI, 0, 8), TmEssentials.TimeSingle.FromSeconds(6.5f));
            //animation.GenerateFrames(new Rotation(MathF.PI, MathF.PI, MathF.PI, 8), TmEssentials.TimeSingle.FromSeconds(5.5f));
            //animation.GenerateFrames(new Rotation(MathF.PI, MathF.PI, 0, 8), TmEssentials.TimeSingle.FromSeconds(5.5f));
            //animation.GenerateFrames(new Rotation(MathF.PI, MathF.PI, MathF.PI, 8), TmEssentials.TimeSingle.FromSeconds(6f));

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
