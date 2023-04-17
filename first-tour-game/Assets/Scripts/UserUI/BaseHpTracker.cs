using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseHpTracker : MonoBehaviour
{
    public static BaseHpTracker instance = null;

    Text text;

    private void Start()
    {
        if (instance == null)
            instance = this;

        text = GetComponent<Text>();
        //ChangeText();
    }

    public void ChangeText()
    {
        text.text = "Base HP: " + GameManager.instance.currentBase.currentHealth;
    }
}
