using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public Vector3 dir;
    private Transform target;
    private void Awake()
    {
        Debug.Log("Enemy attacking base!");
        GetComponent<BoxCollider2D>().enabled = true;

        target = GameObject.FindWithTag("Base").transform;
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle + 180f);

        GetComponentInParent<EnemyCombat>().enabled = true;
    }

    public void StopMovementNearBase()
    {
        GetComponent<EnemyMovement>().enabled = false;
    }
}
