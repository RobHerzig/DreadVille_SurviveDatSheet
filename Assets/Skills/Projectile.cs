using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public abstract class Projectile :  NetworkBehaviour {

	internal NetworkInstanceId originID;
	internal float aggroValue= 1;
	public void SetOrigin(GameObject origin){
	
		originID = origin.GetComponent<NetworkIdentity> ().netId;
	}
	private void OnTriggerEnter(Collider other){
		TriggerEnter (other);
		if (NetworkServer.FindLocalObject (originID)!= null && NetworkServer.FindLocalObject (originID).GetComponent<Player> () != null && other.GetComponent<Enemy>() != null) {
			if (other.GetComponent<AggroSystem> () != null) {
				other.GetComponent<AggroSystem> ().SetAggro (originID, aggroValue);
			}
		}
	
	}
	private void OnTriggerStay(Collider other)
	{
		TriggerStay (other);
		if (NetworkServer.FindLocalObject (originID)!= null &&  NetworkServer.FindLocalObject (originID).GetComponent<Player> () != null&& other.GetComponent<Enemy>() != null) {
			if (other.GetComponent<AggroSystem> () != null) {
				other.GetComponent<AggroSystem> ().SetAggro (originID, aggroValue);
			}
		}
	}
	protected virtual void TriggerEnter(Collider other){
	
	}
	protected virtual void TriggerStay(Collider other){

	}
}
