using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class HealthViewControl : NetworkBehaviour {

    [SyncVar] private int health = 100;
    private Text healthText;

	// Use this for initialization
	void Start () {
        //healthText = GameObject.Find("HealthText").GetComponent<Text>();
        //setHealthText(health);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void setHealthText(int newHealth)
    {
        if (isLocalPlayer)
            healthText.text = newHealth.ToString() + "HP";
    }

    public void DeductHealth (int dmg)
    {
        health -= dmg;
    }
}
