using System.Globalization;
using Triangle3DAnimation.MtlLoader;

namespace Triangle3DAnimation.ObjLoader
{
    public class ObjLoader
    {
        public static ObjModel ParseObj(String filePath)
        {
            ObjModelBuilder objModelBuilder = new ObjModelBuilder();
            String[] fileLines = File.ReadAllLines(filePath);
            foreach (String line in fileLines)
            {
                ParseLine(line, objModelBuilder);
            }

            return objModelBuilder.Build();
        }

        private static void ParseLine(String line, ObjModelBuilder objModelBuilder)
        {
            String[] separators = new string[] { " ", "\t" };
            String[] tokens = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 0) 
            {
                return; // empty line
            }

            // check keyword
            switch (tokens[0])
            {
                case "v":
                    ParseVertex(tokens, objModelBuilder);
                    break;
                case "vt":
                    ParseTextureVertex(tokens, objModelBuilder);
                    break;
                case "f":
                    ParseFace(tokens, objModelBuilder);
                    break;
                case "usemtl":
                    if (tokens.Length < 2) { return; }
                    objModelBuilder.setCurrentMaterial(tokens[1]);
                    break;
                case "mtllib":
                    foreach (String materialFileName in tokens.Skip(1).Take(tokens.Length - 1)) 
                    { 
                        ParseMtl(materialFileName, objModelBuilder);    
                    }
                    break;
                default:
                    // other types of data are useless for 3D Triangles in Trackmania
                    break;
            }
        }

        private static void ParseVertex(String[] tokens, ObjModelBuilder objModelBuilder)
        {
            if (tokens.Length < 4) 
            {
                throw new ArgumentException("error : vertex must have x, y and z value");
            }

            float x = float.Parse(tokens[1], CultureInfo.InvariantCulture);
            float y = float.Parse(tokens[2], CultureInfo.InvariantCulture);
            float z = float.Parse(tokens[3], CultureInfo.InvariantCulture);
            // w is ignored

            objModelBuilder.AddVertex(x, y, z);
        }

        private static void ParseTextureVertex(String[] tokens, ObjModelBuilder objModelBuilder)
        {
            if (tokens.Length < 3)
            {
                throw new ArgumentException("error : texture vertex must have u and v value");
            }
            float u = float.Parse(tokens[1], CultureInfo.InvariantCulture);
            float v = float.Parse(tokens[2], CultureInfo.InvariantCulture);
            // w is ignored

            objModelBuilder.AddTextureVertex(u, v);
        }

        private static void ParseFace(String[] tokens, ObjModelBuilder objModelBuilder)
        {
            if (tokens.Length < 4) 
            {
                throw new ArgumentException("error : face must have at least 3 vertices"); 
            } 

            List<ObjFaceVertex> facesVertices = new List<ObjFaceVertex>();
            foreach (String faceVertexDefinition in tokens.Skip(1).Take(tokens.Length - 1))
            {
                facesVertices.Add(ParseFaceVertex(faceVertexDefinition, objModelBuilder));
            }
            objModelBuilder.AddFace(facesVertices);
        }

        private static ObjFaceVertex ParseFaceVertex(String faceVertexDefinition, ObjModelBuilder objModelBuilder)
        {
            String[] separators = new string[] { "//", "/" };
            String[] tokens = faceVertexDefinition.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 3) 
            {
                int vertexIndex = int.Parse(tokens[0]);
                int textureVertexIndex = int.Parse(tokens[1]);
                // vertex normal is ignored
                return new ObjFaceVertex(objModelBuilder.GetVertex(vertexIndex), objModelBuilder.GetTextureVertex(textureVertexIndex));
            } else if (tokens.Length == 2)
            {
                int vertexIndex = int.Parse(tokens[0]);
                // vertex normal is ignored
                return new ObjFaceVertex(objModelBuilder.GetVertex(vertexIndex), null);
            } else
            {
                throw new ArgumentException("error : a face vertex must reference at least one vertex");
            }
        }

        private static void ParseMtl(string materialFileName, ObjModelBuilder objModelBuilder)
        {
            foreach (ObjMaterial objMaterial in MtlLoader.MtlLoader.ParseMtl(materialFileName))
            {
                objModelBuilder.Materials.Add(objMaterial);
            }
        }
    }
}
