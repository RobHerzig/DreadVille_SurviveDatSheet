using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class EnemyBullet : NetworkBehaviour {
	internal float damage;
	internal Rigidbody rg;


	void Start(){
		rg = GetComponent<Rigidbody> ();
		Debug.Log ("bulletspawned");
	}
	void Update(){
		if (!isServer) {
			return;
		}
		transform.rotation.SetLookRotation(rg.velocity);

	}
	void OnTriggerEnter(Collider other){

		if (other.GetComponent<Player> () != null) {


			if (other.GetComponent<Health> () != null) {
				other.GetComponent<Health> ().TakeDamage (damage);

			}
		} 
	}
}


