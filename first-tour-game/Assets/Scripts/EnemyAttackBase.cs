using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    public Vector3 dir;
    private Transform target;

    //use awake function instead of start to fix bug (enable script "enemy combat")
    private void Awake()
    {
//        Debug.Log("Enemy attacking base!");
        GetComponent<BoxCollider2D>().enabled = true;

        if (tag == "HunterEnemy")
        {
                target = Cooperative.instance.Player1.transform;
        }
        else
        {
            if (GameManager.instance.currentBase != null)
            target = GameManager.instance.currentBase.transform;
        }

        
        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);

        GetComponentInParent<EnemyCombat>().enabled = true;
    }

    private void Update()
    {
        if (target == null)
            return;

        dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    /*public void StopMovementNearBase()
    {
        GetComponent<EnemyMovement>().enabled = false;
    }*/
}
