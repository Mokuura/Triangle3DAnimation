using ImageMagick;
using System.Diagnostics;
using System.Globalization;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.MtlLoader
{
    public static class MtlLoader
    {
        public static ObjMaterial currentMaterial;

        public static List<ObjMaterial> ParseMtl(String filePath)
        {
            List<ObjMaterial> materials = new List<ObjMaterial>();
            currentMaterial = null;
            String[] fileLines = File.ReadAllLines(filePath);
            foreach (String line in fileLines)
            {
                ParseLine(line, filePath, materials);
            }
            
            if (currentMaterial != null)
            {
                materials.Add(currentMaterial);
            }
            return materials;
        }

        private static void ParseLine(String line, String filePath, List<ObjMaterial> materials)
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
                case "newmtl":
                    if (tokens.Length < 2)
                    {
                        throw new ArgumentException("error: newmtl must define the material name");
                    }

                    if (currentMaterial != null)
                    {
                        materials.Add(currentMaterial);
                    }   
                    currentMaterial = new ObjMaterial(tokens[1]);
                    break;
                case "Kd":
                    if (currentMaterial == null)
                    {
                        throw new ArgumentException("error: Kd statement must be used after a newmtl was define");
                    }
                    if (tokens.Length == 2)
                    {
                        currentMaterial.DiffuseR = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                        currentMaterial.DiffuseG = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                        currentMaterial.DiffuseB = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                    } else if (tokens.Length == 4) 
                    {
                        currentMaterial.DiffuseR = float.Parse(tokens[1], CultureInfo.InvariantCulture);
                        currentMaterial.DiffuseG = float.Parse(tokens[2], CultureInfo.InvariantCulture);
                        currentMaterial.DiffuseB = float.Parse(tokens[3], CultureInfo.InvariantCulture);
                    } else
                    {
                        throw new ArgumentException("error: Kd statment must define 1 value (RGB all at once) or 3 values (RGB)");
                    }
                    break;
                case "map_Kd":
                    if (tokens.Length < 2)
                    {
                        throw new ArgumentException("error: map_KD statment must define a filename");
                    }

                    String textureFilePath = tokens[tokens.Length - 1];
                    using (MagickImage texture = new MagickImage(filePath + '\\' + textureFilePath))
                    {
                        // TODO
                    }
                    break;
                default:
                    // other types of data are useless for 3D Triangles in Trackmania
                    break;
            }
        }
    }
}
