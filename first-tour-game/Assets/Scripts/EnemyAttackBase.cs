using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    //[SerializeField] EnemyMovement movement;
    private void Start()
    {
        Debug.Log("Enemy attacking base!");
        GetComponent<BoxCollider2D>().enabled = true;
        

    }

    public void StopMovementNearBase()
    {
        GetComponent<EnemyMovement>().enabled = false;
    }
}
