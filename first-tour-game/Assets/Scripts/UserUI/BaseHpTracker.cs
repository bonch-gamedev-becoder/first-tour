using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BaseHpTracker : MonoBehaviour
{
    public static BaseHpTracker instance = null;

    Image Line;

    private void Start()
    {
        if (instance == null)
            instance = this;

        Line = GetComponent<Image>();
        //ChangeText();
    }

    

    public void ChangeHealthLine()
    {
      
        Line.fillAmount = BaseBehavior.instance.currentHealth / BaseBehavior.instance.maxHealth;
        Debug.Log(Line.fillAmount);
    }
}
