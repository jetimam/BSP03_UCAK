using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstAI : IPathFinding
{
    private MazeGenerator.Cell[,] maze;
    private Dictionary<Vector3, MazeGenerator.Cell> coordinateTable;
    private Vector3 currentCell, previousCell, startingPosition;
    private List<Vector3> path;
    private List<Vector3> visited;
    private Queue<Vector3> queue;
    private Dictionary<Vector3, Vector3> backtrackingTable;
    private bool found;

    public BreadthFirstAI()
    {
        this.maze = GameObject.Find("Game").GetComponent<GameLoop>().maze;
        this.coordinateTable = GameObject.Find("Game").GetComponent<GameLoop>().coordinateTable;
    }

    public List<Vector3> Search(Vector3 startingPosition, Vector3 destination)
    {
        this.startingPosition = startingPosition;
        previousCell = startingPosition;
        path = new List<Vector3>();
        visited = new List<Vector3>();
        queue = new Queue<Vector3>();
        backtrackingTable = new Dictionary<Vector3, Vector3>();
        found = false;

        queue.Enqueue(startingPosition);
        visited.Add(startingPosition);

        while (!found && queue.Count > 0)
        {
            currentCell = queue.Dequeue();
            Debug.Log("adding" + previousCell.x + ", " + previousCell.y + "and" + currentCell.x + ", " + currentCell.y);

            if (currentCell == destination)
            {
                Debug.Log("found");
                found = true;
            }
            else
            {
                List<Vector3> children = GenerateChildren(currentCell);
                int childrenCount = children.Count;

                foreach (Vector3 child in children)
                {
                    queue.Enqueue(child);
                    visited.Add(child);
                    backtrackingTable.Add(child, currentCell);
                }
            }
        }

        return BackTrack(currentCell);
    }

    public List<Vector3> GenerateChildren(Vector3 parent)
    {
        List<Vector3> childrenTemp = new List<Vector3>();

        MazeGenerator.Cell cell = (MazeGenerator.Cell)coordinateTable[parent];

        if (!cell.HasFlag(MazeGenerator.Cell.UP))
            childrenTemp.Add(new Vector3(parent.x, parent.y + 1, 0));
        if (!cell.HasFlag(MazeGenerator.Cell.LEFT))
            childrenTemp.Add(new Vector3(parent.x - 1, parent.y, 0));
        if (!cell.HasFlag(MazeGenerator.Cell.RIGHT))
            childrenTemp.Add(new Vector3(parent.x + 1, parent.y, 0));
        if (!cell.HasFlag(MazeGenerator.Cell.DOWN))
            childrenTemp.Add(new Vector3(parent.x, parent.y - 1, 0));

        List<Vector3> childrenFinal = childrenTemp;
        
        for (int i = childrenTemp.Count-1; i >= 0; i--)
        {
            if (visited.Contains(childrenTemp[i]))
            {
                childrenFinal.RemoveAt(i);
            }
        }

        return childrenFinal;
    }

    public List<Vector3> BackTrack(Vector3 currentCell)
    {
        List<Vector3> path = new List<Vector3>();

        while(currentCell != startingPosition)
        {
            path.Add(backtrackingTable[currentCell]);
            currentCell = backtrackingTable[currentCell];
        }

        path.Reverse();

        return path;
    }

    // public List<Vector3> BackTrack(Vector3[] queue)
    // {
    //     List<Vector3> path = new List<Vector3>();

    //     for (int i = 0; i < queue.Length-1; i++)
    //     {
    //         Vector3 movement = queue[i+1] - queue[i]; //this should convert the queue of positions into a list of movements
    //         path.Add(movement);
    //     }

    //     return path;
    // }
}