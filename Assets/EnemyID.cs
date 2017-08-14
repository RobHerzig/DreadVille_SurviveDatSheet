using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemyID : NetworkBehaviour {
    [SyncVar] public string enemyID;
    private Transform myTransform;
	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () {
        SetIdentity();
	}

    void SetIdentity()
    {
        if(myTransform.name == "" || myTransform.name == "EnemyPrototype(Clone)")
        {
            myTransform.name = enemyID;
        }
    }
}
