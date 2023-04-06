using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Enemy attacking base!");
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
