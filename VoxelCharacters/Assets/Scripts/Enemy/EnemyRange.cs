using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyRange : Enemy {



	public GameObject bullet;
	public float shootRatio = 2f;
	public float bulletSpeed = 3f;


	public override void Init()
	{

		agent =GetComponent<NavMeshAgent>();
		agent.stoppingDistance = StoppingDistance;
		GetComponentInChildren<Animator> ();
	}

	// Use this for initialization
	void Start () {
		Init ();
		StartCoroutine (Shoot ());
	}

	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}

		currTarget = agent.destination;
		if(currTarget != null)
			transform.LookAt (currTarget);



	}
	[Command]
	void CmdSpawnBullet(){
		GameObject Bullet = Instantiate (bullet, transform.position + transform.forward * 0.3f + transform.up * 0.3f, transform.rotation);
		Bullet.GetComponent<EnemyBullet> ().damage = damage;

		Bullet.GetComponent<Rigidbody> ().velocity = (currTarget- transform.position).normalized*bulletSpeed; //+transform.up*2).normalized*distanceToTarget*60;
		NetworkServer.Spawn (Bullet);
		Destroy (Bullet, 5);
	}
	IEnumerator Shoot(){
		while (true) {
			if (agent.destination != null) {
				distanceToTarget = Vector3.Distance (agent.destination, transform.position);
				while (currTarget != null && distanceToTarget <= maxDistanceToHitTarget) {
					CmdSpawnBullet ();
					yield return new WaitForSeconds (shootRatio);
				}
			}
		}
	}
}