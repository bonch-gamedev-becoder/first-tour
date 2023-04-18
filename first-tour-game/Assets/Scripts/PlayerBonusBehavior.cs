using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonusBehavior : MonoBehaviour
{
    public static PlayerBonusBehavior instance;

    public int totalResources;

    private CircleCollider2D collider;
    [SerializeField] SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<CircleCollider2D>();
        if (instance == null)
            instance = this;

        totalResources = 0;
        //collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Base")
            return;

        
        if (collision.tag == "ShieldBonus")
        {
            StartCoroutine(ShieldBonus());
            Destroy(collision.gameObject);
        }
        if (collision.tag == "InvisibilityBonus")
        {
            Debug.Log("collide start");
            StartCoroutine(InvisibilityBonus());
            Destroy(collision.gameObject);
        }
        if (collision.tag == "ThanosBonus")
        {
            ThanosBonus();
            Destroy(collision.gameObject);
        }
    }

    void ThanosBonus()
    {
        GameObject[] enemies = FindGameObjectsWithLayer(9);

        if (enemies == null)
            return;

        for (int i = 0; i < enemies.Length; i += 2)
        {
            try
            {
                enemies[i].TryGetComponent(out EnemyCombat combat);
                combat.Death();
            }
            catch (IndexOutOfRangeException)
            {
                break;
            }
        }
    }

    IEnumerator ShieldBonus()
    {
        GameManager.instance.currentBase.ActivateShield();
        yield return new WaitForSeconds(5f);
        GameManager.instance.currentBase.DisableShield();
    }

    IEnumerator InvisibilityBonus()
    {
//        Debug.Log("collide Inside function");

        collider.enabled = false;
        sr.color = new Color(0, 236, 215, 130);

        //Debug.Log("collide before waitforseconds function");

        yield return new WaitForSeconds(3f);
        sr.color = new Color(255, 255, 255, 255);

        //Debug.Log("collide enabled");
        collider.enabled = true;
    }

    private GameObject[] FindGameObjectsWithLayer(int layerNumbeer)
    {
        GameObject[] tempArr = FindObjectsOfType<GameObject>();
        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < tempArr.Length; i++)
        {
            if (tempArr[i].layer == layerNumbeer)
            {
                list.Add(tempArr[i]);
            }
        }
        if (list.Count == 0)
        {
            return null;
        }
        return list.ToArray();
    }
}
