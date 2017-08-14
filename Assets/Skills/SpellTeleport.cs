using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellTeleport : NetworkBehaviour {

	public float range = 3f;



	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}

		if (Input.GetKeyDown (KeyCode.T))
		{
			Teleport (1);
		}
		if (Input.GetKeyDown (KeyCode.R))
		{
			Teleport (-1);
		}


	}
	void Teleport(int forward){
			transform.position += transform.forward * range*forward;
		
	}


}
