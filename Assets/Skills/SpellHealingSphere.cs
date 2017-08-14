using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellHealingSphere : Skill {

	public GameObject HealSphere;
	public int healAmmount = 15;
	public  float range = 30;
	public float speed = 5;
	public GameObject currSphere;
	// Use this for initialization

	public override void TriggerSKill ()
	{
		CmdSpawnSphere ();
	}
	[Command]
	void CmdSpawnSphere(){
		GameObject instance = Instantiate(HealSphere, transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
		instance.GetComponent<Rigidbody> ().velocity = instance.transform.forward*speed;
		HealingSphere SphereScript = instance.GetComponent<HealingSphere>();
		SphereScript.initHealingSphere(damage,damage);
		SphereScript.InitRangeSpeed (range, speed);
		SphereScript.SetOrigin (gameObject);
		SphereScript.InitID (gameObject.GetComponent<NetworkIdentity>().netId);
		currSphere = instance;
		NetworkServer.Spawn (instance);



		

	}

}

