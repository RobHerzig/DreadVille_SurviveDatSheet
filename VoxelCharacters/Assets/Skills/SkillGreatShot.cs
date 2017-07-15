using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SkillGreatShot : NetworkBehaviour {

	public GameObject bullet;
	public int Power = 15;
	public int NumberOfBullets = 4;
	public float bulletSpeed = 5f;
	public float bulletRange = 10f;
	public float jumpRange = 5;
	private bool isAtTarget = true;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(!isLocalPlayer)
		{
			return;
		}

		if(Input.GetKeyDown(KeyCode.C)&& isAtTarget)
		{
			CmdShootBullet(NumberOfBullets,bulletSpeed,bulletRange,Power);
			StartCoroutine(JumpCoroutine(jumpRange,1f));
		}
	}

	[Command]
	public void CmdShootBullet(int numberOfBullets,float bulletSpeed,float bulletRange,int power)
	{
		Vector3 angle = new Vector3 (0, 90, 0) / numberOfBullets;
		for (int i = -numberOfBullets/2 ; i < numberOfBullets/2; i++) {
			
			GameObject instance = Instantiate (bullet, transform.position, gameObject.transform.rotation) as GameObject;
			instance.transform.rotation = Quaternion.Euler( instance.transform.eulerAngles + i * angle);
			instance.transform.parent = gameObject.transform;
			instance.GetComponent<Rigidbody> ().velocity = instance.transform.forward * bulletSpeed;
			GreatShot shotscript = instance.GetComponentInChildren<GreatShot> ();
			shotscript.initDamage (power);
			NetworkServer.Spawn (instance);
			Destroy (instance, bulletRange / bulletSpeed);
		}
	}
	IEnumerator JumpCoroutine(float range,float time)
	{
		isAtTarget = false;
		float elapsedTime = 0;
		Transform player = gameObject.transform;
		Vector3 startPostion = player.position;
		Vector3 targetPosition =player.position + player.forward.normalized * -range;
		while ((elapsedTime / time) < 1f) {
			player.position = Vector3.Lerp (startPostion, targetPosition, (elapsedTime / time));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		isAtTarget = true;
	}


}
