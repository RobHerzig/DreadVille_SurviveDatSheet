using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyMeele : Enemy {
	
	public EnemyWaponSword sword;




	public override void Init()
	{
		if (sword != null)
			sword.damage = damage;
		agent =GetComponent<NavMeshAgent>();
		agent.stoppingDistance = StoppingDistance;
		GetComponentInChildren<Animator> ();

	}
	void Start(){
		Init ();
	}
	// Use this for initialization
	void Update () {
		if (!isServer) {
			return;
		}
		currTarget = agent.destination;
		if(currTarget != null)
			transform.LookAt (currTarget);
		distanceToTarget = Vector3.Distance (agent.destination, transform.position);
		Hit(currTarget != null&& distanceToTarget<=  maxDistanceToHitTarget);
	}
	
	// Update is called once per frame

	 void Hit(bool hit){
		anim.SetBool ("Hit", hit);
	}
}
