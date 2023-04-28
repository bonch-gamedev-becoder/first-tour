using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooperative : MonoBehaviour
{
    public static Cooperative instance;

    //[HideInInspector]
    public GameObject Player1;
    //[HideInInspector]
    public GameObject Player2;



    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void DisablePlayers()
    {
        Player1.SetActive(false);

        if (Player2 != null)
            Player2.SetActive(false);
    }

    public bool PlayersActive()
    {
        if (Player1.active)
            return true;

        return false;
    }


    public void SetControls()
    {
        Player1.GetComponent<PlayerCombat>().shootButton = KeyCode.Space;
        if (!Player1.TryGetComponent(out MoveWASD movewasd))
        {
            Player1.AddComponent<MoveWASD>();
            MoveWASD moveWASD = Player1.GetComponent<MoveWASD>();
            moveWASD.right = KeyCode.D;
            moveWASD.left = KeyCode.A;
            moveWASD.down = KeyCode.S;
            moveWASD.up = KeyCode.W;
        }

        if (Player2 != null)
        {
            Player2.AddComponent<MoveWASD>();
            MoveWASD moveWASD = Player2.GetComponent<MoveWASD>();
            moveWASD.right = KeyCode.RightArrow;
            moveWASD.left = KeyCode.LeftArrow;
            moveWASD.down = KeyCode.DownArrow;
            moveWASD.up = KeyCode.UpArrow;
            Player2.GetComponent<PlayerCombat>().shootButton = KeyCode.Return;
        }

        

        
    }
}
