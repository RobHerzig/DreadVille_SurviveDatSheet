using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player :  NetworkBehaviour{

    protected float DEF = 10f, MAXHP = 100f, CURHP = 100f, ATK = 10f;

	// Use this for initialization
	void Awake () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Beat(float directDamage)
    {
        int actualDamage = (int)directDamage - (int)DEF;
        GetComponent<Health>().TakeDamage(actualDamage);
        //CURHP -= Mathf.Max(0f, actualDamage);
        //CURHP = Mathf.Max(0f, CURHP);
        //Debug.LogWarning("NEW CURRENT HP OF " + gameObject.name + ": " + CURHP);
        //refreshHP();
    }

    void refreshHP()
    {
        //float hpPercentage = CURHP / MAXHP;
        //if(HPBar != null)
        //{
        //    HPBar.DisplayHealth(hpPercentage);
        //}
        //GetComponent<Health>().
    }
}
