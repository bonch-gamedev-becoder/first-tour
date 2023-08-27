using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Results : MonoBehaviour
{ 
    public static Results instance = null;

    public TextMeshProUGUI Score;
    public TextMeshProUGUI Place;


    private void Start()
    {
        if (instance = null)
            instance = this;

    }


}
