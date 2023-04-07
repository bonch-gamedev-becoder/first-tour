using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesSpawn : MonoBehaviour
{
    public static UpgradesSpawn instance = null;

    public List<GameObject> upgrades;

    private void Start()
    {
        if (instance == null)
            instance = this;

    }

    public void SpawnRandomUpgrade()
    {
        for (int x = 0; x < GameManager.instance.level * 12 - 1; x++)
        {
            for (int y = 0; y < GameManager.instance.level * 12 - 1; y++)
            {
                int randnum = Random.Range(0, 10);
                if (randnum == 1)
                    SpawnUpgrade(x,y);
            }
        }
    }

    void SpawnUpgrade(int x, int y)
    {
        Vector2 pos = new Vector2(x + 0.5f, y + 0.5f);
        int randUpgrade = Random.Range(0, upgrades.Count);
        Instantiate(upgrades[randUpgrade], pos, Quaternion.identity);
    }
}
