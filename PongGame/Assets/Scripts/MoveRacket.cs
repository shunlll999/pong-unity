using UnityEngine;
using System.Collections;

public class MoveRacket : MonoBehaviour {

	public float moveSpeed = 30;
	public float xPos = 1.5f;
	public string axis = "Vertical";

	void FixedUpdate () {
		float v = Input.GetAxisRaw(axis);
		GetComponent<Rigidbody2D>().velocity = new Vector2( 0, v )*moveSpeed;

	}
}
