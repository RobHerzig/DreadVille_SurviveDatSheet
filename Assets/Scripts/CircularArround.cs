using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularArround : MonoBehaviour {

	public GameObject Center;
	public float speed;
	public float x;
	public float y;
	public float z;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Center.transform.position, new Vector3 (x, y, z), Time.deltaTime * speed);
	}
}
