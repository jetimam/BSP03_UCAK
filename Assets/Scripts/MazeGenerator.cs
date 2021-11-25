using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    private struct Position
    {
        public int X;
        public int Y;
    }

    private struct Neighbor //the shared wall attribute let's us delete the walls as we go through DFS
    {
        public Position Position;
        public Cell SharedWall;
    }

    [Flags] public enum Cell
    {
        LEFT = 1,    //0 0001
        RIGHT = 2,   //0 0010
        UP = 4,      //0 0100
        DOWN = 8,    //0 1000
        VISITED = 16 //1 0000
    }

    public static Cell[,] Generate(int width, int height)
    {
        Cell[,] maze = new Cell[width, height];
        Cell initial = Cell.RIGHT | Cell.LEFT | Cell.UP | Cell.DOWN; //all four sides have walls AKA 1111

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                maze[i,j] = initial;
            }
        }

        return DFS(maze, width, height);
    }

    private static Cell[,] DFS(Cell[,] maze, int width, int height)
    {
        Stack<Position> toVisit = new Stack<Position>();
        Neighbor currentCellAsNeighbor = new Neighbor{Position = new Position{X = 0, Y = 0}, SharedWall = Cell.RIGHT};
        System.Random random = new System.Random();

        Position initialPosition = new Position {X = random.Next(0, width), Y = random.Next(0, height)};

        maze[initialPosition.X, initialPosition.Y] |= Cell.VISITED; //because of this maze indexing i cant make the size of the paths dynamic, 
                                                                    //it makes the walls overlap with each other.
        toVisit.Push(initialPosition);

        int counter = 0;
        while(toVisit.Count > 0)
        {
            Position currentCell = toVisit.Pop();
            List<Neighbor> neighbors = GetUnvisited(currentCell, maze, width, height);

            if (neighbors.Count > 0)
            {
                toVisit.Push(currentCell);

                int randomNeighborIndex = random.Next(0,neighbors.Count);
                Neighbor randomNeighbor = neighbors[randomNeighborIndex]; //neighbor has a position and a sharedwall attribute
                Position randomNeighborPosition = randomNeighbor.Position;
                
                maze[currentCell.X, currentCell.Y] &= ~randomNeighbor.SharedWall; // removes the shared wall -> 1111 & ~0001 = 1110 (all walls except the shared one)
                maze[randomNeighborPosition.X, randomNeighborPosition.Y] &= ~GetOppositeWall(randomNeighbor.SharedWall);
                // ^^^^ need to do the same since each cell have their own walls, if we remove only one of the shared walls, the wall would still be there because of the neighbor cell
                //need to get opposite wall because (0,0)'s shared wall is the right wall, while (1,0)'s is the left wall
                
                maze[randomNeighborPosition.X, randomNeighborPosition.Y] |= Cell.VISITED; //0 XXXX | 1 0000 = 1 XXXX, meaning that the cell has been visited.

                currentCellAsNeighbor = randomNeighbor;
                toVisit.Push(randomNeighborPosition);
            }
            else
            {
                counter++;
                if (counter % 3 == 0)
                {
                    if (!(currentCell.X == 0 && currentCellAsNeighbor.SharedWall == Cell.LEFT ||
                        currentCell.Y == 0 && currentCellAsNeighbor.SharedWall == Cell.DOWN ||
                        currentCell.X == width-1 && currentCellAsNeighbor.SharedWall == Cell.RIGHT ||
                        currentCell.Y == height-1 && currentCellAsNeighbor.SharedWall == Cell.UP))
                    {
                        maze[currentCell.X, currentCell.Y] &= ~currentCellAsNeighbor.SharedWall;
                    }
                }
            }
        }

        return maze;
    }

    private static List<Neighbor> GetUnvisited(Position position, Cell[,] maze, int width, int height)
    {        
        List<Neighbor> neighbors = new List<Neighbor>();

        if (position.X > 0) //making sure we are not on the leftmost column
        {
            if (!maze[position.X - 1, position.Y].HasFlag(Cell.VISITED)) //if the cell to your left is not visited
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X - 1, Y = position.Y}, SharedWall = Cell.LEFT});
            }
        }
        if (position.Y > 0) //making sure we are not on the very bottom row
        {
            if (!maze[position.X, position.Y - 1].HasFlag(Cell.VISITED)) //if the cell below is not visited
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X, Y = position.Y - 1}, SharedWall = Cell.DOWN});
            }
        }
        if (position.Y < height - 1) //making sure we are not on the highest row
        {
            if (!maze[position.X, position.Y + 1].HasFlag(Cell.VISITED)) //if the cell above is not visited
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X, Y = position.Y + 1}, SharedWall = Cell.UP});
            }
        }
        if (position.X < width - 1) //making sure we are not on the rightmost column
        {
            if (!maze[position.X + 1, position.Y].HasFlag(Cell.VISITED)) //if the cell to your right is not visited
            {
                neighbors.Add(new Neighbor{Position = new Position{X = position.X + 1, Y = position.Y}, SharedWall = Cell.RIGHT});
            }
        }

        return neighbors;
    }

    private static Cell GetOppositeWall(Cell wall)
    {
        switch (wall)
        {
            case Cell.RIGHT:
                return Cell.LEFT;
            case Cell.LEFT:
                return Cell.RIGHT;
            case Cell.UP:
                return Cell.DOWN;
            case Cell.DOWN:
                return Cell.UP;
            default:
                return Cell.LEFT; //doesn't matter
        }
    }
}
