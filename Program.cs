using GBX.NET.Engines.Game;
using GBX.NET;
using Triangle3DAnimation;

public class Program
{
    private static void Main(string[] args)
    {
        Tests.RunTests(args);
        return;


    //    var map = GameBox.ParseNode<CGameCtnChallenge>("C:\\Users\\colin\\OneDrive\\Documents\\Trackmania\\Maps\\My Maps\\TestGBXExplorer.Map.Gbx");

    //    var blockMediaTracker = (CGameCtnMediaBlockTriangles3D)map.ClipGroupInGame.Clips[0].Clip.Tracks[0].Blocks[0];

    //    var objLoaderFactory = new ObjLoaderFactory();
    //    var objLoader = objLoaderFactory.Create();

    //    var fileStream = new FileStream("C:\\Users\\colin\\OneDrive\\Documents\\GBX.NET\\Lowpoly_tree_sample.obj", FileMode.OpenOrCreate);
    //    var result = objLoader.Load(fileStream);

    //    Console.WriteLine(result);

    //    List<Vec4> listVertices = new List<Vec4>();
    //    List<GBX.NET.Vec3> listPositions1 = new List<GBX.NET.Vec3>();
    //    List<GBX.NET.Vec3> listPositions2 = new List<GBX.NET.Vec3>();
    //    foreach (Vertex vertex in result.Vertices)
    //    {
    //        listVertices.Add(new Vec4(0f, 0f, 0f, 1));
    //        float baseX = 950;
    //        float baseY = 17.9f;
    //        float baseZ = 920;

    //        float resizeFactor = 20;

    //        listPositions1.Add(new GBX.NET.Vec3(baseX + (vertex.X * resizeFactor), baseY + (vertex.Y * resizeFactor), baseZ + (vertex.Z * resizeFactor)));
    //        listPositions2.Add(new GBX.NET.Vec3(baseX + (vertex.X * resizeFactor), baseY + (vertex.Y * resizeFactor), baseZ + (vertex.Z * resizeFactor)));
    //    }

    //    blockMediaTracker.Keys[0].Positions = listPositions1.ToArray();
    //    blockMediaTracker.Keys[1].Positions = listPositions2.ToArray();

    //    List<Int3> listTriangles = new List<Int3>();
    //    int faceCount = 0;
    //    foreach (Group group in result.Groups)
    //    {

    //        foreach (Face face in group.Faces)
    //        {
    //            faceCount++;
    //            AddAllTrianglesForFace(face, listTriangles);

    //            for (int j = 0; j < face.Count; j++)
    //            {
    //                listVertices[face[j].VertexIndex - 1] = new Vec4(group.Material.DiffuseColor.X, group.Material.DiffuseColor.Y, group.Material.DiffuseColor.Z, 1);
    //            }
    //        }
    //        Console.WriteLine(group.Material.Name + " " + faceCount);
    //    }
    //    blockMediaTracker.Vertices = listVertices.ToArray();
    //    blockMediaTracker.Triangles = listTriangles.ToArray();


    //    Console.WriteLine("faces : " + faceCount);

    //    //TODO
    //    // actuellement, je ne duplique pas les sommets, donc un sommet peut faire parti de plusieures faces.
    //    //         c'est pratique mais ça à l'air de poser un problème car ducoup les faces connectées ont la même couleur

    //    map.Save();
    //}

    //public static void AddAllTrianglesForFace(Face face, List<Int3> listTriangles)
    //{
    //    if (face.Count < 3) return;

    //    if (face.Count == 3)
    //    {
    //        // obj format uses 1-based indexing but media tracker triangles uses 0-based indexing
    //        listTriangles.Add(new Int3(face[0].VertexIndex - 1, face[1].VertexIndex - 1, face[2].VertexIndex - 1));
    //    }
    //    else
    //    {
    //        for (int i = 1; i < face.Count - 1; i++)
    //        {
    //            listTriangles.Add(new Int3(face[0].VertexIndex - 1, face[i].VertexIndex - 1, face[i + 1].VertexIndex - 1));
    //        }
    //    }
    }
}