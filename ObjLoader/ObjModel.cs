using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjModel
    {
        public List<ObjVertex> Vertices { get; set; }
        public List<ObjFace> Faces { get; set; }
        public List<ObjMaterial> Materials { get; set; }

        public ObjModel(List<ObjVertex> vertices, List<ObjFace> faces, List<ObjMaterial> materials) 
        {
            Vertices = vertices;
            Faces = faces;
            Materials = materials;
        }

        public override String ToString()
        {
            String toReturn = "ObjModel :\n";
            toReturn += "vertices :\n";
            foreach (ObjVertex vertex in Vertices)
            {
                toReturn += vertex.ToString() + "\n";
            }
            toReturn += "faces :\n";
            foreach (ObjFace face in Faces)
            {
                toReturn += face.ToString() + "\n";
            }
            return toReturn;
        }
    }
}
