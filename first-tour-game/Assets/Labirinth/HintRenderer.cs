﻿using System.Collections.Generic;
using UnityEngine;

public class HintRenderer : MonoBehaviour
{
    public Vector2 finishPosition;

    public List<Vector3> positions = new List<Vector3>();

    public void DrawPath(Vector2 spawnPos, GameObject prefabDebug)
    {
        positions.Clear();
        GameObject mazeGameobject = GameObject.FindGameObjectWithTag("Maze");
        MazeSpawner MazeSpawner = mazeGameobject.GetComponent<MazeSpawner>();
        Maze maze = MazeSpawner.maze;

        int ceilSpawnPosX = Mathf.CeilToInt(spawnPos.x);
        int ceilSpawnPosY = Mathf.CeilToInt(spawnPos.y);

        if (ceilSpawnPosX < 0 || ceilSpawnPosX == maze.cells.GetLength(0) - 1) ceilSpawnPosX = 0;
        if (ceilSpawnPosY < 0 || ceilSpawnPosY == maze.cells.GetLength(1) - 1) ceilSpawnPosY = 0;


        MazeGenerator.MakeCellsArrUnvisited(maze.cells, maze.cells.GetLength(0), maze.cells.GetLength(1));
        MazeGenerator.RegenerateBacktracker(maze.cells, ceilSpawnPosX, ceilSpawnPosY, maze.cells.GetLength(0), maze.cells.GetLength(1), finishPosition);
        int x = Mathf.CeilToInt(finishPosition.x);
        int y = Mathf.CeilToInt(finishPosition.y);

        while ((x != ceilSpawnPosX || y != ceilSpawnPosY) && positions.Count < 10000)
        {
            if (positions.Count > 100)
            {
                Debug.Log("coords: " + ceilSpawnPosX + ", " + ceilSpawnPosY);
                return;
            }
            //Debug.Log("ceil: " + ceilSpawnPosX + ", " + ceilSpawnPosY + ". without ceil: " + spawnPos.x + ", " + spawnPos.y);
            positions.Add(new Vector3(x * MazeSpawner.CellSize.x, y * MazeSpawner.CellSize.y, y * MazeSpawner.CellSize.z));

            MazeGeneratorCell currentCell = maze.cells[x, y];

            if (x > 0 && (!currentCell.WallLeft) && (maze.cells[x - 1, y].DistanceFromStart == currentCell.DistanceFromStart - 1))
            {
                x--;
            }
            else if (y > 0 && (!currentCell.WallBottom) && (maze.cells[x, y - 1].DistanceFromStart == currentCell.DistanceFromStart - 1))
            {
                y--;
            }
            else if (x < maze.cells.GetLength(0) - 1 && (!maze.cells[x + 1, y].WallLeft) && (maze.cells[x + 1, y].DistanceFromStart == currentCell.DistanceFromStart - 1))
            {
                x++;
            }
            else if (y < maze.cells.GetLength(1) - 1 && (!maze.cells[x, y + 1].WallBottom) && (maze.cells[x, y + 1].DistanceFromStart == currentCell.DistanceFromStart - 1))
            {
                y++;
            }
            if (prefabDebug != null)
            {
                Instantiate(prefabDebug, new Vector2(x, y), Quaternion.identity);
            }
            
        }
        positions.Add(new Vector2(ceilSpawnPosX, ceilSpawnPosY));
        /*componentLineRenderer.positionCount = positions.Count;
        componentLineRenderer.SetPositions(positions.ToArray());*/
    }
}
