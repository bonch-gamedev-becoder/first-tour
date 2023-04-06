using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleEffect : MonoBehaviour
{

    public float speed;
    public float pause;
    public float minSize;

    void Start()
    {
        StartCoroutine(Scale());
    }

    private IEnumerator Scale()
    {
        while (true)
        {
            yield return new WaitForSeconds(speed + pause);

                LeanTween.scaleX(gameObject, minSize, speed);
                LeanTween.scaleY(gameObject, minSize, speed);

            yield return new WaitForSeconds(pause + speed);

                LeanTween.scaleX(gameObject, 1f, speed);
                LeanTween.scaleY(gameObject, 1f, speed);
        }
    }

}
