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
On the beginning of the scene we have 5 primitives one player and a Ball to shoot at the target.
<img src="/Images/Screenshot (4).png">

## Navigation
For navigation we have navigation keys as "A W S D" and Mouse Input for pointing the player to a direction<br> 
We have 2 options here for Navigation.<br>
1) World Space Navigation.<br>
2) Local Space Navigation.<br>

Both options have a different style of Navigation, based on the user specific preference. It can be enabled and disabled with the toggle click as you can see on the Snapshot below.
<img src="/Images/Screenshot (6).png">

## Camera Transition
When a new Primitive (figure) is spawned the camera focuses to that figure for a few seconds then It again starts looking back to the player.
<img src="/Images/Screenshot (5).png">

## Change Force
You can change the value of force applied for shooting the ball using the Force Slider. As you can see on the snapshot below.
<img src="/Images/Screenshot (7).png">

## Change The Radius within which the Primitives will spawn.
The Primitives will be spawning around the player. We have given a radius range for that. Using the Radius slider you can change the limit until where the new primitives will be spawning.
<img src="/Images/Screenshot (6).png">

## Start Again
Once you click the Start again button the game will restart.
<img src="/Images/Screenshot (9).png">

## Data From JSON
The Data for the shapes of primitives and colours are fetched sequentially from this JSON file.
<img src="/Images/Screenshot (10).png">
