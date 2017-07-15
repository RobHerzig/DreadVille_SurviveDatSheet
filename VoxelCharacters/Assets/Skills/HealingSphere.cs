using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HealingSphere : NetworkBehaviour {

	NetworkInstanceId isFromPlayer;
	float ballDamage = 0;
	float ballHealing = 0;
	float range = 1000000;
	float speed = 1;
	bool isWayBackToPlayer = false;
	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
		GameObject owner = NetworkServer.FindLocalObject (isFromPlayer);
		if (owner != null) {
			if (Vector3.Distance (transform.position, owner.transform.position + owner.transform.up * 0.4f) > range && !isWayBackToPlayer) {
				Debug.Log ("max range " + range + "triggered");
				isWayBackToPlayer = true;
			} else if (Vector3.Distance (transform.position, owner.transform.position + owner.transform.up * 0.4f) < 1f&& isWayBackToPlayer) {
				Debug.Log ("sphere on player");
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
				GetComponent<ParticleSystem> ().enableEmission = false;
				Destroy (gameObject, 1f);
				isWayBackToPlayer = false;
			}
			if (isWayBackToPlayer)
				GetComponent<Rigidbody> ().velocity = (Vector3)(owner.transform.position + transform.up * 0.4f - transform.position).normalized * speed;
		} 
	}

	public void initHealingSphere(float damage,float healing)
	{
		ballDamage = damage;
		ballHealing = healing;
	}
	public void InitRangeSpeed(float range,float speed){
		this.range = range;
		this.speed = speed;
	}
	public void InitID(NetworkInstanceId PlayerID)
	{
		isFromPlayer = PlayerID;
	}
	private void OnTriggerEnter(Collider other)
	{
		if (other.GetComponent<Player>() != null) 
		{
			Debug.Log("HEALING SPHERE HIT " + other.tag);

			if(other.GetComponent<Health>() != null)
			{
				other.GetComponent<Health>().TakeHeal(ballHealing);
			}
		} else if (other.tag == "Enemy")
		{
			Debug.Log("HEALINGSPHERE HIT " + other.tag);
			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(ballDamage);
			}
		}
	}
	public void IsBackToPLayer()
	{
		isWayBackToPlayer = true;
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
