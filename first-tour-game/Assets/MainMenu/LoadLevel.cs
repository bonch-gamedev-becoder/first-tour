using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public int levelNumber;
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartGame);
    }

    void StartGame()
    {
        if (PlayerPrefs.GetInt("maximum") > levelNumber || levelNumber == 1)
            PlayerPrefs.SetInt("level", levelNumber);
        else
            return;

        SceneManager.LoadScene("GameMap");
    }

}
