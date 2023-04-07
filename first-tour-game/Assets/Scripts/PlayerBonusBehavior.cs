using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBonusBehavior : MonoBehaviour
{
    private int totalResources;
    [SerializeField] Text resourcesCounterText;
    [SerializeField] GameObject baseObject;
    private BaseBehavior baseBehaviorScript;
    private SpriteRenderer sr;
    private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        totalResources = 0;
        baseBehaviorScript = baseObject.GetComponent<BaseBehavior>();
        sr = GetComponent<SpriteRenderer>();
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
        if (collision.tag == "ResourceBonus")
        {
            totalResources++;
            resourcesCounterText.text = "Resources: " + totalResources;
        }
        if (collision.tag == "ShieldBonus")
        {
            baseBehaviorScript.ActivateShield();

            yield return new WaitForSeconds(5f);
            baseBehaviorScript.DisableShield();
        }
        if (collision.tag == "InvisibilityBonus")
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            sr.color = new Color(0, 236, 215, 130);
            //yield return new WaitForSeconds(5f);
            float time = 5f;
            while (time > 0)
            {
                time -= Time.deltaTime;
            }
            sr.color = new Color(255, 255, 255, 255);
            GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
