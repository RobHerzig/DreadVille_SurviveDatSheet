using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Shoot : NetworkBehaviour {

    private int damage = 20;
    private float range = 200;

    [SerializeField]
    private Transform camTransform;

    private RaycastHit hit;
	
	// Update is called once per frame
	void Update () {
        CheckIfShooting();
	}

    void CheckIfShooting()
    {
        if (!isLocalPlayer)
            return;

        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shot();
        }
    }

    void Shot()
    {
        Debug.DrawRay(transform.position + transform.forward * 0.5f + transform.up * 0.3f,
            transform.forward * 200f, Color.green, 2, false);
        if (Physics.Raycast(transform.position + transform.forward * 0.5f + transform.up * 0.3f,
            transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.tag + " HIT");
            if(hit.transform.tag == "Other_Player")
            {
                Debug.LogWarning("SHOT OTHER PLAYER" + hit.transform.tag + "/" + hit.transform.name);
                //hit.transform.gameObject.GetComponent<Health>().TakeDamage(5);
                string uIdentity = hit.transform.name;
                CmdTellServerWhoWasShot(hit.transform.gameObject, damage);
            }
            else if (hit.transform.tag == "Enemy")
            {
                Debug.LogWarning("SHOT OTHER PLAYER" + hit.transform.tag + "/" + hit.transform.name);
                //hit.transform.gameObject.GetComponent<Health>().TakeDamage(5);
                string uIdentity = hit.transform.name;
                CmdTellServerWhichEnemyWasShot(hit.transform.gameObject, damage);
            }
        }
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
