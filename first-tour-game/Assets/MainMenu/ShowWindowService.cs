using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWindowService : MonoBehaviour
{
    public static ShowWindowService instance;

    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void Show(GameObject window, float speed)
    {
        LeanTween.scale(window, Vector2.one, 0.5f).setEaseInExpo();
    }

    public void Hide(GameObject window, float speed)
    {
        LeanTween.scale(window, Vector2.zero, 0.5f).setEaseOutExpo();
    }
}
