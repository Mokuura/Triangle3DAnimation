using GBX.NET;
using GBX.NET.Inputs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.Transformations;
using Triangle3DAnimation.ObjLoader;
using Triangle3DAnimation.Utils;

namespace Triangle3DAnimation.Animation
{

    internal class BaseModel : Base
    {
        public ObjModel Model { get; set; }
        
        public BaseModel(ObjModel model, float shadingIntensity) : base(shadingIntensity)
        { 
            Model = model;
        }

        public BaseModel(ObjModel model) : base()
        {
            Model = model;
        }

        public override void InitAnimation(SingleBlockTriangleAnimation animation)
        {
            InitAnimation(Model, animation, ShadingIntensity);
        }

        public static void InitAnimation(ObjModel model, SingleBlockTriangleAnimation animation, float shadingIntensity)
        {
            // on the base frame, we also init triangles and vertices color
            animation.Triangles.Clear();
            animation.Triangles = model.Faces.ConvertAll(face => face.getTriangleVerticesInInt3());

            animation.VerticesColor.Clear();

            animation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), model.Vertices.Count).ToList(); // init
            model.Faces.ForEach(face =>
            {
                Vec4 color;
                if (face.Material != null)
                {
                    // Opacity (4th value in Vec4) is not supported so just putting 1 by default
                    // Adding small random shades to the color so that 2 faces of same color next to each other are differentiable 
                    color = ColorUtils.AddRandomShades(new Vec4(face.Material.DiffuseR, face.Material.DiffuseG, face.Material.DiffuseB, 1), shadingIntensity);    
                } else
                {
                    // default
                    color = ColorUtils.AddRandomShades(new Vec4(0.5f, 0.5f, 0.5f, 1), shadingIntensity);
                }

                if (inRange(face.V1.Vertex.Index - 1, animation.VerticesColor)) animation.VerticesColor[face.V1.Vertex.Index - 1] = color;
                if (inRange(face.V2.Vertex.Index - 1, animation.VerticesColor)) animation.VerticesColor[face.V2.Vertex.Index - 1] = color;
                if (inRange(face.V3.Vertex.Index - 1, animation.VerticesColor)) animation.VerticesColor[face.V3.Vertex.Index - 1] = color;
            });
        }

        private static bool inRange(int index, List<Vec4> list) 
        {
            return (index >= 0) && (index < list.Count);
        }

        public override AnimationFrame GetFirstFrame(TimeSingle time)
        {
            return new AnimationFrame(Model.Vertices.ConvertAll(vertex => vertex.ToVec3()), time);
        }
    }
}
