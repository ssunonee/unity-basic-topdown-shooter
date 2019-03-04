# unity-basic-topdown-shooter 2017.4.20+
1. Every actor have 3 health points, can move forward, turn around and shoot. Every hit reduces health by 1 point. Actor is removed if he runs out of health points.
2. There is a basic implementation of enemy AI, different controllers for player input and enemy input. Actor script has no knowledge about type of controller it controlled by.
3. At the start of the game player and variable quantity of enemies gets spawned.
4. Last surviving actor is considered a winner.
5. Player can see how many health points he has in the top left corner of screen.
6. Actors can collect speed up bonuses, which periodically spawn at the playground. Bonus doubles move speed and fire rate for 4 seconds, bonuses don't stack.
- - - -
![alt text](https://raw.githubusercontent.com/wolfonanareta/unity-basic-topdown-shooter/master/image.png)
