using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FireBall : Projectile {

	float ballDamage = 0;
    bool triggered = false;
	NetworkInstanceId isFromPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void initDamage(float damage)
    {
        ballDamage = damage;
    }
	public void InitID(NetworkInstanceId PlayerID)
	{
		isFromPlayer = PlayerID;
	}
	protected override void TriggerEnter(Collider other)
    {
        
		if (other.GetComponent<Player>() != null && !other.GetComponent<Player>().PlayerID.Equals(isFromPlayer)) 
        {
			//Debug.Log("FIREBALL HIT " + GetComponent<Player>().PlayerID);
            explode();
            if(other.GetComponent<Health>() != null)
            {
                other.GetComponent<Health>().TakeDamage(ballDamage);
            }
        } else if (other.tag == "Enemy")
        {
            Debug.Log("FIREBALL HIT " + other.tag);
            explode();
            if (other.GetComponent<EnemyHealth>() != null)
            {
                other.GetComponent<EnemyHealth>().TakeDamage(ballDamage);
            }
        }
    }

    void explode()
    {
		//gameObject.GetComponent<ParticleSystem>().enableEmission = false;
		//gameObject.GetComponent<Rigidbody> ().velocity = Vector3.zero;
		Destroy(gameObject, 2f);
    }

    public bool GetTriggered()
    {
        return triggered;
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
}
