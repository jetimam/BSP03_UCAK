using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepthFirstAI : IPathFinding
{
    private MazeGenerator.Cell[,] maze;
    private Hashtable coordinateTable;
    private Vector3 currentCell;
    private List<Vector3> path;
    private List<Vector3> visited;
    private Stack<Vector3> frontier;
    private bool found;

    public DepthFirstAI()
    {
        this.maze = GameObject.Find("Game").GetComponent<GameLoop>().maze;
        this.coordinateTable = GameObject.Find("Game").GetComponent<GameLoop>().coordinateTable;
    }

    public List<Vector3> Search(Vector3 startingPosition, Vector3 destination)
    {
        path = new List<Vector3>();
        visited = new List<Vector3>();
        frontier = new Stack<Vector3>();
        found = false;

        frontier.Push(startingPosition);

        while (!found && frontier.Count > 0)
        {
            // Debug.Log("popping stack");
            currentCell = frontier.Pop();
            visited.Add(currentCell);

            if (currentCell == destination)
            {
                Debug.Log("found");
                found = true;
            }
            else 
            {
                // Debug.Log("generating children");
                List<Vector3> children = GenerateChildren(currentCell);
                // Debug.Log("generated children");
                if (children.Count < 0)
                {
                    // Debug.Log("backtracking");
                    path.RemoveAt(path.Count-1);
                    frontier.Push(path[path.Count-1]);
                    // Debug.Log("backtracked");
                }
                else
                {
                    foreach (Vector3 child in children)
                    {
                        frontier.Push(child);
                    }

                    path.Add(frontier.Peek());
                    // Debug.Log("pushed children on the stack");
                }
            }
        }

        return path;
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

        // foreach (Vector3 child in childrenTemp)
        // {
        //     if (visited.Contains(child))
        //     {
        //         childrenFinal.Remove(child);
        //     }
        // }
        
        for (int i = childrenTemp.Count-1; i >= 0; i--)
        {
            if (visited.Contains(childrenTemp[i]))
            {
                childrenFinal.RemoveAt(i);
            }
        }

        return childrenFinal;
    }

    public List<Vector3> BackTrack(Vector3[] stack)
    {
        List<Vector3> path = new List<Vector3>();

        for (int i = 0; i < stack.Length-1; i++)
        {
            Vector3 movement = stack[i+1] - stack[i]; //this should convert the stack of positions into a list of movements
            path.Add(movement);
        }

        return path;
    }
}

// while (stack.Count > 0 || !found)
// {
//     currentCell = stack.Pop();
//     visited.Add(currentCell);
//     path.Add(currentCell);
//     // Debug.Log("i am popping");

//     if (currentCell == destination)
//     {
//         // Debug.Log("i am finding the destination");
//         found = true;
//         // Debug.Log("i found the destination");
//         // path = BackTrack(stack.ToArray());
//     }
//     else
//     {
//         // Debug.Log("i am generating children");
//         List<Vector3> children = GenerateChildren(currentCell);

        
//         foreach (Vector3 child in children)
//         {
//             stack.Push(child);
//         }
//         stack.Push(new Vector3(startingPosition.x + 1, startingPosition.y, 0));
//     }
// }