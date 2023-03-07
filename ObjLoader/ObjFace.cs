using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjFace
    {
        public ObjFaceVertex V1 { get; set; }
        public ObjFaceVertex V2 { get; set; }
        public ObjFaceVertex V3 { get; set; }
        public ObjMaterial? Material { get; set; }

        public ObjFace(ObjFaceVertex x, ObjFaceVertex y, ObjFaceVertex z, ObjMaterial? material)
        {
            V1 = x;
            V2 = y;
            V3 = z;
            Material = material;
        }

        public override String ToString()
        {
            return "face with material : " + (Material == null ? " " : Material.Name);
        }

        public Int3 getTriangleVerticesInInt3()
        {
            // obj format uses 1-based indexing but media tracker triangles uses 0-based indexing
            return new Int3(V1.Vertex.Index - 1, V2.Vertex.Index - 1, V3.Vertex.Index - 1);
        }
    }
}
