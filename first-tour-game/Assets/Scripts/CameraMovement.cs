using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
	private Camera mainCamera;

	Vector3 Player1Pos;
	Vector3 Player2Pos;


	private void Start()
    {
		mainCamera = GetComponent<Camera>();
    }

    void Update()
    {
		if (GameManager.instance.Coop)
			CameraForCoop();
		else
			CameraForSingle();
    }

	void CameraForSingle()
    {

		if (GameManager.instance.currentBase == null)
			return;

		Player1Pos = new Vector3(Cooperative.instance.Player1.transform.position.x, Cooperative.instance.Player1.transform.position.y, -10);

		float distanceFromFirstToSecond = Vector2.Distance(Player1Pos, GameManager.instance.currentBase.transform.position);

		if (distanceFromFirstToSecond >= 5)
			mainCamera.orthographicSize = 5 + distanceFromFirstToSecond / 10;

		transform.position = Vector3.Lerp(Player1Pos, GameManager.instance.currentBase.transform.position, 0.5f / distanceFromFirstToSecond);

//		Debug.Log("Size of camera: " + mainCamera.orthographicSize + "\n distance from target: " + distanceFromFirstToSecond);

	}

	void CameraForCoop()
    {
		Player1Pos = new (Cooperative.instance.Player1.transform.position.x, Cooperative.instance.Player1.transform.position.y, -10);
		Player2Pos = new (Cooperative.instance.Player2.transform.position.x, Cooperative.instance.Player2.transform.position.y, -10);

		float distanceFromFirstToSecond = Vector2.Distance(Player1Pos, Player2Pos);

		if (distanceFromFirstToSecond >= 5)
			mainCamera.orthographicSize = 5 + distanceFromFirstToSecond / 4;

		transform.position = Vector3.Lerp(Player1Pos, Player2Pos, 0.5f / distanceFromFirstToSecond);

		Debug.Log("Size of camera: " + mainCamera.orthographicSize + "\n distance from target: " + distanceFromFirstToSecond);

	}
}
