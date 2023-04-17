using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	private Camera mainCamera;

    private void Start()
    {
		mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
		Look();
    }

	void Look()
	{
		changePos();

	}

	void changePos()
    {
		Vector3 Player1Pos = new Vector3(Cooperative.instance.Player1.transform.position.x, Cooperative.instance.Player1.transform.position.y, -10);
		Vector3 Player2Pos = new Vector3(Cooperative.instance.Player2.transform.position.x, Cooperative.instance.Player2.transform.position.y, -10);

		float distanceFromFirstToSecond = Vector3.Distance(Player1Pos, Player2Pos);

		if (mainCamera.orthographicSize > 5)
		mainCamera.orthographicSize = distanceFromFirstToSecond;


		if (GameManager.instance.Coop)
			transform.position = Vector3.Lerp(Player1Pos, Player2Pos, 0.5f / distanceFromFirstToSecond);
		else
			transform.position = Vector3.Lerp(Player1Pos, GameManager.instance.currentBase.transform.position, 0.5f / distanceFromFirstToSecond);
	}
}
