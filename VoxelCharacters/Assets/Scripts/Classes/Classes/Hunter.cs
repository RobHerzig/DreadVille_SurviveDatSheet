using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : Class {

	public Hunter(byte classState,ClassModel model)
	{
		this.model = model;
		prefab = model.HunterPrefab;
		if (classState == 1) {
			SkillSetOne ();
		} else if(classState == 2) {
			SkillSetTwo ();
		}
	}
	public override void ChangeSKillSet (byte switchToSkillSet){

		if (currSKillSet != switchToSkillSet && switchToSkillSet == 1) {
			ClearSkillSet ();
			SkillSetOne ();
		} else if (currSKillSet != switchToSkillSet && switchToSkillSet== 2){
			ClearSkillSet ();
			SkillSetTwo ();
		}
	}

	private void SkillSetOne(){
		currSKillSet = 1;
		maximumHealth = model.HRmaximumHealth;
		movmentspeed = model.HRmovmentspeed;
		armor = model.HRarmor;
		magicResistence = model.HRmagicResistence;

		firstHand = new Crossbow (model.waponModel);
		secoundHand = firstHand;

		mainSkill = new HRBasicSKill(model.hunterSkillModel);
		secoundSkill = new HRSecoundSkill (model.hunterSkillModel);
		ultimate = new HRUltimate (model.hunterSkillModel);
	}
	private void SkillSetTwo(){
		currSKillSet = 2;
		maximumHealth = model.HMmaximumHealth;
		movmentspeed = model.HMmovmentspeed;
		armor = model.HMarmor;
		magicResistence = model.HMmagicResistence;

		firstHand = new Dagger (model.waponModel);
		secoundHand = new Dagger(model.waponModel);

		mainSkill = new HMBasicSkill (model.hunterSkillModel);
		secoundSkill = new HMSecoundSkill (model.hunterSkillModel);
			ultimate = new HMUltimate (model.hunterSkillModel);
	}
}
