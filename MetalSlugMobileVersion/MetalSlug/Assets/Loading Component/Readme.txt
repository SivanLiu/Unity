Thank you for purchasing Loading Component.

To test the included example scene first you have to add the two scenes ("1" and "2") to your Build Settings, then open scene "1" and see how it works.

How to use:
===========

-Drag & drop "LoadingObject" prefab to your scene heirarchy.
- Setup the parameters as you wish, put the name of the next scene to load (don't forget to add your scenes to Build Settings).
-Now when you want to load the next scene, just call the function LoadNextLevel() by sending a message to the game object holding the LoadingComponent, like this:

SendMessage("LoadNextLevel");


Note:
=====

I put a "Start Delay" parameter so that if you have small scenes in size to be loaded which normally take a very little time (especially if you have a high-end system) then you can control how much time in seconds you want the next scene to start after the end of loading operation (when it reaches 100%) so you can put more time to start the next scene.

On the other hand if you have huge scenes to be loaded, then there is no need to put a high number in "Start Delay" as it will already takes its necessary time to be loaded.

 
 
