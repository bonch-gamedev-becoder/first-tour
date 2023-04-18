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

    Statistics First;
    Statistics Second;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void ShowGameOver()
    {
        getStat();

        LeanTween.scale(gameObject, Vector3.one, 0.5f).setEaseInElastic();
        player1.text = "Score: " + First.score;

        if (Second != null)
        player2.text = "Score: " + Second.score;
    }

    void getStat()
    {
       First = Cooperative.instance.Player1.GetComponent<Statistics>();

        if (Cooperative.instance.Player2 != null)
            Second = Cooperative.instance.Player2.GetComponent<Statistics>();
    }


}
