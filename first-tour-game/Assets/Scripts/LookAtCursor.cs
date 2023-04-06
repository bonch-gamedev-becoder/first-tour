using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCursor : MonoBehaviour
{
   
    void Update()
    {
		Look();
    }

	void Look()
	{
		SoftPlayerCamera();
		Vector3 lookPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
		lookPos = lookPos - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void SoftPlayerCamera()
    {
		LeanTween.move(Camera.main.gameObject, new Vector2(PlayerMovement.instance.transform.position.x, PlayerMovement.instance.transform.position.y), 0f);
    }
}
