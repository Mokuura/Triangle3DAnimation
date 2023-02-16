using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.FrameGeneration;
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
        public override List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            if (!(current is SingleBlockTriangleAnimation))
            {
                throw new ArgumentException("error : a BaseFrame can only be created from a SingleModelTriangleAnimation");
            }

            SingleBlockTriangleAnimation currentAnimation = (SingleBlockTriangleAnimation)current;
            ObjModel baseModel = currentAnimation.BaseModel;

            List<Vec3> vertices = new List<Vec3>();
            vertices = baseModel.Vertices.ConvertAll(vertex => vertex.ToVec3());

            // on the base frame, we also init triangles and vertices color
            currentAnimation.Triangles.Clear();
            currentAnimation.Triangles = baseModel.Faces.ConvertAll(face => face.getTriangleVerticesInInt3());

            currentAnimation.VerticesColor.Clear();

            currentAnimation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), vertices.Count).ToList(); // init
            baseModel.Faces.ForEach(face =>
            {
                // TODO check if Material is null
                // TODO check if Material.Texture not null, otherwise find another color

                // Opacity (4th value in Vec4) is not supported so just putting 1 by default
                // Adding small random shades to the color so that 2 faces of same color next to each other are differentiable 
                Vec4 color = ColorUtils.AddShades(new Vec4(face.Material.DiffuseR, face.Material.DiffuseG, face.Material.DiffuseB, 1));
                currentAnimation.VerticesColor[face.V1.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V2.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V3.Vertex.Index - 1] = color;
            });

            return new List<AnimationFrame> { new AnimationFrame(vertices, time) };
        }
    }
}
