using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellFireball : Skill {

    public GameObject FireBall;



	public override void TriggerSKill ()
	{
			CmdThrowFireball ();

	}
	[Command]
	void CmdThrowFireball(){


		GameObject instance = Instantiate(FireBall, transform.position + transform.up * 0.4f, transform.rotation) as GameObject;
		instance.GetComponent<Rigidbody> ().velocity = instance.transform.forward*10f;
		FireBall ballScript = instance.GetComponent<FireBall>();
		ballScript.initDamage(damage);
		ballScript.InitID (gameObject.GetComponent<NetworkIdentity>().netId);
		NetworkServer.Spawn (instance);
		Destroy (instance, 5f);




	}


}
