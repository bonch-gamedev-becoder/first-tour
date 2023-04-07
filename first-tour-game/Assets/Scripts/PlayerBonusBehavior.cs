using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonusBehavior : MonoBehaviour
{
    public static PlayerBonusBehavior instance;

    public int totalResources;

    [SerializeField] BoxCollider2D collider;
    public BaseBehavior baseBehaviorScript;
    [SerializeField] SpriteRenderer sr;

    public TextMeshPro resourcesCounterText;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
            instance = this;

        totalResources = 0;
        //collider = GetComponent<BoxCollider2D>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Base")
            return;

        Destroy(collision.gameObject);
        if (collision.tag == "ShieldBonus")
        {
            StartCoroutine(ShieldBonus());
        }
        if (collision.tag == "InvisibilityBonus")
        {
            Debug.Log("collide start");
            StartCoroutine(InvisibilityBonus());
        }
        if (collision.tag == "ThanosBonus")
        {
            ThanosBonus();
        }
    }

    void ThanosBonus()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        for (int i = 0; i < enemies.Length; i += 2)
        {
            enemies[i].GetComponent<EnemyCombat>().Death();
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
        Debug.Log("collide Inside function");

        collider.enabled = false;
        sr.color = new Color(0, 236, 215, 130);

        Debug.Log("collide before waitforseconds function");

        yield return new WaitForSeconds(3f);
        sr.color = new Color(255, 255, 255, 255);

        Debug.Log("collide enabled");
        collider.enabled = true;
    }
}
