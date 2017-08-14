using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaponSword : Projectile {

	internal float damage;
	protected override void TriggerEnter(Collider other){
		
		if (other.GetComponent<Player> () != null) {
			

			if (other.GetComponent<Health> () != null) {
				other.GetComponent<Health> ().TakeDamage (damage);
			}
		} 
	}
}
