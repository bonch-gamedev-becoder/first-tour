using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ToMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(GoToMenu);
    }

    void GoToMenu()
    {
        PlayerPrefs.SetInt("Second", 0);
        SceneManager.LoadScene("MainMenu");
    }
}
