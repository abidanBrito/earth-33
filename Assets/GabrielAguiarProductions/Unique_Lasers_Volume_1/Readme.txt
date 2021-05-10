Unique Lasers Volume 1 - Readme


This package contains:

- 9 Lasers Prefabs (customizable);
- 11 Noise Textures (for customization);
- Other Textures (for customization);
- Laser Beam Script (assign camera, control fire point, maximum length, size, speed, etc);
- Particle System Controller Script (control size, speed, color, lights, trails, enable/disable vfxs, etc)



TPS_SCENE & FPS_SCENE - SHORTCUTS:

Mouse 1 - Fire Laser
E - Next Effect
Q - Previous Effect



LASER SCRIPT - DESCRIPTION:

For the Laser to work, it needs a couple of things. 

The first thing it needs is a List of the Line Renderers, which is basically the Lasers parented to each prefab.

After that we need to tell the Laser which Camera the ray is going to be calculated for colisions. So for instance if you want to use the Lasers for a First Person Shooter game, you can simply switch the camera, assign the respective gun as the fire point and that's it. 

The Maximum Lenght of the Laser, in case it doesn't hit anything, this is going to be the maximum lenght.

The Grow Width is how much it will overgrow in the beginning. As you can see each one of this Line Renderers has a width. This parameter will add some extra width when the Laser is shot.

The Grow Speed is the rate at which the Grow Width expands everytime the laser is fired. 

The Shrink Speed is useful in case we want the laser to shrink faster then it grows or vice-versa.

The Disable Delay is how much time the laser will keep shooting after we release the fire button.

The Trail Interval will allow you to decide if you want a continuous trail, which it's 0. If you set it to other number it will have that interval.



PARTICLE SYSTEM CONTROLLER SCRIPT - DESCRIPTION:

Script allows you to resize, slow down or slow up your effects. They also allow you to quickly decide if you want them to Loop, if you want Lights or Trails (in case there is any light or trails the script will find it and let you decide to turn it on or off) whichs is useful for mobile developers.

Effects contain distortions, shockwaves, smoke, particles and other effects that can be easily turned off if developing for mobile. 

Spritesheets are provided in PNG format with the original size and with low resolution for mobile. You can also resize the orignal ones with Unity.

*Quick explanation of how the Script works: https: youtu.be/Pm6hHGOvL4s?t=26



MOBILE IMPROVEMENTS:

- I recommend you deactive distortion, trails, lights and even the dark part of the laser. Those are the main GPU hungry factors of this effects;

- Use the least amount of particle systems possible (by turning off with the script "ParticleSystemController";



POST-PROCESSING EFFECTS INFO:

All footage was done using Post-Processing Effects. Follow this steps if you want to achieve that quality:

1) Go to Unity Archives ( https://unity3d.com/get-unity/download/archive ), select your version and download the Standard Assets from the drop-down list;

2) Import that package to your project in Unity (you can only import the Effects folder if you want);

3) In the DemoScene01 you can add "Depth Of Field", "Bloom", "Vignetter And Chromatic Aberration", "Sun Shafts" and "Color Correction Curves" components to the cameras;

4) Copy values from screenshot "PostProcessingParameters";

4) Enjoy and Have Fun!



CONTACTS:

Feel free to contact me via links bellow in case you have any doubts. 

Twitter: @GabrielAguiProd

Facebook: facebook.com/gabrielaguiarprod/

YouTube: youtube.com/c/gabrielaguiarprod



Thank you for purchasing the Unique Lasers Volume 1 package.
Unique Lasers Volume 01 is created by Gabriel Aguiar
