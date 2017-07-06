using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
	public Color backgroundColor;
	public bool joinedSession = false;
	public int gameState = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (joinedSession) {
			switch (gameState) {
			case 0:
				gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ().color = Color.Lerp (gameObject.transform.GetChild (0).gameObject.GetComponent<Image> ().color, backgroundColor, Time.deltaTime);
				break;	
			}
		}
	}
	public void ChoseClass(){
	
	}
}

