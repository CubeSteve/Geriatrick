using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyPathfinding : MonoBehaviour
{
    //Declare vaiables
    public GameObject target; //For current x, y and target x, y
    public Grid grid;
    public Tilemap tilemap;

    private List<Node> openSet = new List<Node>(); // Stores all nodes with a calculated f cost
    private List<Node> closedSet = new List<Node>(); // Stores all evaluated nodes
    private Node startNode;
    private Node targetNode;
    private Node currentNode;

    private List<Node> path = new List<Node>();

    private void Start()
    {
        //Find path to target
        Pathfinding(gameObject.transform.position, target.transform.position);
    }

    void Pathfinding(Vector2 startPos, Vector2 endPos)
    {
        //Initialise variables
        startNode = gameObject.AddComponent<Node>();
        targetNode = gameObject.AddComponent<Node>();

        // Finds the enemy's route
        path = FindPath();

        //If null do not pathfind
        if (path == null)
        {
            //Do nothing
        }

        //Error testing, delete later
        print(path);
        if (path != null)
        {
            foreach (Node dude in path)
            {
                print(dude);
            }
        }
    }

    List<Node> FindPath()
    {
        //If the tile has a sprite do not calculate
        if (tilemap.GetSprite(grid.WorldToCell(target.transform.position)) != null)
        {
            print("Tile has a sprite");
            return null;
        }

        //Start pathfinding at this object's cell position
        startNode.position = grid.WorldToCell(gameObject.transform.position);
        //End pathfinding at the player's cell position
        targetNode.position = grid.WorldToCell(target.transform.position);
        openSet.Add(startNode);

        //While there are nodes to be checked
        while (openSet.Count > 0)
        {
            //If out of range stop to prevent loop
            if (openSet.Count > 400)
            {
                print("Tile limit reached");
                return null;
            }

            //Make the oldest member the node being compared
            currentNode = openSet[0];
            currentNode.fCost = currentNode.gCost + currentNode.hCost;

            //Compare f costs to find the cheapest node to become the current node
            for (int i = 0; i < openSet.Count; i++)
            {
                openSet[i].fCost = openSet[i].gCost + openSet[i].hCost;
                if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                {
                    currentNode = openSet[i];
                }
            }

            //Move that node to the closed set to mark it as evaluated
            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            //If a path is found to the target
            if (currentNode.position == targetNode.position)
            {
                //Retrace path and return that path
                return RetracePath(startNode, currentNode);
            }

            //Loop through all neighbouring nodes
            foreach (Node neighbour in GetNeighbours(currentNode))
            {
                //If the node has a sprite or is in the closed set skip it
                if (tilemap.GetSprite(neighbour.position))
                {
                    continue;
                }
                foreach (Node node in closedSet)
                {
                    if (node.position == neighbour.position)
                    {
                        continue;
                    }
                }

                //Calculate neighbour's g cost
                int newCostToNeighbour = currentNode.gCost + GetDistance(currentNode.position, neighbour.position);

                //Check the open set does not contain the neighbour
                foreach (Node node in openSet)
                {
                    if (node.position == neighbour.position)
                    {
                        continue;
                    }
                }

                if (newCostToNeighbour < neighbour.gCost)
                {
                    neighbour.gCost = newCostToNeighbour;
                    neighbour.hCost = GetDistance(neighbour.position, Vector3Int.FloorToInt(target.transform.position));
                    neighbour.parent = currentNode;

                    foreach (Node node in openSet)
                    {
                        if (node.position == neighbour.position)
                        {
                            continue;
                        }
                    }

                    //if (!openSet.Contains(neighbour))
                    //{
                    openSet.Add(neighbour);
                    //}
                }
            }
        }

        //Fail
        print("Fail reached");
        return null;
    }

    List<Node> RetracePath(Node start, Node end)
    {
        //Reverses the list to get the route the enemy has to take
        List<Node> retracedPath = new List<Node>();
        Node thisNode = end;

        while (thisNode != start)
        {
            retracedPath.Add(thisNode);
            thisNode = thisNode.parent;
        }
        retracedPath.Reverse();
        return retracedPath;
    }

    List<Node> GetNeighbours(Node node)
    {
        //Gets all of the nodes surrounding a node
        List<Node> neighbours = new List<Node>();

        //Loop though rows
        for (int x = -1; x <= 1; x++)
        {
            //Loop through colums
            for (int y = -1; y <= 1; y++)
            {
                //Skip the middle node
                if (x == 0 && y == 0)
                {
                    continue;
                }

                Node newNeighbour = new Node();
                newNeighbour.position.x = node.position.x + x;
                newNeighbour.position.y = node.position.y + y;

                neighbours.Add(newNeighbour);
            }
        }

        return neighbours;
    }

    int GetDistance(Vector3Int nodeA, Vector3Int nodeB)
    {
        //Returns the distance between two nodes
        int dstX = Mathf.Abs(nodeA.x - nodeB.x);
        int dstY = Mathf.Abs(nodeA.y - nodeB.y);

        if (dstX > dstY)
        {
            return 14 * dstY + 10 * (dstX - dstY);
        }
        return 14 * dstX + 10 * (dstY - dstX);
    }
}
