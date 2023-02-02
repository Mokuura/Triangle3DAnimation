namespace Triangle3DAnimation.ObjLoader
{
    public class ObjTextureVertex
    {
        public float U { get; set; }
        public float V { get; set; }
        public int Index { get; set; }

        public ObjTextureVertex(float u, float v, int index)
        {
            U = u;
            V = v;
            Index = index;
        }

        public override String ToString()
        {
            return U + ", " + V + ", " + Index;
        }
    }
}