using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    public class BaseAnimation : FrameGenerator
    {
        public ObjAnimation Animation { get; set; }

        public TimeSingle FrameDuration { get; set; }

        public BaseAnimation(ObjAnimation animation, TimeSingle frameDuration)
        {
            Animation = animation;
            FrameDuration = frameDuration;
        }

        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            List<AnimationFrame> frames = new List<AnimationFrame>();
            current.GenerateFrames(new BaseFrame(Animation.Frames[1]), time);
            for (int i = 2; Animation.Frames.TryGetValue(i, out _); i++)
            {
                List<Vec3> newPositions = Animation.Frames[i].Vertices.ConvertAll(vertex => vertex.ToVec3());
                AnimationFrame lastFrame = current.AnimationFrames[current.AnimationFrames.Count - 1];
                if (newPositions.Count != lastFrame.VerticesPositions.Count) 
                {
                    // TODO 
                    throw new NotSupportedException();
                }

                frames.Add(new AnimationFrame(newPositions, time + ((i - 1) * FrameDuration)));
            }
            return frames;
        }
    }
}
