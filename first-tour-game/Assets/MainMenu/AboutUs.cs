using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AboutUs : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(About);
    }

    void About()
    {
        Application.OpenURL("https://vk.com/@bonch.gamedev-informaciya-o-gamedev-kruzhke");
    }
}
