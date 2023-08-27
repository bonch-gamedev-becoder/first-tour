using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : MonoBehaviour
{
    public TextMeshProUGUI Login;
    public TextMeshProUGUI Connect;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(RememberInfo);
    }

    void RememberInfo()
    {
        PlayerPrefs.SetString("Login", Login.text);
        PlayerPrefs.SetString("Connect", Connect.text);

        Debug.Log(Login.text);
        Debug.Log(Connect.text);
    }
}
