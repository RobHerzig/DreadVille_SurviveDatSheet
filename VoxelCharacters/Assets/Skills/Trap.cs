using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Trap : NetworkBehaviour {

	int TriggerDamage = 0;
	bool triggered = false;
	float TrapDurration = 5;
	float slow = 0;// between 0-1 is %
	float slowDurration = 0;
	 float TrapSizeTriggered = 0;
	[SyncVar(hook = "OnChangeScale")]
	Vector3 scale;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		transform.localScale = scale;
	}

	public void initDamage(int damage)
	{
		TriggerDamage = damage;
	}
	public void initSlow(float slow,float durration)
	{
		this.slow = slow;
		this.slowDurration = durration;
	}
	public void initScaleSize(float size)
	{
		TrapSizeTriggered = size;
		scale = transform.localScale;

	}
	public void initduration(float duration)
	{
		TrapDurration = duration;

	}
	private void OnTriggerEnter(Collider other)
	{
		Debug.Log("HIT " + other.tag + " Trigger Trap");
	 if (other.tag == "Enemy")
		{
			Debug.Log("TriggerTrap " + other.tag);
			if (!triggered) {
				Trigger ();
				if (other.GetComponent<EnemyHealth> () != null) {
					other.GetComponent<EnemyHealth> ().TakeDamage (TriggerDamage);
				}
			}
		}
	}
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Enemy")
			SLow (other.gameObject);
	}

	void SLow(GameObject enemy){
		if (enemy.GetComponent<EnemyTarget> () != null)
			enemy.GetComponent<EnemyTarget> ().SlowThis(slowDurration, slow); 
	}

	void Trigger()
	{
		triggered = true;
		StartCoroutine (TriggerCoruotine(1f,TrapDurration));
	}
	[Command]
	void Cmdscale(Vector3 start,Vector3 end,float time,float elapsedTime){
		scale = Vector3.Lerp(start,end,(elapsedTime / time));
	
	
	}

	IEnumerator TriggerCoruotine(float time,float trapDuration){
		Vector3 startScale = transform.localScale;
		Vector3 endscale = startScale * TrapSizeTriggered;
		float elapsedTime = 0;
		while ((elapsedTime / time) < 1) 
		{
			Cmdscale (startScale, endscale, time, elapsedTime);
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		yield return new WaitForSeconds (trapDuration);
		Destroy (gameObject);
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
	void OnChangeScale(Vector3 scale){
		this.scale = scale;
	}
}
