using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillCharge : NetworkBehaviour {

	public int Power = 15;
	public GameObject ChargeCollider;
	public float ChargeRange = 5f;
	public float  duration = 3f;
	private bool isAtTarget = true;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}

		if(Input.GetKeyDown(KeyCode.V)&& isAtTarget)
		{
			CmdCharge();
			StartCoroutine (ChargeCoroutine(ChargeRange,duration)) ;
		}
	}
	[Command]
	public void CmdCharge()
	{
		
		isAtTarget = false;
		GameObject instance = Instantiate(ChargeCollider, transform.position , Quaternion.identity) as GameObject;
		instance.transform.parent = gameObject.transform;
		Charge chargeScript = instance.GetComponentInChildren<Charge>();
		chargeScript.initDamage(Power);
		NetworkServer.SpawnWithClientAuthority (instance,connectionToClient);
		RpcSetParent (gameObject, instance);
		Destroy (instance, ChargeRange/duration);
	}

	[ClientRpc]
	void RpcSetParent(GameObject parent ,GameObject child){
		child.transform.parent = parent.transform;
	}


	IEnumerator ChargeCoroutine(float range,float time)
	{
		isAtTarget = false;
		float elapsedTime = 0;
		Transform player = gameObject.transform;
		Vector3 startPostion = player.position;
		Vector3 targetPosition =player.position + player.forward.normalized * range;
		while ((elapsedTime / time) < 1f) {
			player.position = Vector3.Lerp (startPostion, targetPosition, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		isAtTarget = true;
	}

}
