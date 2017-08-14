using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealForPlayer : NetworkBehaviour {

	public float heal = 20;
	void Start(){
		Debug.Log("HealSpawned");
	}
	private void OnTriggerEnter(Collider other)
	{
		
		if (other.GetComponent<Player> () != null) {

			if (other.GetComponent<Health> () != null) {
				other.GetComponent<Health> ().TakeHeal (heal);
				Destroy (gameObject);
			}
		}
	}
}
