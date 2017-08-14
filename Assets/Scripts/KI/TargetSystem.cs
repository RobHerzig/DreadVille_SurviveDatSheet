using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class TargetSystem : NetworkBehaviour {

	public bool isEnemy = true;
	private NavMeshAgent agent;
	private Transform myTransform;
	private Transform targetTransform;
	private LayerMask raycastLayer1;
	private LayerMask raycastLayer2;
	public float radius = 1;
	public float maxDistance = 5;
	private float normalSpeed = 1.5f;
	public bool isCroudControlled;
	internal bool isSlowed;
	public Transform originTarget; 


	// Use this for initialization
	void Start () {
		if (isEnemy) {
			raycastLayer1 = 1 << LayerMask.NameToLayer ("Player");
			raycastLayer2 = 1 << LayerMask.NameToLayer ("Ally");
			originTarget = GameObject.Find ("PlayerBase").transform;
		} else {
			raycastLayer1 =1 << LayerMask.NameToLayer ("Enemy");
		}
		if(originTarget != null)
			targetTransform = originTarget.transform;
		
		agent = GetComponent<NavMeshAgent>();
		myTransform = transform;


	}

	// Update is called once per frame
	void FixedUpdate () {
		if (targetTransform == null && originTarget != null)
			targetTransform = originTarget;

		SearchForTarget();
		MoveToTarget();
	}

	void SearchForTarget()
	{
		if(!isServer)
		{
			return;
		}
		if (GetComponent<AggroSystem> ().Target != null) {
			targetTransform = GetComponent<AggroSystem> ().Target.transform;
		}

		if(targetTransform == originTarget|| targetTransform == null)
		{
			if (!RayCastForTarget (raycastLayer1))
				RayCastForTarget (raycastLayer2);
		}

		if (isEnemy) {
			if (targetTransform != originTarget &&targetTransform.GetComponent<Health> () != null&& targetTransform.GetComponent<Health> ().isDead && originTarget != null) {
				targetTransform = originTarget;
			}
		} else {
			if (targetTransform != originTarget && Vector3.Distance (myTransform.position, targetTransform.position) > maxDistance)
				targetTransform = originTarget;
		}
	}
	bool RayCastForTarget(LayerMask raycastLayer){
		Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);
		if(hitColliders.Length > 0)
		{
			int randomInt = Random.Range(0, hitColliders.Length);
			targetTransform = hitColliders[randomInt].transform;
			return true;
		}
		return false;
	}
	void MoveToTarget()
	{
		if(targetTransform != null && isServer &&!isCroudControlled)
		{
			SetNavDestination(targetTransform);
		}
	}

	public void CroudControllThis(float durration)
	{
		StartCoroutine(CCCoroutine (durration));
	}
	IEnumerator CCCoroutine(float durration)
	{
		isCroudControlled = true;
		yield return new WaitForSeconds (durration);
		isCroudControlled = false;
	}

	public void SlowThis(float durration,float percent){
		if (!isSlowed) {
			StartCoroutine (SlowCoroutine (durration, percent));
		}
	}

	IEnumerator SlowCoroutine(float durration,float percent){
		agent.speed = normalSpeed * (1f - percent);
		yield return new WaitForSeconds (durration);
		agent.speed = normalSpeed;
		isSlowed = false;
	}

	void SetNavDestination(Transform destination)
	{
		agent.SetDestination(destination.position);
	}
}
