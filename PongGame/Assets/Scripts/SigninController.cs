using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SocketIO;

public class SigninController : MonoBehaviour {

	public Button submitBtn;
	public InputField inputText;
	private SocketIOComponent _socket;
	// Use this for initialization
	void Start () {

		Canvas canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
		canvas.enabled = false;

		submitBtn.onClick.AddListener(OnClickSubmit);
		NetworkManager.Instance.Init();
		_socket = NetworkManager.Instance.Socket;
		_socket.On("NET_AVARIABLE",  (SocketIOEvent evt) =>{
			canvas.enabled = true;
			Debug.Log("Net Avariable");
		});
		_socket.On("CONNECTED", OnUserSignUped);
		DontDestroyOnLoad(_socket.gameObject);
		DontDestroyOnLoad(GameManager.Instance.gameObject);
	}


	private void OnUserSignUped(SocketIOEvent evt ){
		Debug.Log("ID = "+evt.data.GetField("id").ToString());
		Debug.Log("NAME = "+evt.data.GetField("name").ToString());
		UserData usrData = new UserData();
		usrData.ID = Converter.JsonToString(evt.data.GetField("id").ToString());
		usrData.UserName = Converter.JsonToString(evt.data.GetField("name").ToString());
		usrData.isSignUp = true;
		GameManager.Instance.userData = usrData;
		SceneManager.LoadScene("Main");
	}

	void OnClickSubmit(){

		string nameuser = inputText.text;
		Dictionary<string, string> data = new Dictionary<string, string>();
		data["name"] = nameuser;
		NetworkManager.Instance.Socket.Emit("SIGNUP",new JSONObject(data));


	}

}
	