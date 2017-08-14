using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Charge : NetworkBehaviour {

	int Damage = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void initDamage(int damage)
	{
		Damage = damage;
	}

	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("HIT " + other.tag + " WITH FIREBALL");
		if (other.tag == "Other_Player") 
		{
			Debug.Log("FIREBALL HIT " + other.tag);
		
			if(other.GetComponent<Health>() != null)
			{
				other.GetComponent<Health>().TakeDamage(Damage);
			}
		} else if (other.tag == "Enemy")
		{
			Debug.Log("FIREBALL HIT " + other.tag);

			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(Damage);
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
