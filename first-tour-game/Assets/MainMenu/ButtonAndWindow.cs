using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonAndWindow : MonoBehaviour
{
    public Image Window;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Levels);
    }

    void Levels()
    {
        if (Window.transform.localScale.x == 1)
            ShowWindowService.instance.Hide(Window.gameObject, 0.5f);
        
        else
            ShowWindowService.instance.Show(Window.gameObject, 0.5f);
    }

    
}
