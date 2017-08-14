using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellFreezeCone : Skill {

	public GameObject freezeCone;
	public float freezeTime = 3f;
	public int durration = 2;
	// Use this for initialization
	public override void TriggerSKill ()
	{
		CmdUseCone ();

	}
	[Command]
	public void CmdUseCone()
	{
		GameObject instance = Instantiate(freezeCone, transform.position + transform.up * 0.4f, gameObject.transform.rotation) as GameObject;
		FreezeCone coneScript = instance.GetComponent<FreezeCone>();
		coneScript.initDamage(damage);
		coneScript.initFreezeTime (freezeTime);
		coneScript.SetOrigin (gameObject);
		NetworkServer.Spawn (instance);
		Destroy (instance,durration);
	}


}
