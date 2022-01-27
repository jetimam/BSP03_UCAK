using System.Collections.Generic;
using UnityEngine;

public class RandomAI : IPathFinding
{
    private System.Random random;
    private Dictionary<Vector3, MazeGenerator.Cell> coordinateTable;

    public RandomAI(int seed)
    {
        this.random = new System.Random(seed);
        this.coordinateTable = GameObject.Find("Game").GetComponent<GameLoop>().coordinateTable;
    }

    public List<Vector3> Search(Vector3 startingPosition, Vector3 destination)
    {
        List<Vector3> randomPath = new List<Vector3>();

        randomPath.Add(GenerateChildren(startingPosition)[0]);

        return randomPath;
    }

    public List<Vector3> GenerateChildren(Vector3 parent)
    {        
        List<Vector3> path = new List<Vector3>();
        List<int> possibleChildren = new List<int>();

        MazeGenerator.Cell cell = (MazeGenerator.Cell)coordinateTable[parent];

        for (int i = 0; i < 4; i++)
        {
            if (!cell.HasFlag(MazeGenerator.Cell.RIGHT))
                possibleChildren.Add(1);
            if (!cell.HasFlag(MazeGenerator.Cell.LEFT))
                possibleChildren.Add(2);
            if (!cell.HasFlag(MazeGenerator.Cell.UP))
                possibleChildren.Add(3);
            if (!cell.HasFlag(MazeGenerator.Cell.DOWN))
                possibleChildren.Add(4);
        }

        int rnd = random.Next(0, possibleChildren.Count);

        rnd = possibleChildren[rnd];

        if (rnd == 1)
        {
            path.Add(new Vector3(parent.x + 1, parent.y, 0));
            return path;
        }
        else if (rnd == 2)
        {
            path.Add(new Vector3(parent.x - 1, parent.y, 0));
            return path;
        }
        else if (rnd == 3)
        {
            path.Add(new Vector3(parent.x, parent.y + 1 , 0));
            return path;
        }
        else if (rnd == 4)
        {
            path.Add(new Vector3(parent.x, parent.y - 1 , 0));
            return path;
        }

        return path;
    }

    public List<Vector3> BackTrack(Vector3 cell) //filler method, not necessary, implemented for the interface
    {
        return new List<Vector3>();
    }
}