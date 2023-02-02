﻿using System;
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
            return "face :"; // TODO
        }
    }
}
