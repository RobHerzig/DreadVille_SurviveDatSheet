using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FreezeCone : NetworkBehaviour {

	float ConeDamage = 0;
	float freezeTime = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initDamage(float damage)
	{
		ConeDamage = damage;
	}
	public void initFreezeTime(float FreezeTime)
	{
		freezeTime = FreezeTime;
	}

	private void OnTriggerEnter(Collider other)
	{
	 if (other.tag == "Enemy")
		{
			Debug.Log("CONE HIT " + other.tag);

			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(ConeDamage);
				FreezeEnemy (other.gameObject, freezeTime);
			}
		}
	}
	public void FreezeEnemy(GameObject enemy,float durration)
	{
		enemy.GetComponent<EnemyTarget> ().CroudControllThis(durration);
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