using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ClassSelection : MonoBehaviour {

	internal bool wasConnected = false;
	internal int menueState = 0;
	internal byte PlayerClass;
	internal byte classSpec;
	private GameObject playerPrefab;
	GameObject SelectionMenue;
	GameObject confirmMenue;
	GameObject connMenue;
	internal myNetworkManager manager;




	void Awake () {
		SelectionMenue =transform.GetChild (0).gameObject; 
		confirmMenue = transform.GetChild (1).gameObject;
		connMenue = transform.GetChild (2).gameObject;
	}

	// Update is called once per frame
	void Update () {
		
		if (menueState <= 11)
			Selection ();
	}



	void FadeInOut(CanvasGroup canvas,float speed,bool isFadedingIn){
		//Debug.Log ("fade");
		if(isFadedingIn)
			canvas.alpha += Time.deltaTime*speed;
		else
			canvas.alpha -= Time.deltaTime*speed;
	}
	void Selection(){
		
		switch (menueState) {
		case 0:
				SelectionMenue.GetComponent<CanvasGroup> ().alpha = 0;
				SelectionMenue.gameObject.SetActive (true);
				menueState = 1;
			break;
		case 1:
			if (SelectionMenue.GetComponent<CanvasGroup> ().alpha < 1)
				FadeInOut (SelectionMenue.GetComponent<CanvasGroup> (), 2, true);
			else
				menueState = 2;
			break;
		case 2:
			//------(select class in classselectionButton)----------------
			break;


		case 3:
			if (SelectionMenue.GetComponent<CanvasGroup> ().alpha > 0)
				FadeInOut (SelectionMenue.GetComponent<CanvasGroup> (), 2, false);
			else {
				SelectionMenue.GetComponent<CanvasGroup> ().alpha = 1;
				SelectionMenue.gameObject.SetActive (false);
				confirmMenue.gameObject.SetActive (true);
				confirmMenue.GetComponent<CanvasGroup> ().alpha = 0;
				menueState = 4;
			}
			break;
		case 4:
			if (confirmMenue.GetComponent<CanvasGroup> ().alpha < 1)
				FadeInOut (confirmMenue.GetComponent<CanvasGroup> (), 2, true);
			else
				menueState = 5;
			break;
		case 5:
			//--------(confirm class)--------------------
			break;
		case 6:
			if (confirmMenue.GetComponent<CanvasGroup> ().alpha > 0)
				FadeInOut (confirmMenue.GetComponent<CanvasGroup> (), 2, false);
			else {
				confirmMenue.gameObject.SetActive (false);
				menueState = 7;
			}
			break;
		case 7:
			
			if (connMenue.GetComponent<CanvasGroup> ().alpha < 1)
				FadeInOut (connMenue.GetComponent<CanvasGroup> (), 2, true);
			else {
				connMenue.SetActive (true);
				menueState = 9;
			}
			break;
		case 8:
			if (confirmMenue.GetComponent<CanvasGroup> ().alpha > 0)
				FadeInOut (confirmMenue.GetComponent<CanvasGroup> (), 2, false);
			else {
				confirmMenue.gameObject.SetActive (false);
				menueState = 0;
			}
			break;
		case 9:
			break;
		case 10:
			if (confirmMenue.GetComponent<CanvasGroup> ().alpha > 0)
				FadeInOut (confirmMenue.GetComponent<CanvasGroup> (), 2, false);
			else {
				//menueState = 0;
				manager.isHosting = true;
				gameObject.SetActive (false);

			}
			break;
		case 11:
			if (confirmMenue.GetComponent<CanvasGroup> ().alpha > 0)
				FadeInOut (confirmMenue.GetComponent<CanvasGroup> (), 2, false);
			else {
				//menueState = 0;
				manager.isClientcon = true; 
				gameObject.SetActive (false);
			}
			break;

		}

	}
		

}
