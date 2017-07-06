using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Class : MonoBehaviour {

	internal ClassModel model;

	internal byte currSKillSet;
	internal GameObject prefab;
	internal float maximumHealth;
	internal float currentHealth;

	internal float movmentspeed;

	internal float armor;
	internal float magicResistence;

	internal Wapon firstHand;
	internal Wapon secoundHand;

	internal Skill mainSkill;
	internal Skill secoundSkill;
	internal Skill ultimate;

	public abstract void  ChangeSKillSet (byte switchToSkillSet);
	public void ClearSkillSet(){
		Destroy (firstHand);
		Destroy (secoundHand);
		Destroy (mainSkill);
		Destroy (secoundSkill);
		Destroy (ultimate);

	}
}
