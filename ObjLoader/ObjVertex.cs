namespace Triangle3DAnimation.ObjLoader
{
    public class ObjVertex
    {
        public float X { get; private set; }

        public float Y { get; private set; }

        public float Z { get; private set; }

        public int Index { get; private set; }

        public ObjVertex(float x, float y, float z, int index)
        {
            X = x;
            Y = y;
            Z = z;
            Index = index;
        }

        public override String ToString()
        {
            return X + ", " + Y + ", " + Z + ", " + Index;
        }
    }
}
