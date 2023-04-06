using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
   
    void Update()
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        transform.eulerAngles = new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(pos.y, pos.x));
    }
}
