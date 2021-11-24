using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazePrinter : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float size;
    [SerializeField] private Transform wallPrefab;

    void Start()
    {
        MazeGenerator.Cell[,] maze = MazeGenerator.Generate(width, height);
        Draw(maze);
    }

    void Update() {}

    public void Draw(MazeGenerator.Cell[,] maze)
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                MazeGenerator.Cell cell = maze[i,j];
                Vector3 position = new Vector3(-width/2 + i, -height/2 + j, 0);

                if (cell.HasFlag(MazeGenerator.Cell.UP))
                {
                    Transform topWall = Instantiate(wallPrefab, transform);  //place wall on the game scene
                    topWall.position = position + new Vector3(0, size/2, 0); //choose the location of the wall
                    topWall.localScale = new Vector3(size, topWall.localScale.x, topWall.localScale.y); //the size of the wall
                }
                if (cell.HasFlag(MazeGenerator.Cell.LEFT))
                {
                    Transform leftWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                    leftWall.position = position + new Vector3(-size/2, 0, 0); //choose the location of the wall
                    leftWall.localScale = new Vector3(size, leftWall.localScale.x, leftWall.localScale.y); //the size of the wall
                    leftWall.eulerAngles = new Vector3(0, 0, 90); //flip the horizontal walls
                }

                if (i == width - 1) //for the very far right of the grid
                {
                    if (cell.HasFlag(MazeGenerator.Cell.RIGHT))
                    {
                        Transform rightWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                        rightWall.position = position + new Vector3(size/2, 0, 0); //choose the location of the wall
                        rightWall.localScale = new Vector3(size, rightWall.localScale.x, rightWall.localScale.y); //the size of the wall
                        rightWall.eulerAngles = new Vector3(0, 0, 90); //flip the horizontal walls
                    }
                }
                if (j == 0)
                {
                    if (cell.HasFlag(MazeGenerator.Cell.DOWN)) //for the very bottom of the grid
                    {
                        Transform bottomWall = Instantiate(wallPrefab, transform); //place wall on the game scene
                        bottomWall.position = position + new Vector3(0, -size/2, 0); //choose the location of the wall
                        bottomWall.localScale = new Vector3(size, bottomWall.localScale.x, bottomWall.localScale.y); //the size of the wall
                    }
                }
            }
        }
    }
}
