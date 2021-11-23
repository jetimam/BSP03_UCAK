using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePrinter : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float size = 1f;
    public Transform wallPrefab = null;

    void Start()
    {
        var maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    void Update() {}

    public void Draw(MazeGenerator.WallState[,] maze)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var cell = maze[i,j];
                var position = new Vector3(-width/2 + i, -height/2 + j, 0);

                if (cell.HasFlag(MazeGenerator.WallState.UP))
                {
                    var topWall = Instantiate(wallPrefab, transform) as Transform;
                    topWall.position = position + new Vector3(0, size/2, 0);
                    topWall.localScale = new Vector3(size, topWall.localScale.x, topWall.localScale.y);
                }
                if (cell.HasFlag(MazeGenerator.WallState.LEFT))
                {
                    var leftWall = Instantiate(wallPrefab, transform) as Transform;
                    leftWall.position = position + new Vector3(-size/2, 0, 0);
                    leftWall.localScale = new Vector3(size, leftWall.localScale.x, leftWall.localScale.y);
                    leftWall.eulerAngles = new Vector3(0, 0, 90);
                }

                if (i == width - 1)
                {
                    if (cell.HasFlag(MazeGenerator.WallState.RIGHT))
                    {
                        var rightWall = Instantiate(wallPrefab, transform) as Transform;
                        rightWall.position = position + new Vector3(size/2, 0, 0);
                        rightWall.localScale = new Vector3(size, rightWall.localScale.x, rightWall.localScale.y);
                        rightWall.eulerAngles = new Vector3(0, 0, 90);
                    }
                }
                if (j == 0)
                {
                    if (cell.HasFlag(MazeGenerator.WallState.DOWN))
                    {
                        var bottomWall = Instantiate(wallPrefab, transform) as Transform;
                        bottomWall.position = position + new Vector3(0, -size/2, 0);
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.x, bottomWall.localScale.y);
                    }
                }
            }
        }
    }
}
