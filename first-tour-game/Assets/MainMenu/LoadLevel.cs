using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int levelNumber;
    public bool previous;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (previous)
        {
            SceneManager.LoadScene("GameMap");
            return;
        }


        if (PlayerPrefs.GetInt("maximum") >= levelNumber)
            PlayerPrefs.SetInt("level", levelNumber);
        else
            return;

        SceneManager.LoadScene("GameMap");
    }

}
