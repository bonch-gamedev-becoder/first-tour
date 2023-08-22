using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TableUI : MonoBehaviour
{
    public static TableUI instance;



    private void Start()
    {
        if (instance == null)
            instance = this;
    }

    void UpdateTable()
    {
        
    }


}
