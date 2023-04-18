using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public static GameOver instance;

    public Text player1;
    public Text player2;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowGameOver()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseInElastic();
        player1.text = "Score: " + GameManager.instance.points;
    }


}
