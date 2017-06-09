using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {

    int ballDamage = 0;
    bool triggered = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void initDamage(int damage)
    {
        ballDamage = damage;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT " + other.tag + " WITH FIREBALL");
        if (other.tag == "Other_Player") 
        {
            Debug.Log("FIREBALL HIT " + other.tag);
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
        triggered = true;
    }

    public bool GetTriggered()
    {
        return triggered;
    }
}
