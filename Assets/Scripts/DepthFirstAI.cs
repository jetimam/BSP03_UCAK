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
    private Stack<Vector3> stack;
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
        stack = new Stack<Vector3>();
        found = false;

        stack.Push(startingPosition);
        path.Add(startingPosition);

        while (!found && stack.Count > 0)
        {
            currentCell = stack.Pop();
            // Debug.Log("at cell: " + currentCell.x + " " + currentCell.y);
            visited.Add(currentCell);

            if (currentCell == destination)
            {
                Debug.Log("found");
                found = true;
            }
            else 
            {
                List<Vector3> children = GenerateChildren(currentCell);
                if (children.Count <= 0)
                {
                    path.RemoveAt(path.Count-1);

                    if (path.Count != 0)
                    {
                        stack.Push(path[path.Count-1]);
                        // Debug.Log("going back to:" + path[path.Count-1].x + " " + path[path.Count-1].y);
                    }
                }
                else
                {
                    foreach (Vector3 child in children)
                    {
                        // Debug.Log("generating children: " + child.x + " " + child.y);
                        stack.Push(child);
                    }

                    path.Add(stack.Peek());
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