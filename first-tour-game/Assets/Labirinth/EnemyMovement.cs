using UnityEngine;
using System.Collections;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float Speed;
    public float coolDown;

    private BoxCollider2D boxCol;

    private Vector2 forward;

    bool isAttackBaseScriptAdded;

    [SerializeField] private LayerMask mask;

    private void Start()
    {
        isAttackBaseScriptAdded = false;
        forward = new Vector2(0, 1);
        boxCol = GetComponent<BoxCollider2D>();
        RightHandAlgoritm();          // расскоментить что бы ходил
    }


    void RotateRight()
    {
        transform.Rotate(0, 0, -90);
        CheckRotation();
    }

    void RotateLeft()
    {
        transform.Rotate(0, 0, 90);
        CheckRotation();
    }

    void CheckRotation()
    {
        if (transform.localEulerAngles.z == 270)
            forward = new Vector2(1, 0);
        if (transform.localEulerAngles.z == 90)
            forward = new Vector2(-1, 0);
        if (transform.localEulerAngles.z == 180)
            forward = new Vector2(0, -1);
        if (transform.localEulerAngles.z == 0)
            forward = new Vector2(0, 1);

    }

    private void RightHandAlgoritm()
    {
        float distance = Vector2.Distance(GameManager.instance.currentMaze.finishPosition, transform.position);

        float coefficientOfEnemy = 0;
        if (tag == "ArtilleryEnemy")
        {
            coefficientOfEnemy = 5f;
        }
        else if (tag == "Enemy")
        {
            coefficientOfEnemy = 2.5f;
        }

        //start attack base and freeze movement
        if (distance < GameManager.instance.level * coefficientOfEnemy)
        {
            if (!isAttackBaseScriptAdded)
            {
                Debug.Log("add enemy attack base");
                gameObject.AddComponent<EnemyAttackBase>();
                Destroy(this);
                isAttackBaseScriptAdded = true;
            }
            
            boxCol.enabled = true;
        }

        if (HitWall())
        {
            Obsticle();
        }
        else
            StartCoroutine(RestartAlgorithm());
    }

    void Obsticle()
    {
        RotateRight();

        if (HitWall())
        {
            RotateLeft();

            if (HitWall())
                RotateLeft();
        }

        StartCoroutine(RestartAlgorithm());
    }

    private bool HitWall()
    {
        RaycastHit2D hit;
        Vector2 start = transform.position;

        Vector2 end = Vector3Extension.AsVector2(transform.position) + forward;
        boxCol.enabled = false;

        hit = Physics2D.Linecast(start, end, mask);

        boxCol.enabled = true;

        if (hit.transform == null)
        {
            Debug.Log("MovingForward");
            MoveForward();
            return false;
        }
        else if (!hit.transform.CompareTag("blockingLayer") && !hit.transform.CompareTag("blockingLayerBreakable"))
        {
            Debug.Log(hit.transform.tag);
            //hit.transform.GetComponent<BoxCollider2D>().enabled = false;
            MoveForward();
            return false;
        }

        return true;
    }


    private void MoveForward()
    {
        LeanTween.move(gameObject, Vector3Extension.AsVector2(transform.position) + forward, Speed / 10);
    }

    public void MoveTo(Vector2 pos)
    {
        transform.position = pos;
    }

    private void MoveBack()
    {
        transform.position = Vector3Extension.AsVector2(transform.position) - forward;
    }

    private IEnumerator RestartAlgorithm()
    {
        yield return new WaitForSeconds(coolDown);
        RightHandAlgoritm();
    }


}