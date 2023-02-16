using GBX.NET;
using GBX.NET.Engines.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.Transformations;
using Triangle3DAnimation.ObjLoader;

namespace Triangle3DAnimation.Animation
{
    /*
     * Animation based on a single 3D model. A single block of media tracker will be used to represent it,
     * meaning only the positions of vertices can change during the animation. Colors can't change and it's 
     * impossible to introduce new vertices/new triangles.
     */
    public class SingleBlockTriangleAnimation : TriangleAnimation
    {

        public List<Vec4> VerticesColor { get; set; }

        public List<Int3> Triangles { get; set; }

        public List<AnimationFrame> AnimationFrames { get; set; }

        public List<Transformation> Transformations { get; set; }

        public Base BaseOfAnimation { get; set; }

        public TimeSingle Start { get; set; }

        public TimeSingle End { get; set; }

        public SingleBlockTriangleAnimation(Base baseOfAnimation, TimeSingle start, TimeSingle end) : base() 
        {
            AnimationFrames = new List<AnimationFrame>();
            VerticesColor = new List<Vec4>();
            Triangles = new List<Int3>();
            Transformations = new List<Transformation>();
            BaseOfAnimation = baseOfAnimation;
            Start = start;
            End = end;
        }

        public void AddTransformation(Transformation transformation)
        {
            if (transformation.Start < Start || transformation.End > End)
            {
                throw new Exception("error: transformation start or end time is outside the animation period");
            }
            Transformations.Add(transformation);         
        }

        public void GenerateFrames()
        {
            // TODO divide rotation transformation according to Number of steps


            List<Transformation> transformationsWithoutPartialOverlap = new List<Transformation>();
            foreach (Transformation transformation in Transformations) 
            {
                transformationsWithoutPartialOverlap.AddRange(transformation.GetWithoutPartialOverlap(Transformations));
            }  
            
            
            // TODO generates the frames
        }

        public CGameCtnMediaBlockTriangles3D ToTriangle3DMediaTrackerBlock(Vec3 position)
        {
            if (AnimationFrames.Count < 2)
            {
                throw new Exception("error : a BlockTriangles3D must have at least 2 frames");
            }

            CGameCtnMediaBlockTriangles3D triangle3DBlock = CGameCtnMediaBlockTriangles3D.Create(VerticesColor.ToArray()).ForTMUF().Build();
            triangle3DBlock.Vertices = VerticesColor.ToArray();
            triangle3DBlock.Triangles= Triangles.ToArray();
            triangle3DBlock.Keys = AnimationFrames.ConvertAll<CGameCtnMediaBlockTriangles.Key>(frame => frame.ToMediaTrackerKey(triangle3DBlock, position));

            return triangle3DBlock;
        }
    }
}
