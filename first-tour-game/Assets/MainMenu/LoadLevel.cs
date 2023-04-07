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
        PlayerPrefs.SetInt("level", levelNumber);
        SceneManager.LoadScene("GameMap");
    }

}
