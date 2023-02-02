﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TmEssentials;
using Triangle3DAnimation.Animation.FrameGeneration;

namespace Triangle3DAnimation.Animation
{
    public class TriangleAnimation
    {
        public List<AnimationFrame> AnimationFrames { get; set; }

        public TriangleAnimation() 
        {
            AnimationFrames = new List<AnimationFrame>();
        }

        public void CreateNextFrame(FrameGenerator frameGenerator, TimeSingle time)
        {
            // TODO check if time is > time of last frame, print a warning in that case and do not throw an exception
            AnimationFrame nextFrame = frameGenerator.GenerateFrame(this, time);
            AnimationFrames.Add(nextFrame);
        }
    }
}