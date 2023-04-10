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
    public GameObject upgrades;
    public int difficulty;
    public int mazeCof = 8;
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

        difficultyScale();



    }

    private void difficultyScale()
    {
        Debug.Log("level = " + level);
        difficulty = 2;

        if (level > 1)
            difficulty = 3;
        if (level > 3)
            difficulty = 4;
        if (level > 6)
            difficulty = 6;
        if (level > 8)
            difficulty = 10;

        Debug.Log("difficulty=" + difficulty);
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

        Instantiate(enemySpawnPoints, transform.position, Quaternion.identity);

        Instantiate(upgrades, transform.position, Quaternion.identity);
    }

    void SpawnBase()
    {
        int num = difficulty * mazeCof/2;
        Vector2 pos = new Vector2(num, num);
        currentBase = Instantiate(Base, pos, Quaternion.identity).GetComponent<BaseBehavior>();
    }

    public void AddPoints(int number)
    {
        points += number;

        if (points > difficulty * 10)
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

    public void loadNextLevel()
    {
        points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
