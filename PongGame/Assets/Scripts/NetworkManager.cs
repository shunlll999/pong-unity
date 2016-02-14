﻿using UnityEngine;
using System.Collections;
using SocketIO;

public class NetworkManager : MonoBehaviour {

	private static NetworkManager _instance;
	private SocketIOComponent _socket;

	public static NetworkManager Instance{

		get
		{
			if( _instance == null ){
				_instance = new GameObject("_NetworkManager").AddComponent<NetworkManager>();
			}

			return _instance;
		}

	}

	public void Init(){
		DontDestroyOnLoad(gameObject);
	}

	public SocketIOComponent Socket{
		get{
			GameObject goSocket = GameObject.Find("SocketIO");
			_socket = goSocket.GetComponent<SocketIOComponent>();
			return _socket;
		}
	}

}
