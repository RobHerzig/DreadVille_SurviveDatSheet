using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour {

	 Button primarySkill;
	 Button secoundarySkill;
	 Button teritraySkill;
	 Button UltimateSkill;

	internal RectTransform XPBar;
	Text currLevel;



	public void UpdateCooldownOnButton(int ButtonID,float currCooldown,float Cooldown)
	{
		switch (ButtonID) {
		case 1: 
			primarySkill.transform.GetChild (0).GetComponent<Image> ().fillAmount = currCooldown / Cooldown;
			break;
		case 2: 
			secoundarySkill.transform.GetChild (0).GetComponent<Image> ().fillAmount =currCooldown / Cooldown;
			break;
		case 3: 
			teritraySkill.transform.GetChild (0).GetComponent<Image> ().fillAmount = currCooldown / Cooldown;
			break;case 4: 
			UltimateSkill.transform.GetChild (0).GetComponent<Image> ().fillAmount =currCooldown / Cooldown;
			break;

		}
	}
	public void UpdateLevel( int level){
	
		currLevel.text = level+""; 
	}
	void Awake(){
		XPBar = transform.GetChild (6).GetComponent<RectTransform> ();
		currLevel = transform.GetChild (4).transform.GetChild (0).GetComponent<Text> ();
		primarySkill = transform.GetChild (0).GetComponent<Button> ();
		secoundarySkill = transform.GetChild (1).GetComponent<Button> ();
		teritraySkill = transform.GetChild (2).GetComponent<Button> ();
		UltimateSkill = transform.GetChild (3).GetComponent<Button> ();


	}

}
