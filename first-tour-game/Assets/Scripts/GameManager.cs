using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public int level;


    private void Awake()
    {
        if (instance == null)
            instance = this;

    }

}