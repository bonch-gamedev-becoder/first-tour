using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;

public class NewEnemyMovementTest : MonoBehaviour
{

    [SerializeField] MazeSpawner mazespawner;
    private HintRenderer renderer;
    [SerializeField] float cooldown = 1f;
    [SerializeField] float coefficientOfEnemy;
    private bool isAttackBaseScriptAdded;

    // Start is called before the first frame update
    void Start()
    {
        renderer = new HintRenderer();
        renderer.DrawPath(transform.position);
        isAttackBaseScriptAdded = false;
        StartCoroutine(StartMovement());
    }

    private IEnumerator StartMovement()
    {
        List<Vector3> positions = renderer.positions;
        positions.Reverse();
        for (int i = 0; i < positions.Count; i++)
        {
            transform.position = new Vector2(positions[i].x + 0.5f, positions[i].y + 0.7f);

            float distance = Vector2.Distance(GameManager.instance.currentMaze.finishPosition, transform.position);

            //start attack base and freeze movement
            int num = GameManager.instance.difficulty;
            if (num >= 4)
                num /= 2;

            if (distance < num * coefficientOfEnemy)
            {
                if (!isAttackBaseScriptAdded)
                {
                    Debug.Log("add enemy attack base");
                    gameObject.AddComponent<EnemyAttackBase>();
                    Destroy(this);
                    isAttackBaseScriptAdded = true;
                }

                //boxCol.enabled = true;
            }
            yield return new WaitForSeconds(cooldown);
        }
        enabled = false;

    }
}
