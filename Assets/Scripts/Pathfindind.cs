using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;

public class Pathfindind : MonoBehaviour {
    // gCost = distància al node inicial
    // hCost = distància al node final
    // fCost = gCost + hCost

    ////public Transform seeker, target;

    PathRequestManager requestManager;
    GridScript grid;
    private int weight = 20;

    private void Awake()
    {
        requestManager = GetComponent<PathRequestManager>();
        grid = GetComponent<GridScript>();
    }

    /*private void Update()
    {
        FindPath(seeker.position, target.position);
    }*/

    public void StartFindPath(Vector3 startPos, Vector3 targetPos)
    {
        StartCoroutine(FindPath(startPos, targetPos));
    }

    IEnumerator FindPath(Vector3 startPos, Vector3 targetPos)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        // Tradueixo la posició a Node dins la grid
        Node startNode = grid.NodeFromWorldPoint(startPos);
        Node targetNode = grid.NodeFromWorldPoint(targetPos);

        //if (startNode.walkable && targetNode.walkable)
        //{

            //List<Node> openSet = new List<Node>();//
            Heap<Node> openSet = new Heap<Node>(grid.MaxSize); // Nodes no evaluats
            HashSet<Node> closedSet = new HashSet<Node>();    // Nodes evaluats
                                                              // HashSet es com una llista però no admet duplicats (com el hashmap sense el key)
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                // 1 - Busco el Node que tingui el fCost menor
                //
                /*Node currentNode = openSet[0];
                for(int i = 1; i< openSet.Count; i++)
                {
                    if(openSet[i].fCost < currentNode.fCost || openSet[i].fCost == currentNode.fCost 
                        && openSet[i].hCost < currentNode.hCost)
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);//*/
                Node currentNode = openSet.RemoveFirst(); //
                closedSet.Add(currentNode);

                if (currentNode == targetNode) // Ja he arrivat al final
                {
                    sw.Stop();
                    print("Path found: " + sw.ElapsedMilliseconds + " ms");
                    pathSuccess = true;
                    //RetracePath(startNode, targetNode);
                    break;
                }

                // 2 - Per cada veí del node que estic evaluant
                foreach (Node neighbour in grid.GetNeighbours(currentNode))
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour)) // Si no es trepitja o ja l'he evaluat, fora
                    {
                        continue;
                    }
                    //int hes = neighbour.easyWalk ? weight : 0;
                    int newMovementCostToNeighbour = currentNode.gCost + GetDistance(currentNode, neighbour) + neighbour.movementPenalty;
                    if (newMovementCostToNeighbour < neighbour.gCost  // si el cami del veí fins ara es menor que el meu
                        || !openSet.Contains(neighbour))             // o si no he evaluat el veí, evaluo el punt
                    {
                        neighbour.gCost = newMovementCostToNeighbour;
                        neighbour.hCost = GetDistance(neighbour, targetNode);
                        neighbour.parent = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }

                }
            }
        //}
        yield return null;

        if (pathSuccess)
        {
            waypoints = RetracePath(startNode, targetNode);
        }
        requestManager.FinishedProcessingPath(waypoints, pathSuccess);
    }

    Vector3[] RetracePath(Node startNode, Node endNode)
    {
        List<Node> path = new List<Node>();
        Node currentNode = endNode;

        while(currentNode != startNode)
        {
            path.Add(currentNode);
            currentNode = currentNode.parent;
        }
        //path.Reverse();
        //grid.path = path;
        Vector3[] waypoints = SimplifyPath(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] SimplifyPath(List<Node> path)
    {
        List<Vector3> waypoints = new List<Vector3>();
        Vector2 directionOld = Vector2.zero;

        for(int i = 1; i < path.Count; i++)
        {
            Vector2 directionNew = new Vector2(path[i - 1].gridX - path[i].gridX, path[i - 1].gridY - path[i].gridY);
            if(directionNew != directionOld)
            {
                waypoints.Add(path[i].worldPosition);
            }
            directionOld = directionNew;
        }
        return waypoints.ToArray();
    }

    int GetDistance(Node nodeA, Node nodeB)
    {
        int dstX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
        int dstY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

        if (dstX > dstY)
            return 14 * dstY + 10 * (dstX - dstY);
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
