using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MazeGenerator
{
    //public AStar aStar;
    public Button BackTracker;
    public Button RightHand;
    public int Width = GameManager.instance.mazeCof * GameManager.instance.difficulty;
    public int Height = GameManager.instance.mazeCof * GameManager.instance.difficulty;

    public Maze GenerateMaze()
    {
        MazeGeneratorCell[,] cells = new MazeGeneratorCell[Width, Height];

        for (int x = 0; x < cells.GetLength(0); x++)
        {
            for (int y = 0; y < cells.GetLength(1); y++)
            {
                cells[x, y] = new MazeGeneratorCell {X = x, Y = y};
            }
        }

        //Если стены выходят за пределы лабиринта, они удаляются
        for (int x = 0; x < cells.GetLength(0); x++)
        {
            cells[x, Height - 1].WallLeft = false;
        }

        for (int y = 0; y < cells.GetLength(1); y++)
        {
            cells[Width - 1, y].WallBottom = false;
        }


        RemoveWallsWithBacktracker(cells);

        Maze maze = new Maze();

        maze.cells = cells;
        maze.finishPosition = PlaceBase(maze.cells);
        GameManager.instance.CoreGameplay(maze);
        PlayerMovement.instance.transform.Translate(new Vector2(maze.finishPosition.x, maze.finishPosition.y - 1));


        //AStar aStar = new AStar(maze);

        return maze;
    }

    public void RemoveWallsWithBacktracker(MazeGeneratorCell[,] maze)
    {
        //Задаем начало лабиринта в левом нижнем углу
        MazeGeneratorCell current = maze[0, 0];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();
        do
        {
            List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].Visited) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].Visited) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].Visited) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].Visited) unvisitedNeighbours.Add(maze[x, y + 1]);

            if (unvisitedNeighbours.Count > 0)
            {
                MazeGeneratorCell chosen = unvisitedNeighbours[UnityEngine.Random.Range(0, unvisitedNeighbours.Count)];
                RemoveWall(current, chosen);

                chosen.Visited = true;
                stack.Push(chosen);
                chosen.DistanceFromStart = current.DistanceFromStart + 1;
                current = chosen;
            }
            else
            {
                current = stack.Pop();
            }

        } while (stack.Count > 0);
    }


    public static void MakeCellsArrUnvisited(MazeGeneratorCell[,] maze, int Width, int Height)
    {
        for (int i = 0; i < Width; i++)
        {
            for (int j = 0; j < Height; j++)
            {
                maze[i, j].Visited = false;
                maze[i, j].DistanceFromStart = 0;
            }
        }
    }

    public static void RegenerateBacktracker(MazeGeneratorCell[,] maze, int startX, int startY, int Width, int Height)
    {
        MazeGeneratorCell current = maze[startX, startY];
        current.Visited = true;
        current.DistanceFromStart = 0;

        Queue<MazeGeneratorCell> queue = new Queue<MazeGeneratorCell>();
       
        queue.Enqueue(current);

        while (queue.Count != 0)
        {
            current = queue.Dequeue();
            current.Visited = true;
            int x = current.X;
            int y = current.Y;
            if (x == 8 && y == 8) 
            {
                return;
            }
            Debug.Log("distance: " + current.DistanceFromStart);
            if (x > 0 && !maze[x - 1, y].Visited && !current.WallLeft)
            {
                //x--;
                
                    maze[x - 1, y].DistanceFromStart = current.DistanceFromStart + 1;
                    queue.Enqueue(maze[x - 1, y]);
                
            }

            if (y > 0 && !maze[x, y - 1].Visited && !current.WallBottom)
            {
                //y--; 
                
                    maze[x, y - 1].DistanceFromStart = current.DistanceFromStart + 1;
                    queue.Enqueue(maze[x, y - 1]);
                
                    
            }

            if (x < Width - 2 && !maze[x + 1, y].Visited && !maze[x + 1, y].WallLeft)
            {
                    //x++;
                    maze[x + 1, y].DistanceFromStart = current.DistanceFromStart + 1;
                    queue.Enqueue(maze[x + 1, y]);
                
            }

            if (y < Height - 2 && !maze[x, y + 1].Visited && !maze[x, y + 1].WallBottom)
            {
                    maze[x, y + 1].DistanceFromStart = current.DistanceFromStart + 1;
                    queue.Enqueue(maze[x, y + 1]);
                
            }
        }
    }

    private void RemoveWall(MazeGeneratorCell a, MazeGeneratorCell b)
    {
        if (a.X == b.X)
        {
            if (a.Y > b.Y) a.WallBottom = false;
            else b.WallBottom = false;
        }
        else
        {
            if (a.X > b.X) a.WallLeft = false;
            else b.WallLeft = false;
        }
    }

    private Vector2Int PlaceBase(MazeGeneratorCell[,] maze)
    {
        int X = Width / 2;
        int Y = Height / 2;

        int radious;
        if (GameManager.instance.difficulty < 4)
            radious = GameManager.instance.difficulty;
        else
            radious = GameManager.instance.difficulty / 2;

        int A = Mathf.Abs(X - radious);
        int B = X * 2 - A;

        Debug.Log("radious = " + radious);
        Debug.Log("A = " + A);
        Debug.Log("B = " + B);

        for (int x = 0; x < maze.GetLength(0); x++)
        {
            for (int y = 0; y < maze.GetLength(1); y++)
            {
                if (maze[x, y].X >= A && maze[x, y].X < B)
                {
                    if (maze[x,y].Y >= A && maze[x, y].Y < B)
                    {
                        maze[x, y].WallLeft = false;
                        maze[x, y].WallBottom = false;
                    }
                }
            }
        }
        return new Vector2Int(X, Y);
    }

    //private Vector2Int PlaceMazeExit(MazeGeneratorCell[,] maze)
    //{
    //    MazeGeneratorCell furthest = maze[0, 0];

    //    for (int x = 0; x < maze.GetLength(0); x++)
    //    {
    //        if (maze[x, Height - 2].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, Height - 2];
    //        if (maze[x, 0].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[x, 0];
    //    }

    //    for (int y = 0; y < maze.GetLength(1); y++)
    //    {
    //        if (maze[Width - 2, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[Width - 2, y];
    //        if (maze[0, y].DistanceFromStart > furthest.DistanceFromStart) furthest = maze[0, y];
    //    }

    //    if (furthest.X == 0) furthest.WallLeft = false;
    //    else if (furthest.Y == 0) furthest.WallBottom = false;
    //    else if (furthest.X == Width - 2) maze[furthest.X + 1, furthest.Y].WallLeft = false;
    //    else if (furthest.Y == Height - 2) maze[furthest.X, furthest.Y + 1].WallBottom = false;

    //    return new Vector2Int(furthest.X, furthest.Y);
    //}
}