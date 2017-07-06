using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Solider : Class {

	public Solider(byte classState,ClassModel model)
	{
		this.model = model;
		prefab = model.SoliderPrefab;
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
		maximumHealth = model.STmaximumHealth;
		movmentspeed = model.STmovmentspeed;
		armor = model.STarmor;
		magicResistence = model.STmagicResistence;

		firstHand = new SmallSword (model.waponModel);
			secoundHand = new Shield (model.waponModel);

		mainSkill = new STBasicSkill (model.soliderSkillModel);
		secoundSkill = new STSecoundSkill (model.soliderSkillModel);
		ultimate = new STUltimate (model.soliderSkillModel);
	}
	private void SkillSetTwo(){
		currSKillSet = 2;
		maximumHealth = model.SMmaximumHealth;
		movmentspeed = model.SMmovmentspeed;
		armor = model.SMarmor;
		magicResistence = model.SMmagicResistence;

		firstHand = new GreatSword(model.waponModel);
		secoundHand = firstHand;

		mainSkill = new SMBasicSkill (model.soliderSkillModel);
		secoundSkill = new SMSecoundSkill (model.soliderSkillModel);
		ultimate = new SMUltimate (model.soliderSkillModel);
	}

}
