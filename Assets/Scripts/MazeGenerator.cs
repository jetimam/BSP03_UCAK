using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Flags] public enum WallState
    {
        LEFT = 1,   //0001
        RIGHT = 2,  //0010
        UP = 4,     //0100
        DOWN = 8,   //1000
        VISITED = 128
    }

    public static WallState[,] DFS(WallState[,] maze)
    {
        Stack<WallState[,]> visited = new Stack<WallState[,]>();
        return maze;
    }

    public static WallState[,] Generate(int width, int height)
    {
        WallState[,] maze = new WallState[width, height];
        WallState initial = WallState.RIGHT | WallState.LEFT | WallState.UP | WallState.DOWN; //all four sides have walls
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i,j] = initial;
            }
        }
        return maze;
    }
}
