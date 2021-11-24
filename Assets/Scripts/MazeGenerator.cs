using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public struct Position
    {
        public int X;
        public int Y;
    }

    public struct Neighbor
    {
        public Position Position;
        public WallState SharedWall;
    }

    [Flags] public enum WallState
    {
        LEFT = 1,   //0001
        RIGHT = 2,  //0010
        UP = 4,     //0100
        DOWN = 8,   //1000
        VISITED = 128
    }

    public static WallState[,] DFS(WallState[,] maze, int width, int height)
    {
        Stack<Position> toVisit = new Stack<Position>();
        System.Random random = new System.Random();

        Position position = new Position {X = random.Next(0, width), Y = random.Next(0, height)};

        maze[position.X, position.Y] |= WallState.VISITED;
        toVisit.Push(position);

        while(toVisit.Count > 0)
        {
            Position currentCell = toVisit.Pop();
            List<Neighbor> neighbors = GetUnvisited(currentCell, maze, width, height);

            if (neighbors.Count > 0)
            {
                toVisit.Push(currentCell);

                int randomNeighborIndex = random.Next(0,neighbors.Count);
                Neighbor randomNeighbor = neighbors[randomNeighborIndex];

                Position randomNeighborPosition = randomNeighbor.Position;
                maze[currentCell.X, currentCell.Y] &= ~randomNeighbor.SharedWall;
                maze[randomNeighborPosition.X, randomNeighborPosition.Y] &= ~GetOppositeWall(randomNeighbor.SharedWall);
                maze[randomNeighborPosition.X, randomNeighborPosition.Y] |= WallState.VISITED;

                toVisit.Push(randomNeighborPosition);
            }
        }

        return maze;
    }

    public static WallState GetOppositeWall(WallState wall)
    {
        switch (wall)
        {
            case WallState.RIGHT:
                return WallState.LEFT;
            case WallState.LEFT:
                return WallState.RIGHT;
            case WallState.UP:
                return WallState.DOWN;
            case WallState.DOWN:
                return WallState.UP;
            default:
                return WallState.LEFT; //doesn't matter
        }
    }

    public static List<Neighbor> GetUnvisited(Position position, WallState[,] maze, int width, int height)
    {        
        List<Neighbor> neighbors = new List<Neighbor>();

        if (position.X > 0) //left
        {
            if (!maze[position.X - 1, position.Y].HasFlag(WallState.VISITED))
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X - 1, Y = position.Y}, SharedWall = WallState.LEFT});
            }
        }
        if (position.Y > 0) //down
        {
            if (!maze[position.X, position.Y - 1].HasFlag(WallState.VISITED))
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X, Y = position.Y - 1}, SharedWall = WallState.DOWN});
            }
        }
        if (position.Y < height - 1) //up
        {
            if (!maze[position.X, position.Y + 1].HasFlag(WallState.VISITED))
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X, Y = position.Y + 1}, SharedWall = WallState.UP});
            }
        }
        if (position.X < width - 1) //right
        {
            if (!maze[position.X + 1, position.Y].HasFlag(WallState.VISITED))
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X + 1, Y = position.Y}, SharedWall = WallState.RIGHT});
            }
        }

        return neighbors;
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
        return DFS(maze, width, height);
    }
}
