using GBX.NET;
using GBX.NET.Inputs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.Transformations;
using Triangle3DAnimation.ObjLoader;
using Triangle3DAnimation.Utils;

namespace Triangle3DAnimation.Animation
{
    /**
     * Generate a frame from the base model
     * 
     */
    internal class BaseModel : Base
    {
        public ObjModel Model { get; set; }
        public BaseModel(ObjModel model)
        { 
            Model = model;
        }

        public void InitAnimation(SingleBlockTriangleAnimation animation)
        {
            InitAnimation(Model, animation);
        }

        public static void InitAnimation(ObjModel model, SingleBlockTriangleAnimation animation)
        {
            // on the base frame, we also init triangles and vertices color
            animation.Triangles.Clear();
            animation.Triangles = model.Faces.ConvertAll(face => face.getTriangleVerticesInInt3());

            animation.VerticesColor.Clear();

            animation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), model.Vertices.Count).ToList(); // init
            model.Faces.ForEach(face =>
            {
                // TODO check if Material is null
                // TODO check if Material.Texture not null, otherwise find another color

                // Opacity (4th value in Vec4) is not supported so just putting 1 by default
                // Adding small random shades to the color so that 2 faces of same color next to each other are differentiable 
                Vec4 color = ColorUtils.AddRandomShades(new Vec4(face.Material.DiffuseR, face.Material.DiffuseG, face.Material.DiffuseB, 1));
                animation.VerticesColor[face.V1.Vertex.Index - 1] = color;
                animation.VerticesColor[face.V2.Vertex.Index - 1] = color;
                animation.VerticesColor[face.V3.Vertex.Index - 1] = color;
            });
        }

        public AnimationFrame GetFirstFrame(TimeSingle time)
        {
            return new AnimationFrame(Model.Vertices.ConvertAll(vertex => vertex.ToVec3()), time);
        }
    }
}
