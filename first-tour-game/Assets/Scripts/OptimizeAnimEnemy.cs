using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptimizeAnimEnemy : MonoBehaviour
{
    private EnemyCombat enemyCombatScript; 
    void Start()
    {
        enemyCombatScript = GetComponent<EnemyCombat>();
    }

    private void OnBecameInvisible()
    {
        Debug.Log("camera out");
        enemyCombatScript.deathEffect = null;
    }

    private void OnBecameVisible()
    {
        Debug.Log("camera in");
        enemyCombatScript.deathEffect = enemyCombatScript.deathEffectPermanent;
    }
}   
