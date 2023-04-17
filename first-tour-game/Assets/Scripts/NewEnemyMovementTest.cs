using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;
public class NewEnemyMovementTest : MonoBehaviour
{
    [SerializeField] GameObject prefabDebug;
    [SerializeField] MazeSpawner mazespawner;
    private HintRenderer renderer;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float coefficientOfEnemy;
    private bool isAttackBaseScriptAdded;

    private Vector2 lastPlayerPosition;
    // Start is called before the first frame update

    private void Start()
    {
        lastPlayerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        isAttackBaseScriptAdded = false;
        Test();
    }
    public void Test()
    {
        renderer = new HintRenderer();
        renderer.finishPosition = lastPlayerPosition;
        renderer.DrawPath(transform.position, prefabDebug);
        StartCoroutine("StartMovement");
    }

    private void Update()
    {
        if (tag == "HunterEnemy")
        {
            Vector2 obj = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector2 temp = obj - lastPlayerPosition;
            if (temp.x > 1f || temp.y > 1f)
            {
                lastPlayerPosition = obj;
            }
        }
    }


    public IEnumerator StartMovement()
    {
        List<Vector3> positions = renderer.positions;
        positions.Reverse();
        Random random = new Random();
        int randomPositionToSpawnUpgrade = random.Next(0, positions.Count);
        for (int i = 0; i < positions.Count; i++)
        {   
            transform.position = new Vector2(positions[i].x + 0.5f, positions[i].y + 0.7f);

            float distance = Vector2.Distance(GameManager.instance.currentMaze.finishPosition, transform.position);

            if (tag == "HunterEnemy")
            {
                distance = Vector2.Distance(lastPlayerPosition, transform.position);
            }
            
            if (tag == "JokerEnemy" && i == randomPositionToSpawnUpgrade)
            {
                GetComponent<UpgradesSpawn>().SpawnUpgrade(Mathf.CeilToInt(positions[i].x), Mathf.CeilToInt(positions[i].y));
            }

            //start attack base and freeze movement
            int num = GameManager.instance.difficulty;
            if (num >= 4)
                num /= 2;

            if (distance < num * coefficientOfEnemy)
            {
                if (!isAttackBaseScriptAdded)
                {
                    Debug.Log("attack: " + positions[i].x + ", " + positions[i].y);
                    gameObject.AddComponent<EnemyAttackBase>();
                    isAttackBaseScriptAdded = true;
                    Destroy(this);
                    enabled = false;
                }

                //boxCol.enabled = true;
            }
            yield return new WaitForSeconds(cooldown);
        }
    }
}
