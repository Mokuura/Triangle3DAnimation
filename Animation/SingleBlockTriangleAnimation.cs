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

        public TimeSingle? End { get; set; }

        public SingleBlockTriangleAnimation(Base baseModel) : base() 
        {
            AnimationFrames = new List<AnimationFrame>();
            VerticesColor = new List<Vec4>();
            Triangles = new List<Int3>();
            Transformations = new List<Transformation>();
            BaseOfAnimation = baseModel;
            Start = TmEssentials.TimeSingle.FromSeconds(0);
        }

        public void AddTransformation(Transformation transformation)
        {
            if (End == null || End < transformation.End)
            {
                End = transformation.End;
            }
            
            // subdivide all rotations            
            if (transformation is Rotation)
            {
                Transformations.AddRange(((Rotation)transformation).getAllRotationsPerStep());
            }
            else
            {
                Transformations.Add(transformation);
            }       
        }

        public void GenerateFrames()
        {
            AnimationFrames.Clear();
            // init colors and faces
            BaseOfAnimation.InitAnimation(this);
            AnimationFrames.Add(BaseOfAnimation.GetFirstFrame(Start));

            if (BaseOfAnimation is BaseAnimation)
            {

                Transformations.AddRange(((BaseAnimation)BaseOfAnimation).GenerateAllTransformations(Start, End));
            }

            if (Transformations.Count <= 0) 
            {
                return;
            }

            // remove all partial overlap
            List<Transformation> transformationsWithoutPartialOverlap = new List<Transformation>();
            foreach (Transformation transformation in Transformations) 
            {
                transformationsWithoutPartialOverlap.AddRange(transformation.GetWithoutPartialOverlap(Transformations));
            }

            // merge transformations that fully overlap
            List<List<Transformation>> orderedAndGroupedTransformations = OrderAndGroupTransformations(transformationsWithoutPartialOverlap);

            // apply all the transformations
            AnimationFrame baseFrame = AnimationFrames[0];
            AnimationFrame lastFrame = baseFrame;
            List<Transformation> previousTransformations = new List<Transformation>();
            TimeSingle lastTransformationEndTime = Start;
            foreach (List<Transformation> frameTransformations in orderedAndGroupedTransformations)
            {
                if (frameTransformations[0].Start != lastTransformationEndTime)
                {
                    AnimationFrames.Add(new AnimationFrame(lastFrame.VerticesPositions, frameTransformations[0].Start));
                }
                AnimationFrame newFrame = GenerateFrameFromTransformation(baseFrame, frameTransformations.Concat(previousTransformations).ToList());
                AnimationFrames.Add(newFrame);
                lastFrame = newFrame;
                previousTransformations.AddRange(frameTransformations);
            }
        }

        public static List<List<Transformation>> OrderAndGroupTransformations(List<Transformation> transformations)
        {
            List<List<Transformation>> result = new List<List<Transformation>>();
            List<TimeSingle> startTimes = transformations.Select(transformation => transformation.Start).Distinct().ToList();
            
            foreach (TimeSingle start in startTimes) 
            {
                result.Add(transformations.Where(transformation => transformation.Start == start).ToList());
            }

            return result;
        }

        public static AnimationFrame GenerateFrameFromTransformation(AnimationFrame baseFrame, List<Transformation> transformations)
        {
            List<Vec3> newPositions = baseFrame.VerticesPositions;

            // priority to CustomTransformations
            foreach (Transformation transformation in transformations)
            {
                if (transformation is CustomTransformation)
                {
                    newPositions = transformation.apply(newPositions);
                }     
            }

            foreach (Transformation transformation in transformations)
            {
                if (!(transformation is CustomTransformation))
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
