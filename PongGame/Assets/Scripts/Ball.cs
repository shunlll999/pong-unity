using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	public float ballSpeed = 30;

	void Start(){
		//StartCom();
		NetworkManager.Instance.Socket.On("READY", OnReadyPlay );
		NetworkManager.Instance.Socket.On("GET_SHOOT", OnGetShoot );
	}

	private void OnReadyPlay(SocketIO.SocketIOEvent evt ){

		Debug.Log("READY TO PLAY");
//		float py = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
//		Vector2 direction = new  Vector2(1,py).normalized;
//		GetComponent<Rigidbody2D>().velocity = direction*ballSpeed;
		StartCom();

	}

	public void StartCom(){
		GetComponent<Rigidbody2D>().velocity = Vector2.right*ballSpeed;
	}

	void OnCollisionEnter2D( Collision2D col ){

		if( col.gameObject.name == "RocketLeft" ){
			float py = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);

			JSONObject data = new JSONObject();
			data.AddField("float", py.ToString());
			data.AddField("rocket", col.gameObject.name);
			NetworkManager.Instance.Socket.Emit("SHOOT", data );
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//Vector2 direction = new  Vector2(1,py).normalized;
			//GetComponent<Rigidbody2D>().velocity = direction*ballSpeed;
		}

		if( col.gameObject.name == "RocketRight" ){
			float py = HitFactor(transform.position, col.transform.position, col.collider.bounds.size.y);
			JSONObject data = new JSONObject();
			data.AddField("float", py.ToString());
			data.AddField("rocket", col.gameObject.name);
			NetworkManager.Instance.Socket.Emit("SHOOT", data );
			GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			//Vector2 direction = new  Vector2(-1,py).normalized;
			//GetComponent<Rigidbody2D>().velocity = direction*ballSpeed;
		}


	}

	private void OnGetShoot( SocketIO.SocketIOEvent evt ){

		Debug.Log("Recieve Rocket "+evt.data.GetField("rocket").ToString());
		Debug.Log("Recieve Float "+evt.data.GetField("float").ToString());
		if( Converter.JsonToString(evt.data.GetField("rocket").ToString()) == "RocketLeft" ){
			
			Vector2 direction = new  Vector2(1,Converter.JsonToFloat(evt.data.GetField("float").ToString())).normalized;
			GetComponent<Rigidbody2D>().velocity = direction*ballSpeed;
		}

		if(Converter.JsonToString(evt.data.GetField("rocket").ToString()) == "RocketRight" ){
			Vector2 direction = new  Vector2(-1,Converter.JsonToFloat(evt.data.GetField("float").ToString())).normalized;
			GetComponent<Rigidbody2D>().velocity = direction*ballSpeed;
		}
	}

	float HitFactor( Vector2 ballPos, Vector2 racketPos, float racketHeight ){
		// ascii art:
		// ||  1 <- at the top of the racket
		// ||
		// ||  0 <- at the middle of the racket
		// ||
		// || -1 <- at the bottom of the racket
		return (ballPos.y-racketPos.y)/racketHeight;
	}

}
