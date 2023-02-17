using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation.Transformations
{
    public class InitModel : InitTransformation
    {
        public ObjModel Model { get; set; }
        public InitModel(ObjModel model, TimeSingle start, TimeSingle end) : base(start, end)
        {
            Model = model;
        }
    }
}
