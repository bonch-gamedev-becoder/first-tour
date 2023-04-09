using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsGenerator : MonoBehaviour
{
    public Spawner spawner;

    public List<Vector2> spawnPoints = new List<Vector2>();
    public List<GameObject> currentSpawnPoints = new List<GameObject>();

    void Start()
    {
        ReloadSpawners();
        
    }

    public void ReloadSpawners()
    {
        DeleteOldPoints();
        GetSpawnPositions();
        SetSpawners();
        Debug.Log("Spawners reloaded.");
        StartCoroutine(WaitForReset());
    }

    public IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(20 / GameManager.instance.difficulty);
        ReloadSpawners();
    }

    private void DeleteOldPoints()
    {
        for (int i = 0; i < currentSpawnPoints.Count; i++)
        {
            Destroy(currentSpawnPoints[i].gameObject);
        }
    }

    void GetSpawnPositions()
    {
        int num = GameManager.instance.difficulty * GameManager.instance.mazeCof - 1;

        for (int x = 0; x < num; x++)
            for (int y = 0; y < num; y++)
                if (y % num == 0 || x % num == 0) {
                    Vector2 vector = new(GameManager.instance.currentMaze.cells[x, y].X + 0.5f, GameManager.instance.currentMaze.cells[x, y].Y + 0.5f);
                    spawnPoints.Add(vector);
                }       
    }

    void SetSpawners()
    {
        for (int i = 0; i < GameManager.instance.difficulty; i++)
            SetSpawner();
        
    }

    void SetSpawner()
    {
        int randnum = Random.Range(0, spawnPoints.Count);
        GameObject obj = Instantiate(spawner, spawnPoints[randnum], Quaternion.identity).gameObject;
        currentSpawnPoints.Add(obj);
        spawnPoints.RemoveAt(randnum);
    }
}
