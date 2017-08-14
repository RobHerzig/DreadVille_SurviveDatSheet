using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GrowingSphere : Projectile {

	float ballDamage = 0;
	bool triggered = false;
	int dps= 1;
	NetworkInstanceId isFromPlayer;
	ParticleSystem blue;
	ParticleSystem red;
	ParticleSystem white;
	float currsize = 1;
	float lerpSize= 1;
	float growingScale = 0.6f;
	float maxSize = 5;
	float triggerBonusScale = 1;
	float detonatationRadius = 1;

	// Use this for initialization
	void Start () {

		white = transform.GetChild (0).GetComponent<ParticleSystem> ();
		blue = transform.GetChild (1).GetComponent<ParticleSystem> ();
		red = transform.GetChild (2).GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		
		if (transform.GetChild (1).gameObject.activeSelf || transform.GetChild (2).gameObject.activeSelf)
			transform.GetChild (0).gameObject.SetActive (false);
		grow ();
		if (currsize > maxSize)
			StartCoroutine( explode ());
	}
	void grow(){
		if(currsize<lerpSize)
			currsize += Time.deltaTime * growingScale;
		transform.localScale = Vector3.Lerp (transform.localScale, Vector3.one * currsize*0.1f, Time.deltaTime);
		white.startLifetime = currsize;
		red.startLifetime = currsize;
		blue.startLifetime = currsize;
	}
	public void InitSphere(float damage,int NumberofSpellsToexplode,float detonationsradius,float maxSize){
		ballDamage = damage;
		triggerBonusScale = NumberofSpellsToexplode/(maxSize -currsize);
		detonatationRadius = detonationsradius;
		this.maxSize = maxSize;

	}
	IEnumerator explode(){
		blue.startSpeed = 5;
		red.startSpeed = 5;

		yield return new WaitForSeconds (1f);
		dps = 4;
		transform.localScale = Vector3.one * 0.1f*detonatationRadius;
		red.enableEmission = false;
		blue.enableEmission = false;
		yield return new WaitForSeconds (currsize-2);
		Destroy (gameObject);
	}
	public void initDamage(float damage)
	{
		ballDamage = damage;
	}
	public void InitID(NetworkInstanceId PlayerID)
	{
		isFromPlayer = PlayerID;
	}
	protected override void TriggerStay(Collider other)
	{
		if (other.tag == "Enemy") {
			if (other.GetComponent<EnemyHealth> () != null) {
				if(transform.GetChild (2).gameObject.activeSelf)
					other.GetComponent<EnemyHealth> ().TakeDamage (dps*2);
				else
					other.GetComponent<EnemyHealth> ().TakeDamage (dps);
			}
			if (transform.GetChild (1).gameObject.activeSelf)
				other.GetComponent<EnemyTarget> ().SlowThis (1f, 0.8f);
		}
	}

	protected override void TriggerEnter(Collider other)
	{
	if (other.tag == "Enemy")
		{
			if (other.GetComponent<EnemyHealth>() != null)
			{
				other.GetComponent<EnemyHealth>().TakeDamage(ballDamage);
			}
		}
		if (other.GetComponent<FireBall> () != null) {
			transform.GetChild (2).gameObject.SetActive (true);
			if (lerpSize < maxSize)
				lerpSize += triggerBonusScale;
		}
		if (other.GetComponent<FreezeCone> () != null) {
			transform.GetChild (1).gameObject.SetActive (true);
			if (lerpSize < maxSize)
				lerpSize += triggerBonusScale;
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

