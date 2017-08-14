using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PiercingShot : NetworkBehaviour {

	int projectileDamage = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initDamage(int damage)
	{
		projectileDamage = damage;
	}

	private void OnTriggerEnter(Collider other)
	{
 if (other.tag == "Enemy")
		{
			Debug.Log("PiercingShot HIT " + other.tag);
	
			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(projectileDamage);
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
