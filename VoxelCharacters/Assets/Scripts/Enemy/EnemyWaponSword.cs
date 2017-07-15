using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaponSword : MonoBehaviour {

	internal float damage;
	void OnTriggerEnter(Collider other){
		
		if (other.GetComponent<Player> () != null) {
			

			if (other.GetComponent<Health> () != null) {
				other.GetComponent<Health> ().TakeDamage (damage);
			}
		} 
	}
}
