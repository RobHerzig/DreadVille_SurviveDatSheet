using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpellDeathBeam : Skill {

	public GameObject deathBeam;
	public float maxDuration = 5f;
	private GameObject currBeam;



	public override void TriggerSKill ()
	{
		CmdActiveDeathBeam ();

	}



	[Command]
	void CmdDestroyBeam(float time){
		time -= Time.deltaTime;

		if (currBeam != null) {
			ParticleSystem[] particles = currBeam.transform.GetComponentsInChildren<ParticleSystem> ();
			foreach (ParticleSystem particle in particles) {
				particle.enableEmission = false;
			}
			//if (time < 0)
				//currBeam.GetComponent<DeathBeam> ().destroyThis = true;
		}
	}
	[Command]
	public void CmdActiveDeathBeam()
	{
		
		GameObject instance =Instantiate(deathBeam, transform.position + transform.up * 0.3f+transform.forward*-0.1f, transform.rotation) as GameObject;
		DeathBeam beamScript = instance.GetComponentInChildren<DeathBeam>();
		instance.transform.parent = gameObject.transform;
		beamScript.initDamage(damage);
		beamScript.InitID (gameObject.GetComponent<NetworkIdentity>().netId);
		beamScript.SetOrigin (gameObject);
		instance.transform.parent = transform;
		NetworkServer.SpawnWithClientAuthority (instance, gameObject);
		RpcSetParent (gameObject, instance);
		Destroy (instance, maxDuration);

	}
	[ClientRpc]
	void RpcSetParent(GameObject parent ,GameObject child){
		child.transform.parent = parent.transform;
	}

}
