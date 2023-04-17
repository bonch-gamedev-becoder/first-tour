using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooperative : MonoBehaviour
{
    public static Cooperative instance;

    [HideInInspector]
    public GameObject Player1;
    [HideInInspector]
    public GameObject Player2;



    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


    public void SetControls()
    {
        if (!Player1.TryGetComponent(out MoveWASD movewasd))
            Player1.AddComponent<MoveWASD>();


        if (Player2 != null)
            Player2.AddComponent<MoveArrows>();
        
    }
}
