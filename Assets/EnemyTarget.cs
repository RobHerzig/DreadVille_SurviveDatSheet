using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;

public class EnemyTarget : NetworkBehaviour {

    private NavMeshAgent agent;
    private Transform myTransform;
    private Transform targetTransform;
    private LayerMask raycastLayer;
	public float radius = 1;
	private float normalSpeed = 1.5f;
	public bool isCroudControlled;
	internal bool isSlowed;
	public Transform PlayerBase; 


	// Use this for initialization
	void Start () {
		PlayerBase = GameObject.Find ("PlayerBase").transform;
		targetTransform = PlayerBase.transform;
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        raycastLayer = 1 << LayerMask.NameToLayer("Player");
		float distanceToPlayer;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (targetTransform == null)
			targetTransform = PlayerBase;
		
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
			
		if(targetTransform == PlayerBase)
        {
            Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);
            if(hitColliders.Length > 0)
            {
                int randomInt = Random.Range(0, hitColliders.Length);
                targetTransform = hitColliders[randomInt].transform;
            }
        }

		if(targetTransform != PlayerBase && targetTransform.GetComponent<Health>().isDead)
        {
			targetTransform = PlayerBase;
        }
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
