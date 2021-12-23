using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : IPathFinding
{
    private Transform hunterPrefab;
    private Transform preyPrefab;
    private System.Random random;
    // private float size;

    public RandomAI(int seed)
    {
        this.random = new System.Random(seed);
        // this.size = GameObject.Find("Game").GetComponent<GameLoop>().size;
    }

    public List<Vector3> Search(Vector3 startingPosition, Vector3 destination)
    {
        List<Vector3> randomPath = new List<Vector3>();

        for (int i = 0; i < 10; i++)
        {
            randomPath.Add(GenerateChildren(startingPosition)[0]);
            startingPosition = randomPath[i];
        }

        return randomPath;
    }

    public List<Vector3> GenerateChildren(Vector3 parent)
    {
        List<Vector3> path = new List<Vector3>();

        float rnd = random.Next(1, 5);

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
        else
        {
            path.Add(new Vector3(parent.x, parent.y - 1 , 0));
            return path;
        }
    }

    public List<Vector3> BackTrack(Vector3[] stack) //filler method, not necessary, implemented for interface reasons
    {
        return new List<Vector3>();
    }
}