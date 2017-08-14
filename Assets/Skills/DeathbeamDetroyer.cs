using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeathbeamDetroyer : NetworkBehaviour {
	float time = 0;
	bool distableParticles = false;

	[Command]
	public void CmdDestroyBeam(){
		time -= Time.deltaTime;
		if (!distableParticles) {
			ParticleSystem[] particles = transform.GetComponentsInChildren<ParticleSystem> ();
			foreach (ParticleSystem particle in particles) {
				particle.enableEmission = false;
			}
		}
		if(time <0 )
			NetworkServer.Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			if(Input.GetKeyUp(KeyCode.Alpha3))
				{
				CmdDestroyBeam();
				}
		}
	}
}
