using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class InitAnimation : InitTransformation
    {
        public ObjAnimation Animation { get; set; }
        public InitAnimation(ObjAnimation animation, TimeSingle start, TimeSingle end) : base(start, end)
        {
            Animation = animation;
        }

        public List<InitModel> getAllInitModel()
        {
            TimeSingle totalDuration = this.End - this.Start;
            TimeSingle stepDuration = totalDuration / Animation.Frames.Count;
            List<InitModel> result = new List<InitModel>();
            TimeSingle startOfStep = this.Start;

            
            for (int i = 0; i < Animation.Frames.Count; i++)
            {
                result.Add(new InitModel(Animation.Frames[i + 1], startOfStep, startOfStep + stepDuration));
                startOfStep += stepDuration;
            }

            return result;
        }
    }
}
