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

            // opacity (4th value in Vec4) is not supported so just putting 1 by default
            currentAnimation.VerticesColor = Enumerable.Repeat(new Vec4(1, 1, 1, 1), vertices.Count).ToList(); // TODO find the correct color

            return new AnimationFrame(vertices, time);
        }
    }
}
