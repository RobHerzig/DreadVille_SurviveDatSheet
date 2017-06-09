using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum beatState
{
    IDLE,
    STRIKE_NORMAL,
    STRIKE_STRONG
};

public class GreatSword : MonoBehaviour {

   
    beatState state = beatState.IDLE;

    public int NormalDamage = 10;
    public int StrongDamage = 20;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("SWORD TRIGGERED" + other.tag);
        if(other.tag == "Other_Player")
        {
            other.GetComponent<Player>().Beat(50f);
        }

        if(other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(10);
        }
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setState(beatState newState)
    {
        state = newState;
    }
}
