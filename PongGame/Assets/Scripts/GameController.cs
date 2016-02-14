using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using SocketIO;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
	
	public Text oldText;
	public Text friendText;

	public MoveRacket racketPlayer1;
	public MoveRacket racketPlayer2;
	public List<UserData> _usersData;

	void Start () {
		
		if( GameManager.Instance.userData != null ){
			//txtView.text = GameManager.Instance.userData.UserName;
			_usersData = new List<UserData>();
			NetworkManager.Instance.Socket.On("USER_CONNECTED", OnUserConnected);
			NetworkManager.Instance.Socket.On("USER_DISCONNECTED", OnUserDisconnected);
			NetworkManager.Instance.Socket.On("PLAY_AVARIABLE", OnPlayAvariable );
			StartCoroutine("RequestPlay");
		}else{
			SceneManager.LoadScene("SignIn");
		}

	}

	IEnumerator RequestPlay(){
		yield return new WaitForSeconds(1);
		NetworkManager.Instance.Socket.Emit("PLAY_REQUEST");
	}

	private void OnPlayAvariable(SocketIOEvent evt ){
		
		UserData player1 = CheckPlayer(evt.data.GetField("player1"));
		UserData player2 = CheckPlayer(evt.data.GetField("player2"));

		if( GameManager.Instance.userData.ID == player1.ID ){
			Debug.Log("Player1 Ready!!");
			GameManager.Instance.player = GameManager.Player.player1;
		}else{
			if( GameManager.Instance.userData.ID == player2.ID ){
				Debug.Log("Player2 Ready!!");
				GameManager.Instance.player = GameManager.Player.player2;
			}else{
				Debug.Log("JUST WATCH!!");
				GameManager.Instance.player = GameManager.Player.guest;
			}
		}

		oldText.text 		= player1.UserName;
		friendText.text 	= player2.UserName;
	}

	private UserData CheckPlayer( JSONObject player ){

		UserData usr = null;
		if( player != null ){

			usr = new UserData();
			usr.ID = Converter.JsonToString(player.GetField("id").ToString());
			usr.UserName = Converter.JsonToString(player.GetField("name").ToString());
		}

		return usr;
	}

	private void OnUserDisconnected(SocketIOEvent evt ){

		UserData  usrdata = new UserData();
		usrdata.ID = Converter.JsonToString(evt.data.GetField("id").ToString());
		usrdata.UserName = Converter.JsonToString(evt.data.GetField("name").ToString());
		_usersData.Remove(usrdata);
		usrdata = null;


	}

	private void OnUserConnected( SocketIOEvent evt ){

		UserData  usrdata = new UserData();
		usrdata.ID = Converter.JsonToString(evt.data.GetField("id").ToString());
		usrdata.UserName = Converter.JsonToString(evt.data.GetField("name").ToString());
		_usersData.Add(usrdata);

		Debug.Log("Counter User : "+_usersData.Count);
	}
		
}
