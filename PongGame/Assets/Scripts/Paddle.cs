using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float paddleSpeed = 1;
	public Vector3 playerPos;

	void Update () {
		float yPos = gameObject.transform.position.y + (Input.GetAxis("Vertical")*paddleSpeed);
		playerPos = new Vector3(-40f, Mathf.Clamp(yPos, -23,23),0);
		gameObject.transform.position = playerPos;
	}
}
