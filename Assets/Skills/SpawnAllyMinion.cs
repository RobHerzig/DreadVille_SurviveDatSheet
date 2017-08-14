using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnAllyMinion : Skill {

	public GameObject Ally;
	//public float cooldown = 2;

	public float targetSwitchDuration = 3f;
	public float allyRotationSpeed = 2;
	public float allySpeed = 4f;
	public float allyDurration = 10f;
	private bool isAllyActive = false;

	// Update is called once per frame
	public override void TriggerSKill ()
	{
		isAllyActive = true;
		CmdSpawnMinion();
		StartCoroutine(ActiveCoroutine(allyDurration));

	}
	IEnumerator ActiveCoroutine(float duration){
		yield return new WaitForSeconds (duration);
		isAllyActive = false;
	}
	[Command]
	public void CmdSpawnMinion()
	{
		GameObject instance = Instantiate(Ally, transform.position + transform.up * 0.6f, transform.rotation) as GameObject;
		instance.GetComponent<AllyMinion>().InitAlly(damage,damage,allySpeed,targetSwitchDuration,allyRotationSpeed);
		instance.GetComponent<AllyMinion> ().SetOrigin (gameObject);
		NetworkServer.Spawn (instance);
		Destroy (instance, allyDurration);
	}


}
