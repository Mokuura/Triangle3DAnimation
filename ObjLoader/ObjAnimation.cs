using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjAnimation
    {
        public Dictionary<int, ObjModel> Frames { get; set; } 

        public ObjAnimation() 
        {
            Frames = new Dictionary<int, ObjModel>();
        }
    }
}
