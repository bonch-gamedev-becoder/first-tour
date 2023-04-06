using UnityEngine;
using System.Collections.Generic;

public class Maze
{
    public MazeGeneratorCell[,] cells;
    public Vector2Int finishPosition;
    public List<MazeGeneratorCell> pathAStar = new List<MazeGeneratorCell>();
}

public class MazeGeneratorCell
{
    public int X;
    public int Y;


    public bool WallLeft = true;
    public bool WallBottom = true;
    public bool Visited = false;

    public bool VisitedAStar = false;
    public int DistanceFromStart;
    public float DistanceTillEnd;
    public float F;     //G + H, пройденный путь + расстояние до конца
    public MazeGeneratorCell parent;
}
