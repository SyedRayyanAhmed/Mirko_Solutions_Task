# Mirko Solutions Interview

Build file can be found inside the Build folder.<br>
Build > Build.rar<br>

## Unity Version (URP)
2021.3.26f1

## Terminology
For simplicity and understanding these are the defination of the terminologies mentioned in the project.<br>

- Primitives = Geometric shapes.<br>
- Random Position = A location that is chosen randomly.<br>
- Random Colour = A colour that is chosen randomly.<br>
- Radius = A distance from the player within which the primitives are placed randomly.<br>
- Force = A measure of how hard and fast a ball is thrown.<br>
- Camera Transition = A change in the camera's focus to a new figure.<br>

## Main Scene Layout
On the beginning of the scene we have 1 primitives one player and a Ball to shoot at the target.
<img src="/Images/Screenshot (26).png">

## Navigation
For navigation we have axis control. On PC it's mouse movement and On Joystick it's axis controller. Using axis control we change the view of the camera as Up & Down view. We can shoot the target using "s" button on PC, "A" button on X-box and Joystick Button 0.<br> 

reference on the Snapshot below.
<img src="/Images/element (2).png">

## Creating Shapes
We have two primitive shapes in our disposal. 1) Sphere.fbx 2) Cube.fbx<br>
using Math formula and calculations we created the other shapes from these primitives such as. 1) Rectangle 2) Ellipse
<img src="/Images/Screenshot (20).png">

## Mesh Creation
For Primitive shape such as Pyramid we did Mesh creation usind 3D Co-Ordinates, Vertices, Triangles and Polygons.
<img src="/Images/element.png">

## Destroy with Physics simulation
For destroying the target we used Physics explosion simulation to make the physics look realistic.
<img src="/Images/element (1).png">

## Camera Transition
When a new Primitive (figure) is spawned the camera focuses to that figure for a few seconds then It again starts looking back to the player.
<img src="/Images/Screenshot (33).png">

## Ball Colour is same as the target Colour
The colour of the ball is fetched from JSON after hitting and destroying every target.
<img src="/Images/Screenshot (28).png">

## Change The Radius within which the Primitives will spawn.
The Primitives will be spawning around the player. We have given a radius range for that. Using the Radius slider you can change the limit until where the new primitives will be spawning.
<img src="/Images/Screenshot (23).png">

## Change Force - Start Again - Shoot Again
You can change the value of force applied for shooting the ball using the Force Slider. As you can see on the snapshot below. If you Click Shoot again button the ball will respawn and if you click the Start again button the game will restart.
<img src="/Images/Screenshot Copy(26).png">

## Data From JSON
The Data for the shapes of primitives and colours are fetched sequentially from this JSON file.
<img src="/Images/Screenshot (110).png">
