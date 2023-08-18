using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPointsGenerator : MonoBehaviour
{
    public List<Spawner> spawners;

    [HideInInspector]
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
//        Debug.Log("Spawners reloaded.");
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
//        Debug.Log("mazecof = " + GameManager.instance.mazeCof);
        //Debug.Log("difficulty = " + GameManager.instance.difficulty);
        int num = GameManager.instance.difficulty * GameManager.instance.mazeCof - 1;

        float baseX = num / 2 + 0.5f;
        float baseY = num / 2 + 0.5f;

        //Debug.Log("normalX = " + baseX);
        //Debug.Log("normalY = " + baseY);

        for (int x = 0; x < num; x++)
            for (int y = 0; y < num; y++)
                if (Mathf.Abs(x - baseX) > GameManager.instance.difficulty + 2) {
                    if (Mathf.Abs(x - baseY) > GameManager.instance.difficulty + 2)
                    {
}                        Vector2 vector = new Vector3(GameManager.instance.currentMaze.cells[x, y].X + 0.5f, GameManager.instance.currentMaze.cells[x, y].Y + 0.5f);
                        spawnPoints.Add(vector);
                    }
                }       
    

    void SetSpawners()
    {
        for (int i = 0; i < GameManager.instance.difficulty * 3; i++)
            SetSpawner();
        
    }

    void SetSpawner()
    {
        int randnumChance = Random.Range(0, 100);
        int randnum = Random.Range(0, spawnPoints.Count - 1);
        int randSpawner = 0;

        if (randnumChance >= 0 && randnumChance <= 40)
        {
            randSpawner = 0;
        }
        else if (randnumChance >= 41 && randnumChance <= 70)
        {
            randSpawner = 1;
        }
        else if (randnumChance >= 71 && randnumChance <= 90)
        {
            randSpawner = 2;
        }
        else if (randnumChance >= 91 && randnumChance <= 100)
        {
            randSpawner = 3;
        }
        GameObject obj = Instantiate(spawners[randSpawner], spawnPoints[randnum], Quaternion.identity).gameObject;
        currentSpawnPoints.Add(obj);
        spawnPoints.RemoveAt(randnum);
    }
}
