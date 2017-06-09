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
    private float radius = 100;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        myTransform = transform;
        raycastLayer = 1 << LayerMask.NameToLayer("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        SearchForTarget();
        MoveToTarget();
	}

    void SearchForTarget()
    {
        if(!isServer)
        {
            return;
        }
        
        if(targetTransform == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(myTransform.position, radius, raycastLayer);
            if(hitColliders.Length > 0)
            {
                int randomInt = Random.Range(0, hitColliders.Length);
                targetTransform = hitColliders[randomInt].transform;
            }
        }

        if(targetTransform != null && targetTransform.GetComponent<Health>().isDead)
        {
            targetTransform = null;
        }
    }

    void MoveToTarget()
    {
        if(targetTransform != null && isServer)
        {
            SetNavDestination(targetTransform);
        }
    }

    void SetNavDestination(Transform destination)
    {
        agent.SetDestination(destination.position);
    }
}
