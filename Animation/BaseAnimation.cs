using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.Transformations;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation
{
    public class BaseAnimation : Base
    {
        public ObjAnimation Animation { get; set; }

        public TimeSingle FrameDuration { get; set; }

        public BaseAnimation(ObjAnimation animation, TimeSingle frameDuration) : base()
        {
            FrameDuration = frameDuration;
            Animation = animation;
        }

        public BaseAnimation(ObjAnimation animation, TimeSingle frameDuration, float shadingIntensity) : base(shadingIntensity)
        {
            FrameDuration = frameDuration;
            Animation = animation;
        }

        public override void InitAnimation(SingleBlockTriangleAnimation animation)
        {
            BaseModel.InitAnimation(Animation.Frames[1], animation, ShadingIntensity);    
        }

        public override AnimationFrame GetFirstFrame(TimeSingle time)
        {
            return new AnimationFrame(Animation.Frames[1].Vertices.ConvertAll(vertex => vertex.ToVec3()), time);
        }

        public List<CustomTransformation> GenerateAllTransformations(TimeSingle start, TimeSingle? end)
        {
            if (end != null)
            {
                return GenerateAllTransformationsRepeated(start, (TimeSingle) end);
            }
            
            List<CustomTransformation> result = new List<CustomTransformation>();
            for (int i = 2; Animation.Frames.TryGetValue(i, out _); i++)
            {
                List<Vec3> oldPositions = Animation.Frames[i - 1].Vertices.ConvertAll(vertex => vertex.ToVec3());
                List<Vec3> newPositions = Animation.Frames[i].Vertices.ConvertAll(vertex => vertex.ToVec3());
                if (oldPositions.Count != newPositions.Count)
                {
                    throw new Exception("error : ObjAnimation contains frames with different vertex count");
                }
                List<Vec3> offset = new List<Vec3>();
                for (int j = 0; j < oldPositions.Count; j++)
                {
                    offset.Add(newPositions[j] - oldPositions[j]);
                }

                result.Add(new CustomTransformation(offset, start + (i - 2) * FrameDuration, start + (i - 1) * FrameDuration));
            }

            return result;
        }

        public List<CustomTransformation> GenerateAllTransformationsRepeated(TimeSingle start, TimeSingle end)
        {
            List<CustomTransformation> result = new List<CustomTransformation>();
            TimeSingle current = start;
            int i = 2;
            while(current < end)
            {
                if (Animation.Frames.TryGetValue(i, out _))
                {
                    List<Vec3> oldPositions = Animation.Frames[i - 1].Vertices.ConvertAll(vertex => vertex.ToVec3());
                    List<Vec3> newPositions = Animation.Frames[i].Vertices.ConvertAll(vertex => vertex.ToVec3());
                    if (oldPositions.Count != newPositions.Count)
                    {
                        throw new Exception("error : ObjAnimation contains frames with different vertex count");
                    }
                    List<Vec3> offset = new List<Vec3>();
                    for (int j = 0; j < oldPositions.Count; j++)
                    {
                        offset.Add(newPositions[j] - oldPositions[j]);
                    }

                    result.Add(new CustomTransformation(offset, current, current + FrameDuration));
                    current += FrameDuration;
                    i++;
                } else
                {
                    i = 2;
                }   
            }

            return result;
        }
    }
}
