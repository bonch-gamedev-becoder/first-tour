using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Maze currentMaze;

    public GameObject enemySpawnPoints;
    public int level;


    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void CoreGameplay(Maze maze)
    {
        currentMaze = maze;
        Instantiate(enemySpawnPoints, transform.position, Quaternion.identity);
    }

}
