using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerRespawn : NetworkBehaviour {

    private Health healthScript;
    private Image deathImage;
    private GameObject respawnButton;

	// Use this for initialization
	void Start () {
        healthScript = GetComponent<Health>();
        deathImage = GameObject.Find("DeathImage").GetComponent<Image>();
        setRespawnButton();
        healthScript.EventRespawn += EnablePlayer;
	}

    void setRespawnButton()
    {
        if(isLocalPlayer)
        {
            respawnButton = GameObject.Find("GameManager").GetComponent<GameManager>().RespawnButton;
            respawnButton.GetComponent<Button>().onClick.AddListener(commenceRespawn);
            respawnButton.SetActive(false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EnablePlayer()
    {
        //TODO: Disable all the stuff
        GetComponent<Player>().enabled = true;
        GetComponent<Shoot>().enabled = true;
        GetComponent<SpellFireball>().enabled = true;
        GetComponent<MovementControllerSatanist>().enabled = true;

        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in renderers)
        {
            r.enabled = true;
        }


        if (isLocalPlayer)
        {
            if (deathImage != null)
                deathImage.enabled = false;
            respawnButton.SetActive(false);
        }
    }

    private void OnDisable()
    {
        //healthScript.EventRespawn -= EnablePlayer;
    }

    void commenceRespawn()
    {
        CmdRespawnOnServer();
    }

    [Command]
    void CmdRespawnOnServer()
    {
        healthScript.ResetHealth();
    }
}
