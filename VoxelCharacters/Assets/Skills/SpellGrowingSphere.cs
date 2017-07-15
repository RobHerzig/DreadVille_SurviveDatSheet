using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellGrowingSphere : Skill {

	public GameObject Sphere;
	public int numberOfSpellstoExplode = 4;
	public float detonationRadius = 10f;
	public float maxSize = 5;
	public float maxDuration = 10;

	public override void TriggerSKill ()
	{
		CmdThrowFireball ();

	}
	[Command]
	void CmdThrowFireball(){


		GameObject instance = Instantiate(Sphere, transform.position + transform.up * 0.6f+transform.forward*2f, transform.rotation) as GameObject;
		instance.GetComponent<GrowingSphere>().InitSphere(damage,numberOfSpellstoExplode,detonationRadius,maxSize);
	
		NetworkServer.Spawn (instance);
		Destroy (instance, maxDuration);




	}


}