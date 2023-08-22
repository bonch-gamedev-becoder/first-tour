using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Register : MonoBehaviour
{
    public static Register instance = null;

    private InputField Login;
    private InputField Info;


    private void Start()
    {
        if (instance == null)
            instance = this;

    }

    private void SavePlayerInfo()
    {
        PlayerPrefs.SetString("Login", Login.text);
        PlayerPrefs.SetString("Info", Info.text);

    }


}
