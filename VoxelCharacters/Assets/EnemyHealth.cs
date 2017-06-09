using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyHealth : NetworkBehaviour {
    public int maxHealth = 100;
  
    [SyncVar(hook = "OnChangeHealth")]
    private int health = 100;

    public RectTransform healthBar;

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        CheckHealth();
    }

    void CheckHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void setHealthBar(float newRatio)
    {
        //float ratioNew = (float)health / (float)maxHealth;
        healthBar.localScale = new Vector3(newRatio, healthBar.localScale.y, healthBar.localScale.z);
    }

    void OnChangeHealth(int health)
    {
        health = health;
        //setHealthText(health);

        float newRatio = (float)health / (float)maxHealth;
        setHealthBar(newRatio);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
