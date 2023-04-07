using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsGenerator : MonoBehaviour
{
    public Spawner spawner;

    public List<Vector2> spawnPoints = new List<Vector2>();

    void Start()
    {
        GetSpawnPositions();
        SetSpawners();
    }

    void GetSpawnPositions()
    {
        int num = GameManager.instance.level * 12 - 1;

        for (int x = 0; x < num; x++)
            for (int y = 0; y < num; y++)
                if (x == 0 || x == num || y == 0 || y == num) {
                    Vector2 vector = new(GameManager.instance.currentMaze.cells[x, y].X + 0.5f, GameManager.instance.currentMaze.cells[x, y].Y + 0.5f);
                    spawnPoints.Add(vector);
                }       
    }

    void SetSpawners()
    {
        for (int i = 0; i < GameManager.instance.level * 3; i++)
            SetSpawner();
        
    }

    void SetSpawner()
    {
        int randnum = Random.Range(0, spawnPoints.Count);
        Instantiate(spawner, spawnPoints[randnum], Quaternion.identity);
        spawnPoints.RemoveAt(randnum);
    }
}
