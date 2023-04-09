using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollisionArtillery : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Collider2D test = GetComponent<Collider2D>();
        Collider2D test2 = bulletPrefab.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(test, test2, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
