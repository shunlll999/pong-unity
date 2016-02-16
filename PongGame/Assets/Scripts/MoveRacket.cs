using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveRacket : MonoBehaviour {

	public float moveSpeed = 30;
	public float xPos = 1.5f;
	public string axis = "Vertical";

//	void FixedUpdate () {
//		float v = Input.GetAxisRaw(axis);
//		GetComponent<Rigidbody2D>().velocity = new Vector2( 0, v )*moveSpeed;
//
//	}

	public void Move(){

		float v = Input.GetAxisRaw(axis);
		if( v != 0 ){
			transform.position = new Vector3( transform.position.x,   transform.position.y+((moveSpeed*Time.deltaTime)*v),  transform.position.z);
			Dictionary<string, string> data = new Dictionary<string, string>();
			data["position"] = transform.position.x.ToString()+","+transform.position.y.ToString();
			NetworkManager.Instance.Socket.Emit("RACKET_MOVE", new JSONObject(data));
		}
		
	}

	public void FromBoradcastVelocity( Vector2 velocity ){
		StartCoroutine(MoveLerp(velocity.y));
	}

	IEnumerator MoveLerp( float pos){
		float posY = transform.position.y;
		while( posY !=  pos){
			posY = Mathf.Lerp(posY, pos, 5*Time.deltaTime);
			transform.position = new Vector2( transform.position.x, posY);
			yield return new WaitForEndOfFrame();
		}

		yield return new WaitForEndOfFrame();

	}
}
