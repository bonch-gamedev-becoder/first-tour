using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTracker : MonoBehaviour
{
    public static ScoreTracker instance = null;

    Text text;

    private void Start()
    {
        if (instance == null)
            instance = this;

        text = GetComponent<Text>();
    }

    public void ChangeText()
    {
        text.text = "Score: " + GameManager.instance.points;
    }
}
