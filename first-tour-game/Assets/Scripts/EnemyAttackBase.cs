using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBase : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("Enemy attacking base!");
<<<<<<< Updated upstream
=======
        GetComponent<BoxCollider2D>().enabled = true;
        /*GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        GetComponent<EnemyMovement>().enabled = false;*/
>>>>>>> Stashed changes
    }
}
