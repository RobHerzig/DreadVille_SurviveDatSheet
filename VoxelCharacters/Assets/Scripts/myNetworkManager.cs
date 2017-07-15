using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.Networking.NetworkSystem;

public class myNetworkManager : NetworkManager {


	public GameObject classSelection;
	int avatarIndex = 0;
	public GameObject menueCam;
	GameObject instanceCamera;
	internal bool isHosting= false;
	internal bool isClientcon= false;

	// Use this for initialization
	void Start () {
		instanceCamera = Instantiate<GameObject> (menueCam);
		GameObject selection = Instantiate<GameObject> (classSelection,new Vector3(Screen.width/2,Screen.height/2,0f),Quaternion.identity);
		selection.transform.SetParent(GameObject.Find("MainUI").transform);
		selection.GetComponent<ClassSelection> ().manager = this;
		GameObject classselectButtons = selection.transform.GetChild (0).gameObject;
		for (int i = 0; i < 6; i++) {
			Button button = classselectButtons.transform.GetChild (i).GetComponent<Button> ();
			button.onClick.AddListener (delegate {AvatarPicker (button.name);});
		}

	}


	void Update(){
		if (isHosting) {
			Destroy (instanceCamera);
			isHosting = false;
			StartHost ();
		}
		if (isClientcon){
			Debug.Log ("join");
			Destroy (instanceCamera);
			isClientcon = false;
			networkAddress = "localhost";
			networkPort = 7777;
			StartClient ();
		}
	}

	public void HostButton(){
		Destroy (instanceCamera);
		StartHost ();
	}
	public void JoinButton(){
		Destroy (instanceCamera);

		networkAddress = "localhost";
		networkPort = 7777;
		StartClient ();
	}
	void AvatarPicker(string buttonName)
	{
		switch (buttonName)
		{
		case "Player1":
			avatarIndex = 0;
			break;
		case "Player2":
			avatarIndex = 1;
			break;
		case "Player3":
			avatarIndex = 2;
			break;
		case "Player4":
			avatarIndex = 3;
			break;
		case "Player5":
			avatarIndex = 4;
			break;
		case "Player6":
			avatarIndex = 5;
			break;
		}

		  playerPrefab = spawnPrefabs[avatarIndex];
	}

	/// Copied from Unity's original NetworkManager script except where noted
	public override void OnClientConnect(NetworkConnection conn)
	{



		/// ***
		/// This is added:
		/// First, turn off the canvas...

		/// Can't directly send an int variable to 'addPlayer()' so you have to use a message service...
		IntegerMessage msg = new IntegerMessage(avatarIndex);
		/// ***

		if (!clientLoadedScene)
		{
			// Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
			ClientScene.Ready(conn);
			if (autoCreatePlayer)
			{
				///***
				/// This is changed - the original calls a differnet version of addPlayer
				/// this calls a version that allows a message to be sent
				ClientScene.AddPlayer(conn, 0, msg);
			}
		}

	}

	/// Copied from Unity's original NetworkManager 'OnServerAddPlayerInternal' script except where noted
	/// Since OnServerAddPlayer calls OnServerAddPlayerInternal and needs to pass the message - just add it all into one.
	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
	{
		/// *** additions
		/// I skipped all the debug messages...
		/// This is added to recieve the message from addPlayer()...
		int id = 0;

		if (extraMessageReader != null)
		{
			IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
			id = i.value;
		}

		/// using the sent message - pick the correct prefab
		GameObject playerPrefab = spawnPrefabs[id];
		/// *** end of additions

		GameObject player;
		Transform startPos = GetStartPosition();
		if (startPos != null)
		{
			player = (GameObject)Instantiate(playerPrefab, startPos.position, startPos.rotation);
		}
		else
		{
			player = (GameObject)Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
		}

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}
}