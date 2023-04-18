using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSoundManger : MonoBehaviour
{
    private void Start()
    {
        Destroy(SoundsManager.instance.gameObject);
    }
}
