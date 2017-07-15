using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGate : MonoBehaviour {
	Transform parent;
	bool open = false;
	public bool isRightDoor = false;
	float time = 0;
	public void Open (){
		open = true;
		parent = gameObject.transform.parent;
	}
	// Update is called once per frame
	void Update () {
		if (time< 1&&open) {
			if(isRightDoor)
				transform.RotateAround (parent.position, Vector3.up, 80*Time.deltaTime);
			else
				transform.RotateAround (parent.position, Vector3.down, 80*Time.deltaTime);
			time += Time.deltaTime;
		}
	}
}
