using System.Collections.Generic;
using UnityEngine;

public class BreadthFirstAI : IPathFinding
{
    private MazeGenerator.Cell[,] maze;
    private Dictionary<Vector3, MazeGenerator.Cell> coordinateTable;
    private Vector3 currentCell, startingPosition;
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

            if (currentCell == destination)
            {
                found = true;
            }
            else
            {
                foreach (Vector3 child in GenerateChildren(currentCell))
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
}