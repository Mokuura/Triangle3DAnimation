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
        public List<ObjTextureVertex> TextureVertices { get; set; }
        public List<ObjFace> Faces { get; set; }

        public ObjModel(List<ObjVertex> vertices, List<ObjTextureVertex> textureVertices, List<ObjFace> faces) 
        {
            Vertices = vertices;
            TextureVertices = textureVertices;
            Faces = faces;
        }

        public override String ToString()
        {
            String toReturn = "ObjModel :\n";
            toReturn += "vertices :\n";
            foreach (ObjVertex vertex in Vertices)
            {
                toReturn += vertex.ToString() + "\n";
            }
            toReturn += "textures vertices :\n";
            foreach (ObjTextureVertex textureVertex in TextureVertices)
            {
                toReturn += textureVertex.ToString() + "\n";
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
