using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Maze currentMaze;
    public BaseBehavior currentBase;

    public GameObject Base;
    public GameObject enemySpawnPoints;
    public int level;
    public int points;


    private void Awake()
    {
        //DontDestroyOnLoad(this);

        if (instance == null)
            instance = this;

        
        level = PlayerPrefs.GetInt("level");

        if (level == 0)
            level = 1;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }



    public void CoreGameplay(Maze maze)
    {
        currentMaze = maze;

        SpawnBase();
        //UpgradesSpawn.instance.SpawnRandomUpgrade();

        Instantiate(enemySpawnPoints, transform.position, Quaternion.identity);
    }

    void SpawnBase()
    {
        int num = level * 12 / 2;
        Vector2 pos = new Vector2(num, num);
        currentBase = Instantiate(Base, pos, Quaternion.identity).GetComponent<BaseBehavior>();
    }

    public void AddPoints(int number)
    {
        points += number;

        if (points > level * 10 * 1.5f)
            levelComplete();
    }

    void levelComplete()
    {
        level += 1;
        PlayerPrefs.SetInt("level", level);

        if (level > PlayerPrefs.GetInt("maximum"))
            PlayerPrefs.SetInt("maximum", level);

        Debug.Log("You completed " + level + " level!");
        loadNextLevel();
    }

    void loadNextLevel()
    {
        points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
