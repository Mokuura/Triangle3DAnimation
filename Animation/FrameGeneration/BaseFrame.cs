using GBX.NET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation.FrameGeneration
{
    /**
     * Generate a frame from the base model
     * 
     */
    internal class BaseFrame : FrameGenerator
    {
        public AnimationFrame GenerateFrame(TriangleAnimation current, TimeSingle time)
        {
            if (!(current is SingleModelTriangleAnimation))
            {
                throw new ArgumentException("error : a BaseFrame can only be created from a SingleModelTriangleAnimation");
            }

            SingleModelTriangleAnimation currentAnimation = (SingleModelTriangleAnimation) current;
            ObjModel baseModel = currentAnimation.BaseModel;

            List<Vec3> vertices = new List<Vec3>();
            vertices = baseModel.Vertices.ConvertAll(vertex => vertex.ToVec3());  

            // on the base frame, we also init triangles and vertices color

            currentAnimation.Triangles.Clear();
            currentAnimation.Triangles = baseModel.Faces.ConvertAll(face => face.getTriangleVerticesInInt3());

            currentAnimation.VerticesColor.Clear();
            currentAnimation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), vertices.Count).ToList(); 
            baseModel.Faces.ForEach(face =>
            {
                // TODO check if Material.Texture not null, otherwise find another color
                
                // opacity (4th value in Vec4) is not supported so just putting 1 by default
                Vec4 color = new Vec4(face.Material.DiffuseR, face.Material.DiffuseG, face.Material.DiffuseB, 1);
                currentAnimation.VerticesColor[face.V1.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V2.Vertex.Index - 1] = color;
                currentAnimation.VerticesColor[face.V3.Vertex.Index - 1] = color;

                // TODO implement this in a better way (check for values < 0). this is used to make adjacent faces of slighly different color
                Random random = new Random();
                float offset = (random.NextSingle() / 10) - 0.05f;
                currentAnimation.VerticesColor[face.V3.Vertex.Index - 1] += new Vec4(offset, offset, offset, 0);
                currentAnimation.VerticesColor[face.V2.Vertex.Index - 1] += new Vec4(offset, offset, offset, 0);
                currentAnimation.VerticesColor[face.V1.Vertex.Index - 1] += new Vec4(offset, offset, offset, 0);
            }); 

            return new AnimationFrame(vertices, time);
        }
    }
}
