using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrid : MonoBehaviour
{
    public Transform player;
    public LayerMask unwalkableMask;
    public Vector2 gridWorldSize;
    public float nodeRadius;
    PlayerNode[,] grid;//2d vector

    float nodeDiameter;
    int gridSizeX,gridSizeY;
    void Start(){
        nodeDiameter = nodeRadius*2;
        gridSizeX  = Mathf.RoundToInt(gridWorldSize.x/nodeDiameter);
        gridSizeY  = Mathf.RoundToInt(gridWorldSize.y/nodeDiameter);
        CreateGrid();

    }
    void CreateGrid(){

        grid = new PlayerNode[gridSizeX, gridSizeY];
        //transform.position is the center point, and gridworld/2 since i want to move from center to corner
        Vector3 worldBottomLeft=transform.position - (Vector3.right * gridWorldSize.x/2) - (Vector3.forward *gridWorldSize.y/2);

        for (int x=0; x< gridSizeX; x++){
            for (int y=0; y< gridSizeY; y++){
                Vector3 worldPoint = worldBottomLeft + Vector3.right* (x* nodeDiameter + nodeRadius) + Vector3.forward * (y *nodeDiameter + nodeRadius);
                //checksphere will check if collision happens
                bool walkable = !(Physics.CheckSphere(worldPoint, nodeRadius, unwalkableMask));
                grid[x,y] = new PlayerNode(walkable, worldPoint, x,y);
            
            }
        }
    }

    public List<PlayerNode> GetNeighbours(PlayerNode node){
        List<PlayerNode> neighbours = new List<PlayerNode>();
        for (int x=-1; x<=1; x++){
            for (int y=-1; y<= 1;y++){
                if (x== 0 && y == 0){
                    continue;
                }
                int checkX = node.gridX + x;
                int checkY = node.gridY + y;

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY ){
                    neighbours.Add(grid[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }



    public PlayerNode NodeFromWorldPoint(Vector3 worldPosition){
        //convert conti to discrete
        float percentX = (worldPosition.x +gridWorldSize.x/2) /gridWorldSize.x;
        float percentY = (worldPosition.z +gridWorldSize.y/2) /gridWorldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        //put -1 since index starts at zero 
        int x = Mathf.RoundToInt((gridSizeX-1)* percentX);
        int y = Mathf.RoundToInt((gridSizeY-1)* percentY);
        return grid[x,y];
    }

    public List<PlayerNode> path ;
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3 (gridWorldSize.x, 1, gridWorldSize.y));
        if (grid != null){
            PlayerNode playerNode = NodeFromWorldPoint(player.position);
            foreach(PlayerNode n in grid){
                Gizmos.color = (n.walkable)?Color.white:Color.red;
                if (playerNode == n){
                    Gizmos.color = Color.cyan;
                }
                if (path != null){
                    if (path.Contains(n)) 
                        Gizmos.color = Color.black;
                }
                Gizmos.DrawCube(n.worldPosition, Vector3.one*(nodeDiameter-.1f));
                // - to subtract, .1f have seperation btw the cubes 
            }
        }
    }

    
}
