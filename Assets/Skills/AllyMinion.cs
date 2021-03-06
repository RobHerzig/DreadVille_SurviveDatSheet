﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.AI;
public class AllyMinion : Projectile {

	NetworkInstanceId isFromPlayer;
	float AllyDamage = 0;
	float AllyHeal= 0;
	Transform currTarget;
	private float speed = 0;
	float targetSwitchduration= 0;
	float roationspeed;
	public float radius = 5;
	private LayerMask raycastLayer;
	bool isTargetPlayer;
	Vector3 newVelocity;
	void Update(){
		if (!isServer) {
			return;
		}
		GetComponent<Rigidbody> ().velocity = Vector3.Lerp (GetComponent<Rigidbody> ().velocity, newVelocity, Time.deltaTime*roationspeed);
	}
	public void InitAlly(float damage,float heal,float speed,float targetSwitchDuration,float roationspeed)
	{
		AllyDamage = damage;
		AllyHeal = heal;
		this.speed = speed;
		targetSwitchduration = targetSwitchDuration;
		this.roationspeed = roationspeed;
	}

	void Start(){
		if (!isServer) {
			return;
		}
		StartCoroutine (TargetCoroutine());
		//newVelocity = transform.forward*speed;
		raycastLayer = 1 << LayerMask.NameToLayer("Player");

	}
	protected override void TriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null) 
		{
			

			if(other.GetComponent<Health>() != null)
			{
				other.GetComponent<Health>().TakeHeal(AllyHeal);
			}
		} else if (other.tag == "Enemy")
		{
			
			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(AllyDamage);
			}
		}
	}
	void SearchForTarget()
	{
		



			
			Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, raycastLayer);
			if(hitColliders.Length > 0)
			{
				int randomInt = Random.Range(0, hitColliders.Length);
				currTarget = hitColliders[randomInt].transform;
			}
		if(newVelocity != null)
			newVelocity = speed*(currTarget.position - transform.position).normalized;
	
	}
	IEnumerator TargetCoroutine(){
		while (true) {
			isTargetPlayer = !isTargetPlayer;
			if (isTargetPlayer) 
				raycastLayer = 1 << LayerMask.NameToLayer("Player");
			else
				raycastLayer = 1 << LayerMask.NameToLayer("Enemy");
			
			SearchForTarget ();
			yield return new WaitForSeconds(targetSwitchduration);
		}
	}
}
