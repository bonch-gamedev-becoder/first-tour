using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    public float Speed = 2;

    private BoxCollider2D boxCol;

    private Vector2 forward;

    private Vector2 End;

    private Rigidbody2D componentRigidbody;

    bool canMove;
    float coolDown;

    private void Start()
    {
        canMove = true;
        forward = new Vector2(0, 1);
        coolDown = 1f;
        boxCol = GetComponent<BoxCollider2D>();
        componentRigidbody = GetComponent<Rigidbody2D>();
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

        hit = Physics2D.Linecast(start, end);

        boxCol.enabled = true;

        if (hit.transform == null)
        {
            MoveForward();
            return false;
        }

        return true;
    }


    private void MoveForward()
    {
        transform.position = Vector3Extension.AsVector2(transform.position) + forward;
        canMove = false;
    }

    public void MoveTo(Vector2 pos)
    {
        transform.position = pos;
        canMove = false;
    }

    private void MoveBack()
    {
        transform.position = Vector3Extension.AsVector2(transform.position) - forward;
        canMove = false;
    }

    private IEnumerator RestartAlgorithm()
    {
        yield return new WaitForSeconds(coolDown);
        RightHandAlgoritm();
    }


}