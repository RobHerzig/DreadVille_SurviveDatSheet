using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellLightBombs : Skill {

	public GameObject HealSphere;
	public  float range = 30;
	public float speed = 5;
	public float accerleration = 1;
	public float spawnSpeed = 3;
	public float timeInactive = 2;
	public int numerOFSpheres= 4;
	// Use this for initialization
	void Start () {

	}


	public override void TriggerSKill ()
	{
		StartCoroutine (SphereSpawner ());
	}
			



	IEnumerator SphereSpawner(){
		Vector3 currForward = transform.forward*0.5f;
		float radius = currForward.x + currForward.y;
		float scope = radius * Mathf.PI * 2;
		//float timetoWait = scope / (numerOFSpheres*spawnSpeed);
		for (int i = 0; i < numerOFSpheres; i++) {
			CmdSpawnSphere (currForward,360f/(spawnSpeed));
			yield return new WaitForSeconds (spawnSpeed/numerOFSpheres);
		}
	}



	[Command]
	void CmdSpawnSphere(Vector3 spawnPoint,float angleToPassPerSec){
		GameObject instance = Instantiate(HealSphere, (transform.position + transform.up * 0.4f+ spawnPoint), Quaternion.identity) as GameObject;
		instance.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		LightBomb SphereScript = instance.GetComponent<LightBomb>();
		SphereScript.initHealingSphere(damage, range, speed, angleToPassPerSec, timeInactive, accerleration,gameObject.transform);
		instance.transform.parent = transform;
		NetworkServer.Spawn (instance);
		RpcSetParent (gameObject, instance);


	}
	[ClientRpc]
	void RpcSetParent(GameObject parent ,GameObject child){
		child.transform.parent = parent.transform;
	}
}
