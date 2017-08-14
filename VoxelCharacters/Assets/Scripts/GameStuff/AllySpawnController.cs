using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class AllySpawnController : NetworkBehaviour{

	public Transform spawnPoint;
	public GameObject Ally;
	public List<Transform> destinations;
	List<Transform> occuipdedDestionations;


	void SpawnAlly(GameObject ally,Transform destination){
		Quaternion lookRotation = Quaternion.LookRotation( destination.position -  spawnPoint.transform.position); 
		GameObject Instance =Instantiate (ally, spawnPoint.position, lookRotation)as GameObject;
		Instance.GetComponent<TargetSystem> ().originTarget = destination;
		NetworkServer.Spawn (Instance);
	}

	// Use this for initialization
	void Start () {
		if (!isServer) {
			return;
		}
		occuipdedDestionations = new List<Transform> ();
		InitAllAllys ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	void InitAllAllys(){
		foreach (Transform destination in destinations) {
			SpawnAlly (Ally, destination);
			occuipdedDestionations.Add (destination);
		}
		destinations.Clear ();
	}
}
