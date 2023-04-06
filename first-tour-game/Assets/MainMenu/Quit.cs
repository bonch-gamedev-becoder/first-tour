using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quit : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(Application.Quit);
    }
}
