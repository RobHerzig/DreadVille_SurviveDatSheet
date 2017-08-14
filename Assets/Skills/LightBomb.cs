using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LightBomb : Projectile {


	Transform Player;
	float ballDamage = 0;

	float range = 0;
	float speed = 1;
	float angle= 0;
	Transform target;
	float lifeTime = 2;
	bool isCirculating = true;
	Vector3 newVelocity;
	float acceleration;

	// Use this for initialization
	void Start () {

	}

	public void initHealingSphere(float damage,float range,float speed,float roationspeed,float lifetime,float acceleration,Transform player)
	{
		ballDamage = damage;
		this.range = range;
		this.angle = roationspeed;
		this.speed = speed;
		this.lifeTime = lifetime;
		this.acceleration = acceleration;
		Player = player;
			

	}
	// Update is called once per frame
	void Update () {

		lifeTime -= Time.deltaTime;
		if (lifeTime < 0)
			isCirculating = false;
		if (isCirculating || target == null)
			CircularMotion ();
		if (target == null && !isCirculating) {
			SearchForTarget ();
		}
		if (target != null) {
			GetComponent<Rigidbody> ().isKinematic = false;
			GetComponent<Rigidbody> ().velocity = Vector3.Lerp (GetComponent<Rigidbody> ().velocity, newVelocity, Time.deltaTime * acceleration);
			Destroy (gameObject, 2f);
		}
		
	}
	void SearchForTarget()
	{
		LayerMask raycastLayer = 1 << LayerMask.NameToLayer("Enemy");
		Collider[] hitColliders = Physics.OverlapSphere(transform.position, range, raycastLayer);
		if(hitColliders.Length > 0)
		{
			int randomInt = Random.Range(0, hitColliders.Length);
			target = hitColliders[randomInt].transform;
			Debug.Log ("lol");
			newVelocity = speed*(target.position - transform.position).normalized;
		}


	}

	void CircularMotion(){
		transform.RotateAround (transform.parent.transform.position, Vector3.up, Time.deltaTime * angle);
	}



	protected override void TriggerEnter(Collider other)
	{
	if (other.tag == "Enemy")
		{
			Debug.Log("HEALINGSPHERE HIT " + other.tag);
			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(ballDamage);
			}
			gameObject.GetComponent<ParticleSystem>().enableEmission = false;
			Destroy (gameObject, 1f);
		}
	}




	[Command]
	void CmdTellServerWhoWasShot(GameObject go, int dmg)
	{
		go.GetComponent<Health>().TakeDamage(dmg);
	}
	[Command]
	void CmdTellServerWhichEnemyWasShot(GameObject go, int dmg)
	{
		//GameObject go = GameObject.Find(ID);
		go.GetComponent<EnemyHealth>().TakeDamage(dmg);
	}
}
