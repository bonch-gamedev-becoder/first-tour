using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Maze currentMaze;
    public BaseBehavior currentBase;

    [HideInInspector]
    public bool Coop;

    public GameObject PlayerPrefab;
    public GameObject Base;
    public GameObject enemySpawnPoints;
    public GameObject upgrades;
    public int difficulty;
    public int mazeCof = 8;
    public int level;
    public int points;
    public int Player1Score;
    public int Player2Score;
    public bool gameOver;


    private void Awake()
    {

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

        /*if (level > 1)
            difficulty = 3;
        if (level > 3)
            difficulty = 4;
        if (level > 6)
            difficulty = 6;
        if (level > 8)
            difficulty = 10;*/

        if (level > 1)
            difficulty = 2;
        if (level > 3)
            difficulty = 2;
        if (level > 6)
            difficulty = 3;
        if (level > 8)
            difficulty = 3;

        Debug.Log("difficulty=" + difficulty);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }

        if (Input.GetKeyDown(KeyCode.L) || PlayerPrefs.GetInt("Second") == 1)
        {
            if (Coop == false)
            SpawnSecondPlayer();
        }
    }



    public void CoreGameplay(Maze maze)
    {
        currentMaze = maze;

        SpawnBase();

        Instantiate(enemySpawnPoints, transform.position, Quaternion.identity);

        Instantiate(upgrades, transform.position, Quaternion.identity);

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (Cooperative.instance == null)
            return;

        Vector2 pos = new Vector2(currentMaze.finishPosition.x, currentMaze.finishPosition.y - 1);
        Cooperative.instance.Player1 = Instantiate(PlayerPrefab, pos, Quaternion.identity);
        Cooperative.instance.SetControls();
    }

    void SpawnSecondPlayer()
    {
        if (Cooperative.instance == null)
            return;

        Coop = true;
        PlayerPrefs.SetInt("Second", 1);
        Vector2 pos = new Vector2(currentMaze.finishPosition.x, currentMaze.finishPosition.y + 1);
        Cooperative.instance.Player2 = Instantiate(PlayerPrefab, pos, Quaternion.identity);
        Cooperative.instance.SetControls();
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

        if (points > level * 10)
            levelComplete();

        ScoreTracker.instance.ChangeText();
    }

    public void AddPointsToTheShooter(GameObject shooter)
    {
        if (shooter == null)
            return;

        if (shooter.TryGetComponent<Statistics>(out Statistics stat)) 
            stat.AddScore();  
        else 
            shooter.AddComponent<Statistics>().AddScore();
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
        PlayerPrefs.SetInt("previous", level);
        points = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void showStat()
    {
        if (gameOver)
            return;

        gameOver = true;
        Cooperative.instance.DisablePlayers();
        GameOver.instance.ShowGameOver();
    }
}
