using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private float timeTillStart;

    public GameObject prefab;

    public float coolDown;


    void Start()
    {
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(timeTillStart);
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        SpawnGameObject();
        yield return new WaitForSeconds(coolDown / GameManager.instance.difficulty);
        StartCoroutine(SpawnEnemy());
    }

    private void SpawnGameObject()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
    }
}
