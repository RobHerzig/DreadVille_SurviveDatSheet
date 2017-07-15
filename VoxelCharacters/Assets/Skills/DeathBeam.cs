using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathBeam : NetworkBehaviour {

	float beamDamage = 0;
	NetworkInstanceId isFromPlayer;
	[Command]
	public void CmdDestroyBeam(){
		NetworkServer.Destroy (transform.parent.gameObject);
	}
	public void initDamage(float damage)
	{
		beamDamage = damage;
	}

	public void InitID(NetworkInstanceId PlayerID)
	{
		isFromPlayer = PlayerID;
	}

	private void OnTriggerEnter(Collider other){
		
	}

	private void OnTriggerStay(Collider other)
	{

		if (other.tag == "Enemy")
		{
			

			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(beamDamage);
			}
		}
	}



	[Command]
	void CmdTellServerWhoWasShot(GameObject go, int dmg)
	{
		//GameObject go = GameObject.Find(ID);
		go.GetComponent<Health>().TakeDamage(dmg);
	}
	[Command]
	void CmdTellServerWhichEnemyWasShot(GameObject go, int dmg)
	{
		//GameObject go = GameObject.Find(ID);
		go.GetComponent<EnemyHealth>().TakeDamage(dmg);
	}
}