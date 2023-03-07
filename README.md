# Triangle3DAnimation

Triangle3DAnimation is a tool to generate animations for Trackmania with obj files, using mediatracker triangle3D blocks. 
It uses GBX.NET to build triangle3D mediatracker blocks, that you can then add to your maps.

# Usage

Start by cloning this repo.

Here's an example of what you can do with this tool :

```
// 1 : Load your obj files
// You can either load a single obj file, or an animation in the form of one obj file per frame,
// with a number at the end corresponding to the frame index (one-based indexing).
// If you load an animation, every obj file needs to have the same faces, only the vertex positions
// can change.
// If your obj file uses any mtl file, they need to have the same name and be in the same directory.

// To load a single obj file
// PATH_TO_DIRECTORY is the path of the directory where your obj file is located
// FILE_NAME is the name of the obj file without ".obj"
ObjModel objModel = ObjLoader.ObjLoader.ParseObj("PATH_TO_DIRECTORY", "FILE_NAME");

// To load an animation
// PATH_TO_DIRECTORY is the path of the directory where all the obj files are located
// FILE_NAME is the name of the obj file without ".obj" and without the frame index
ObjAnimation objAnimation = ObjLoader.ObjLoader.ParseObjAnimation("PATH_TO_DIRECTORY", "FILE_NAME");

// 2 : Create the base of your animation and build the animation
Base baseModel = new BaseModel(objModel);

// the second parameter is the length between each frame of your animation, in seconds 
Base baseAnimation = new BaseAnimation(objAnimation, TmEssentials.TimeSingle.FromSeconds(0.2f));

SingleBlockTriangleAnimation animationWithSingleObj = new SingleBlockTriangleAnimation(baseModel);
SingleBlockTriangleAnimation animationWithMultipleObj = new SingleBlockTriangleAnimation(baseAnimation);

// At this point :
// animationWithSingleObj has 1 frame at time = 0s, we need to add more frames using transformations.
// animationWithMultipleObj has one frame per obj file, starting at time = 0s and spaced by the lenght between each frame.
// A frame correspond to a list of coordinates, one for each of the vertex that the base of your animation is made off.
// Trackmania transition between frames using linear interpolation.

// 3 : Add transformations
// You can add transformations in any order, just by specifying the start time and end time.
// Your animation will end when the last transformation (the one with the longer end time) is finished.
animationWithSingleObj.AddTransformation(new Translation(
    new Vec3(10, 10, 10), // Translation vector
    TmEssentials.TimeSingle.FromSeconds(0), // start time
    TmEssentials.TimeSingle.FromSeconds(3f) // end time
));

animationWithSingleObj.AddTransformation(new Rotation(
    MathF.PI / 16, // Pitch
    0,             // Yaw
    0,             // Roll
    10,            // NbOfSteps : The bigger, the smoother the animation will be
    TmEssentials.TimeSingle.FromSeconds(1.5f), // start time
    TmEssentials.TimeSingle.FromSeconds(4f)    // end time
));

animationWithSingleObj.AddTransformation(new Scaling(
    3, // Scaling factor
    TmEssentials.TimeSingle.FromSeconds(1.5f), // start time
    TmEssentials.TimeSingle.FromSeconds(4f) // end time
));

animationWithSingleObj.AddTransformation(new Identity(
    TmEssentials.TimeSingle.FromSeconds(4f), // start time
    TmEssentials.TimeSingle.FromSeconds(6f) // end time
));

// All type of transformations can be used on animationWithMultipleObj as well ;)

animationWithMultipleObj.AddTransformation(new Scaling(
    5, // Scaling factor
    TmEssentials.TimeSingle.FromSeconds(0), // start time
    TmEssentials.TimeSingle.FromSeconds(4f) // end time
));

// etc ...

// 4 : Generate the frames
// Based on all the transformations you added, all the frames will be created
animationWithSingleObj.GenerateFrames();
animationWithMultipleObj.GenerateFrames();
            
// 5 : Export to Trackmania
// Using GBX.NET, add the animation to your map.
var map = GameBox.ParseNode<CGameCtnChallenge>("PATH_TO_YOUR_MAP");

// Access the mediatracker Track where you want to add the animation.
var track1 = map.ClipGroupInGame.Clips[0].Clip.Tracks[0];
var track2 = map.ClipGroupInGame.Clips[1].Clip.Tracks[0];

// Convert your animation to a mediatracker block, with the coordinates of the origin.
CGameCtnMediaBlockTriangles3D block1 = animationWithSingleObj.ToTriangle3DMediaTrackerBlock(new Vec3(1000, 1000, 1000));
CGameCtnMediaBlockTriangles3D block2 = animationWithMultipleObj.ToTriangle3DMediaTrackerBlock(new Vec3(800, 1000, 1000));

// Add the new blocks to mediatracker tracks.
track1.Blocks.Clear();
track2.Blocks.Clear();
track1.Blocks.Add(block1);
track2.Blocks.Add(block2);
            
// save the map, Done.
map.Save();
```

# Limitations
