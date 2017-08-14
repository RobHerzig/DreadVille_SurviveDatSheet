using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BilldBoard : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void Update()
    {
        if(Camera.main != null)
        {
            //transform.LookAt(Camera.main.transform.position);
            transform.rotation = Quaternion.Inverse(Camera.main.transform.rotation);
            //transform.rotation = Camera.main.transform.rotation;
        }

    }
}
