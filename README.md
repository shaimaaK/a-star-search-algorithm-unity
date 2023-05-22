# Path Finding Algorithm : A Star Algorithm 
This project is implemented as part of the Robotics Course in my masters degree. The project simulates the shortest path problem in the setting of a maze where several 
issues of mobile robots encounters:
1. Obstacle Avoidance
2. Find the path leading out of the maze
3. Finding the shortest route

## Demo
![unity-simulation](https://github.com/shaimaaK/a-star-search-algorithm-unity/assets/54285485/127eb097-89ee-476b-9c6f-d69a2695be1c)
![a-star-unity](https://github.com/shaimaaK/a-star-search-algorithm-unity/assets/54285485/7930e06f-4936-411b-b81a-e303bb6a8ca1)



## Table of contents
* [Problem Description](#description)
* [A Guide to Installation and Use](#a-guide-to-installation-and-use)


## Description
The purpose of this project is simulating the path finding algorithm embedded into Unity Game Engine, by creating a [NavMesh agent](https://docs.unity3d.com/Manual/nav-Overview.html). Unity engine renders the game area to define walkable and non-walkable areas for the agent then find the shortest path to the target location as in the [official unity documentation](https://docs.unity3d.com/Manual/nav-InnerWorkings.html).
I developed an algorithm that optimizes the shorest path by: 
1. defines the obstacles to avoid 
2. Find the soltuion to the maze
3. finds the shortest route using `A star search algorithm`
The player is indicated by the red cylinder, and the target location (out of the maze) is represented by the blue sphere.
The floor of the maze is composed of small cube unit that is color coded as the following :
- 🟥 -> collision area
- ◻️ -> walkable area
- 🟦 -> agent initial location
- ⬛ -> shortest route out of the maze, which is computed using `A star search algorithm`
## A Guide to Installation and Use
To try the project, clone the repository and run the **A star algo** scene.<br>
For comparision purposes the NavMesh Agent in **simple maze** scene.





