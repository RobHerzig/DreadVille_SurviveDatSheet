using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillTrap : NetworkBehaviour {

	public GameObject trap;
	public int TriggerDamage = 90;
	public float TrapDurration = 5;
	public float SlowPercent = 0.7f;
	public float TrapSizeTriggered = 5f;
	public float slowDurration = 2;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}

		if(Input.GetKeyDown(KeyCode.Z))
		{
			CmdPlaceTrap();
		}
	}
	[Command]
	public void CmdPlaceTrap()
	{
		GameObject instance = Instantiate(trap, transform.position + transform.up * 0.1f , Quaternion.identity) as GameObject;
		Trap trapscript = instance.GetComponentInChildren<Trap>();
		trapscript.initDamage(TriggerDamage);
		trapscript.initSlow (SlowPercent, slowDurration);
		trapscript.initScaleSize (TrapSizeTriggered);
		trapscript.initduration (TrapDurration);
		NetworkServer.Spawn (instance);
	}

}
