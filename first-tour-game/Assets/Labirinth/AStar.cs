using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AStar : MonoBehaviour
{
    public EnemyMovement enemy;

    public MazeGeneratorCell[,] maze;
    List<MazeGeneratorCell> unvisitedNeighbours = new List<MazeGeneratorCell>();

    MazeGeneratorCell current = null;
    MazeGeneratorCell next = null;
    MazeGeneratorCell MinimumCell = null;
    float MinF = 999;

    public Maze MyMaze;

    public int Width = 50;
    public int Height = 50;

    int k;
    int l;

    //Передаем сюда лабиринт, сгенерированный BackTracker
    public AStar(Maze Maze)
    {
        MyMaze = Maze;
        maze = MyMaze.cells;
        current = maze[0, 0];
        current.DistanceFromStart = 0;
        Vector2 currentPos = new Vector2(current.X, current.Y);
        current.DistanceTillEnd = Vector2.Distance(currentPos, MyMaze.finishPosition);
        current.F = current.DistanceFromStart + current.DistanceTillEnd;
        AStarAlgorithm();
    }

    public void AStarAlgorithm()
    {
            current.VisitedAStar = true;
            unvisitedNeighbours.Remove(current);

            int x = current.X;
            int y = current.Y;

            if (x > 0 && !maze[x - 1, y].VisitedAStar && !current.WallLeft) unvisitedNeighbours.Add(maze[x - 1, y]);
            if (y > 0 && !maze[x, y - 1].VisitedAStar && !current.WallBottom) unvisitedNeighbours.Add(maze[x, y - 1]);
            if (x < Width - 2 && !maze[x + 1, y].VisitedAStar && !maze[x + 1, y].WallLeft) unvisitedNeighbours.Add(maze[x + 1, y]);
            if (y < Height - 2 && !maze[x, y + 1].VisitedAStar && !maze[x, y + 1].WallBottom) unvisitedNeighbours.Add(maze[x, y + 1]);

        MinF = 999;
        for (int i = 0; i < unvisitedNeighbours.Count; i++)
            {
                next = unvisitedNeighbours[i];

                if (next.DistanceFromStart == 0)
                next.DistanceFromStart = current.DistanceFromStart + 1;

                next.DistanceTillEnd = Vector2.Distance(new Vector2(next.X, next.Y), MyMaze.finishPosition);
                next.F = next.DistanceFromStart + next.DistanceTillEnd;
                if (next.F < MinF)
                {
                    MinF = next.F;
                    MinimumCell = unvisitedNeighbours[i];
                }
            }


            MinimumCell.parent = current;
            current = MinimumCell;
            MyMaze.pathAStar.Add(current);
            
            Debug.Log("("+ current.X + "," + current.Y + ")");
            Debug.Log("Расстояние от старта:" + current.DistanceFromStart + "\t" + "Расстояние до конца: "
                + current.DistanceTillEnd + "\t" + "F: " + current.F);
        k++;

        if (current.DistanceTillEnd != 0)
            AStarAlgorithm();
        else
            return;
}

    //private IEnumerator MoveOnPath()
    //{
    //    yield return new WaitForSeconds(0.2f);
    //    Vector2 pos = new Vector2(MyMaze.pathAStar[l].X, MyMaze.pathAStar[l].Y);
    //    player.MoveTo(pos);
    //    l++;

    //    if (l != k)
    //        StartCoroutine(MoveOnPath());
            
    //}

    


     
    
}
