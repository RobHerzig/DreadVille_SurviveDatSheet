using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTrailRing : MonoBehaviour {

    public Vector3 speedVector;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(speedVector * Time.deltaTime);
	}
}
