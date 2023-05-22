using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding : MonoBehaviour
{
    public Transform seeker,target ;
    PlayerGrid grid;
    void Awake(){
        grid = GetComponent<PlayerGrid>();

    }
    void Update(){
        FindPath(seeker.position,target.position);
    }
    // Start is called before the first frame update
    void FindPath(Vector3 startPos, Vector3 targetPos){
        PlayerNode startNode = grid.NodeFromWorldPoint(startPos);
        PlayerNode targetNode = grid.NodeFromWorldPoint(targetPos);

        //create open and closed set
        List<PlayerNode> openSet = new List<PlayerNode>();
        HashSet<PlayerNode> closedSet = new HashSet<PlayerNode> ();

        openSet.Add(startNode);

        while (openSet.Count > 0){
        // while openset is not empty
            PlayerNode currentNode = openSet[0];
            //search for next move
            for (int i =1; i < openSet.Count; i++){
                if (openSet[i].fCost < currentNode.fCost || openSet[i].fCost ==currentNode.fCost && openSet[i].hCost < currentNode.hCost){
                    currentNode = openSet[i];
                }
            }
            // mark visited node from openset and add it to closed (visited) set
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);
            // if i arrive at destination or not
            if (currentNode == targetNode){
                RetracePath(startNode,  targetNode);
                return;
            }
            foreach (PlayerNode neighbour in grid.GetNeighbours(currentNode)){
                if (!neighbour.walkable || closedSet.Contains(neighbour)){
                    continue;
                }
                //distance to the new neighbor is the distance walked + new c->n distance
                int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour);
                //if node is not visited or 
                if (newMovementCostToNeighbour < neighbour.gCost || !openSet.Contains(neighbour)){
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour, targetNode);
                    neighbour.parent = currentNode;
                    if (!openSet.Contains(neighbour)){
                        openSet.Add(neighbour);
                    }
                }
            }
        }


    }
    void RetracePath(PlayerNode startNode, PlayerNode endNode){
        List <PlayerNode> path = new List<PlayerNode>();
        PlayerNode currentNode = endNode;

        while (currentNode != startNode){
            path.Add(currentNode);
            currentNode= currentNode.parent;
        }
        path.Reverse();
        grid.path=path;
    }
    int GetDistance (PlayerNode nodeA, PlayerNode nodeB){
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);
        if (dstX >dstY){
            return  14* dstY + 10*(dstX-dstY);
        }
        return  14* dstX + 10*(dstY-dstX);
    }
}
