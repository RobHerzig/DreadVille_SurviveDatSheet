using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealth : NetworkBehaviour {
	public float maxHealth = 100;
  
    [SyncVar(hook = "OnChangeHealth")]
	private float health;

    public RectTransform healthBar;
	public GameObject HealSphere;

	public void TakeDamage(float dmg)
    {
        health -= dmg;
     
    }


    void setHealthBar(float newRatio)
    {
        float ratioNew = (float)health / (float)maxHealth;
		healthBar.localScale = new Vector3(ratioNew, healthBar.localScale.y, healthBar.localScale.z);
    }

	void OnChangeHealth(float health)
    {
        health = health;
        //setHealthText(health);

        float newRatio = (float)health / (float)maxHealth;
        setHealthBar(newRatio);
    }

    // Use this for initialization
    void Awake () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
		if(health <= 0)
		{
			OnDead ();
		}
	}
	void OnDead(){
		GameController.singleton.currNumberOfEnemies--;
		if(Random.Range(0f,1f)<0.2f) 
			CmdSpawnHeal ();
		GetComponent<Enemy> ().ReleaseXP ();
		Destroy(gameObject);

	}


	[Command]
	void CmdSpawnHeal(){
		GameObject heal = Instantiate (HealSphere,transform.position,Quaternion.identity)as GameObject;
		NetworkServer.Spawn (heal);

	}

}
