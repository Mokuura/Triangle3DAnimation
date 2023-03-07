using static GBX.NET.Engines.Plug.CPlugCrystal;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjModelBuilder
    {
        public List<ObjVertex> Vertices { get; set; }

        public List<ObjFace> Faces { get; set; }

        public List<ObjMaterial> Materials { get; set; }

        public ObjMaterial? CurrentMaterial { get; set; }

        public int VertexCount;

        public int TextureVertexCount;

        public ObjModelBuilder()
        {
            Vertices = new List<ObjVertex>();
            Materials = new List<ObjMaterial>();
            Faces = new List<ObjFace>();
            VertexCount = 0;
            TextureVertexCount = 0;
        }

        public void AddVertex(float x, float y, float z)
        {
            VertexCount++;
            Vertices.Add(new ObjVertex(x, y, z, VertexCount));
        }

        public void AddFace(List<ObjFaceVertex> faceVertices) 
        {
            if (faceVertices.Count == 3)
            {
                Faces.Add(new ObjFace(faceVertices[0], faceVertices[1], faceVertices[2], CurrentMaterial));
            } else if (faceVertices.Count > 3) { }
            {
                for (int i = 1; i < faceVertices.Count - 1; i++)
                {
                    // divide faces with more than 3 vertex into triangles
                    Faces.Add(new ObjFace(faceVertices[0], faceVertices[i], faceVertices[i + 1], CurrentMaterial));
                }
            }
        }

        public ObjVertex GetVertex(int index)
        {
            foreach (ObjVertex objVertex in Vertices)
            {
                if (objVertex.Index == index) return objVertex;
            }
            // not found
            throw new ArgumentException("error : vertex at index " + index + " not found");
        }

        public void setCurrentMaterial(String materialName)
        {
            foreach (ObjMaterial objMaterial in Materials)
            {
                if (objMaterial.Name.Equals(materialName))
                {
                    CurrentMaterial = objMaterial;
                }
            }
        }

        public ObjModel Build()
        {
            return new ObjModel(Vertices, Faces, Materials);
        }
    }
}
