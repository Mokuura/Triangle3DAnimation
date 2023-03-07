using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjFaceVertex
    {
        public ObjVertex Vertex { get; set; }

        public ObjFaceVertex(ObjVertex vertex)
        { 
            Vertex = vertex;
        }     
    }
}
