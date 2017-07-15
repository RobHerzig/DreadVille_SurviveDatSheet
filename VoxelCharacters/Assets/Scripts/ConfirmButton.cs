using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmButton : MonoBehaviour {

	public void Confirm(){
		GameObject.FindObjectOfType<ClassSelection>().menueState = 6;
	}
	public void Cancle(){
		GameObject.FindObjectOfType<ClassSelection>().menueState = 8;
	}
	public void JoinGame(){
		GameObject.FindObjectOfType<ClassSelection>().menueState = 11;
	}
	public void HostGame(){
		GameObject.FindObjectOfType<ClassSelection>().menueState = 10;
	}
}
