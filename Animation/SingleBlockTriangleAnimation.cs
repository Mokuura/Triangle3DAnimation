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

        public InitTransformation InitTransformation { get; set; }

        public TimeSingle Start { get; set; }

        public TimeSingle End { get; set; }

        public SingleBlockTriangleAnimation(InitTransformation initTransformation) : base() 
        {
            AnimationFrames = new List<AnimationFrame>();
            VerticesColor = new List<Vec4>();
            Triangles = new List<Int3>();
            Transformations = new List<Transformation>();
            InitTransformation = initTransformation;
            Start = InitTransformation.Start;
            End = InitTransformation.End;
        }

        public void AddTransformation(Transformation transformation)
        {
            if (transformation is InitTransformation)
            {
                throw new Exception("error: cannot add a InitTransformation");
            }
            
            if (transformation.Start < Start || transformation.End > End)
            {
                throw new Exception("error: transformation start or end time is outside the animation period");
            }

            Transformations.Add(transformation);         
        }

        public void GenerateFrames()
        {
            List<Transformation> transformations = new List<Transformation>(Transformations);
            if (InitTransformation is InitAnimation)
            {
                transformations.AddRange(((InitAnimation) InitTransformation).getAllInitModel());
            } else
            {
                transformations.Add(InitTransformation);
            }

            // TODO if InitTransformation is an animation, then ignore all other transformations
            // (not supported yet)
            
            foreach (Transformation transformation in transformations)
            {
                if (transformation is Rotation)
                {
                    transformations.AddRange(((Rotation) transformation).getAllRotationsPerStep());
                    transformations.Remove(transformation);
                }
            }

            List<Transformation> transformationsWithoutPartialOverlap = new List<Transformation>();
            foreach (Transformation transformation in transformations) 
            {
                transformationsWithoutPartialOverlap.AddRange(transformation.GetWithoutPartialOverlap(Transformations));
            }

            // merge transformations that fully overlap
            List<List<Transformation>> OrderedAndGroupedTransformations = new List<List<Transformation>>(); // TODO
            // TODO check that every transformation in a group has the same start & end

            // apply all the transformations
            AnimationFrame? lastFrame = null;
            foreach (List<Transformation> frameTransformations in OrderedAndGroupedTransformations)
            {
                AnimationFrame newFrame = GenerateFrameFromTransformation(lastFrame, frameTransformations);
                AnimationFrames.Add(newFrame);
                lastFrame = newFrame;
            }
        }

        public static AnimationFrame GenerateFrameFromTransformation(AnimationFrame? lastFrame, List<Transformation> transformations)
        {
            List<Vec3>? newPositions;
            if (lastFrame == null)
            {
                foreach (Transformation transformation in transformations)
                {
                    newPositions = transformation.apply(newPositions);
                    // TODO check that first transformation is of type InitModel
                }
            } else
            {
                newPositions = lastFrame.VerticesPositions;
                foreach (Transformation transformation in transformations)
                {
                    newPositions = transformation.apply(newPositions); 
                }
            }

       
            return new AnimationFrame(newPositions, transformations[0].End);
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
