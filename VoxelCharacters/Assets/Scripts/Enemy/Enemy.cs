using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
public abstract class Enemy : NetworkBehaviour,IComparer<Enemy> {

	public float MaxHealth = 100f;
	public float damage= 1;
	public int value = 1;
	public float StoppingDistance = 0.5f;

	public float XPRadius = 20f;
	public float XPValue = 10f;
	protected NavMeshAgent agent;
	protected float distanceToTarget;
	public float maxDistanceToHitTarget= 0.6f;
	public Animator anim;

	internal Vector3 currTarget;

	void Awake(){
		ScaleEenemy ();
		gameObject.AddComponent<EnemyTarget> ();
	}
	protected void  ScaleEenemy(){
		damage += GameController.singleton.WaveCounter*GameController.singleton.EnemyScaleWithWave *damage;
		MaxHealth += GameController.singleton.WaveCounter*GameController.singleton.EnemyScaleWithWave *MaxHealth;
	}

	// Use this for initialization
	public int Compare (Enemy x, Enemy y)
	{
		if (x.value > y.value)
			return 1;
		else if (x.value < y.value)
			return -1;
		else
			return 0;
	
	}


	public void ReleaseXP(){
		LayerMask	raycastLayer = 1 << LayerMask.NameToLayer("Player");
		Collider[] hitColliders = Physics.OverlapSphere(gameObject.transform.position, XPRadius, raycastLayer);
		foreach (Collider player in hitColliders) {
			player.GetComponent<Player> ().currXP += XPValue;
		}

	
	
	}
	public virtual void Init()
	{

	}
}
