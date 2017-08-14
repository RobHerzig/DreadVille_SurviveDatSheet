using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerDeath : NetworkBehaviour {

    private Health healthScript;
    private Image deathImage;
	// Use this for initialization
	void Start () {
        //TODO: Create death image
        deathImage = GameObject.Find("DeathImage").GetComponent<Image>();
        healthScript = GetComponent<Health>();
        healthScript.EventDie += DisablePlayer;
	}

    void OnDisable()
    {
        //healthScript.EventDie -= DisablePlayer;
    }

    void DisablePlayer()
    {
        Debug.Log("DISABLING PLAYER ");
        //TODO: Disable all the stuff
        GetComponent<Player>().enabled = false;
        GetComponent<Shoot>().enabled = false;
        GetComponent<MovementControllerSatanist>().StopAllMovement();
        GetComponent<MovementControllerSatanist>().enabled = false;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach(Renderer r in renderers)
        {
            r.enabled = false;
        }

        healthScript.isDead = true;

        if(isLocalPlayer)
        {
            if(deathImage != null)
                deathImage.enabled = true;
            Debug.Log("ALLOW RESPAWN");
            //TODO: Respawn Button?
            GameObject.Find("GameManager").GetComponent<GameManager>().RespawnButton.SetActive(true);
        }
    }
}
