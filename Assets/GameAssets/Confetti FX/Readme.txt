----------------------------------------
---Confetti FX--------------------------
----------------------------------------

To use the effects, import the asset to your project.
The effect prefabs can simply be dragged and dropped into the scene.

----------------------------------------
2D Prefabs Explained
----------------------------------------

The 2D prefabs use a spritesheet with 16 different confetti shapes. Colors can be changed easily near the top of the settings.
These shapes will be picked at random, but you can also change this in the Texture Sheet Animation settings.

----------------------------------------
3D Prefabs Explained
----------------------------------------

The 3D prefabs use the Standard Shader, which means the color settings in the particle system won't work.
If you want to combine colors, there are 7 colors which you can "nest" on eachother, similar to the "Mix" prefabs.

If you for example want to combine <Effectname>Blue and <Effectname>Yellow, you can do this:

1. Drop <Effectname>Blue into the scene
2. Drop <Effectname>Yellow on <Effectname>Blue
3. When you select <Effectname>Blue and press the Simulate button in the Scene window, you should see both effects play

If you were to customize your emitter a lot and decide on adding several colors,
you can duplicate it and drag a different material on it from the "Confetti FX\Materials\3D Materials"-folder.

----------------------------------------

Confetti FX by Kenneth "Archanor" Foldal Moe

Twitter: @archanor
Support: archanor.work@gmail.com

----------------------------------------