using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradesSpawn : MonoBehaviour
{

    public List<GameObject> upgrades;

    private void Start()
    {
        if (tag != "JokerEnemy")
        {
            SpawnRandomUpgrade();
        }
    }

    public void SpawnRandomUpgrade()
    {
        for (int x = 0; x < GameManager.instance.difficulty * GameManager.instance.mazeCof - 1; x++)
        {
            for (int y = 0; y < GameManager.instance.difficulty * GameManager.instance.mazeCof - 1; y++)
            {
                //upgrades should not spawn near base
                if (MazeSpawner.instance != null || tag == "JokerEnemy")
                {
                    int randnum = Random.Range(0, 40);
                    if (randnum == 1)
                        SpawnUpgrade(x, y);
                }
            }
        }
    }

    public void SpawnUpgrade(int x, int y)
    {
        Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
        int randUpgrade = Random.Range(0, upgrades.Count);
        if (randUpgrade == upgrades.Count-1)
        {
            int portalfix = Random.Range(0, 10);
            if (portalfix == 1)
                return;
        }

        Instantiate(upgrades[randUpgrade], pos, Quaternion.identity);
    }
}
