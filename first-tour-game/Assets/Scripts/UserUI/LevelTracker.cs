using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker instance = null;

    Text text;

    private void Start()
    {
        if (instance == null)
            instance = this;

        text = GetComponent<Text>();
        ChangeText();
    }

    public void ChangeText()
    {
        text.text = "Level: " + GameManager.instance.level;
    }
}
