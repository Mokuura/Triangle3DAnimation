using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.ObjLoader;
using Triangle3DAnimation.Utils;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    /**
     * Generate a frame from the base model
     * 
     */
    internal class BaseFrame : FrameGenerator
    {

        public ObjModel BaseModel { get; set; }

        public BaseFrame(ObjModel baseModel) 
        {
            BaseModel = baseModel;
        }

        public List<AnimationFrame> GenerateFrames(TriangleAnimation current, TimeSingle time)
        {
            SingleModelTriangleAnimation currentAnimation = (SingleModelTriangleAnimation) current;

            List<Vec3> vertices = BaseModel.Vertices.ConvertAll(vertex => vertex.ToVec3());  

            // on the base frame, we also init triangles and vertices color
            currentAnimation.Triangles.Clear();
            currentAnimation.Triangles = BaseModel.Faces.ConvertAll(face => face.getTriangleVerticesInInt3());

            currentAnimation.VerticesColor.Clear();

            currentAnimation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), vertices.Count).ToList(); // init
            BaseModel.Faces.ForEach(face =>
            {
                // TODO check if Material is null
                // TODO check if Material.Texture not null, otherwise find another color
                // TODO warning if color face.Material.DiffuseR/G/B is not between 0 and 1
                
                // Opacity (4th value in Vec4) is not supported so just putting 1 by default
                // Adding small random shades to the color so that 2 faces of same color next to each other are differentiable 
                Vec4 color = ColorUtils.AddRandomShades(new Vec4(face.Material.DiffuseR, face.Material.DiffuseG, face.Material.DiffuseB, 1));
                currentAnimation.VerticesColor[face.V1.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V2.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V3.Vertex.Index - 1] = color;
            }); 

            return new List<AnimationFrame> { new AnimationFrame(vertices, time) };
        }
    }
}
