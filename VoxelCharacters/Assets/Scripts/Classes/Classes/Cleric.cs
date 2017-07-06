using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cleric : Class {
	
	public Cleric(byte classState,ClassModel model)
	{
		this.model = model;
		prefab = model.ClericPrefab;
		if (classState == 1) {
			SKillSetOne ();
		} else if(classState == 2) {
			SkillSetTwo ();
		}
	}
	public override void ChangeSKillSet (byte switchToSkillSet){
		
		if (currSKillSet != switchToSkillSet && switchToSkillSet == 1) {
			ClearSkillSet ();
			SKillSetOne ();
		} else if (currSKillSet != switchToSkillSet && switchToSkillSet== 2){
			ClearSkillSet ();
			SkillSetTwo ();
		}
	}

	private void SKillSetOne(){
		currSKillSet = 1;
		maximumHealth =	model.CHmaximumHealth;
		movmentspeed = model.CHmovmentspeed;
		armor = model.CHarmor;
		magicResistence = model.CHmagicResistence;

		firstHand = new GreatStaff (model.waponModel);
		secoundHand = firstHand;

		mainSkill = new CHBasicSkill(model.clericSkillModel);
		secoundSkill = new CHSecoundSkill (model.clericSkillModel);
		ultimate = new CHUltimate (model.clericSkillModel);
	
	}
	private void SkillSetTwo(){
		currSKillSet = 2;
		maximumHealth = model.CMmaximumHealth;
		movmentspeed = model.CMmovmentspeed;
		armor = model.CMarmor;
		magicResistence = model.CMmagicResistence;

		firstHand = new SpellBook (model.waponModel);
		secoundHand = firstHand;

		mainSkill = new CMBasicSkill (model.clericSkillModel);
		secoundSkill = new CMSecoundSkill (model.clericSkillModel);
		ultimate = new CMUltimate (model.clericSkillModel);
	}

}
