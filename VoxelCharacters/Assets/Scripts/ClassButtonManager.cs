using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassButtonManager : MonoBehaviour {

	public void selectSoliderTank(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 0;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 0;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
	public void selectSoliderMeele(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 0;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 1;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
	public void selectHunterRange(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 1;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 0;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
	public void selectHunterMeele(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 1;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 1;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
	public void selectClericHealer(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 2;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 0;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
	public void selectClericMage(){
		GameObject.FindObjectOfType<ClassSelection>().PlayerClass = 2;
		GameObject.FindObjectOfType<ClassSelection>().classSpec = 1;
		GameObject.FindObjectOfType<ClassSelection>().menueState = 3;
	}
}

