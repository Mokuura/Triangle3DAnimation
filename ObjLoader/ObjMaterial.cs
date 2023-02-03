namespace Triangle3DAnimation.ObjLoader
{
    public class ObjMaterial
    {
        public String Name { get; set; }
        public float DiffuseR { get; set; }
        public float DiffuseG { get; set; }
        public float DiffuseB { get; set; }

        public ObjMaterial(String name)
        {
            this.Name = name;
        }
    }
}