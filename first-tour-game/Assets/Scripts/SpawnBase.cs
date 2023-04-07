using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBase : MonoBehaviour
{
    public static SpawnBase instance;

    public GameObject Base;

    void Start()
    {
        if (instance == null)
            instance = this;

        int num = GameManager.instance.level * 12 / 2;
        Instantiate(Base, new Vector2(num, num), Quaternion.identity);
    }
}
