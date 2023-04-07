using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Maze currentMaze;

    public GameObject enemySpawnPoints;
    public int level;
    public int points;


    private void Awake()
    {
        if (instance == null)
            instance = this;

        
        level = PlayerPrefs.GetInt("level");
        Debug.Log("Level: " + level);
    }

    

    public void CoreGameplay(Maze maze)
    {
        currentMaze = maze;
        Instantiate(enemySpawnPoints, transform.position, Quaternion.identity);
    }

    public void AddPoints(int number)
    {
        points += number;

        if (points > level * 10)
            levelComplete();
    }

    void levelComplete()
    {
        PlayerPrefs.SetInt("level", level + 1);

        if (PlayerPrefs.GetInt("level") > PlayerPrefs.GetInt("maximum"))
            PlayerPrefs.SetInt("maximum", PlayerPrefs.GetInt("level"));

        Debug.Log("You completed " + level + " level!");
        loadNextLevel();
    }

    void loadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
