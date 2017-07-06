using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHBasicSkill : Skill {
	public CHBasicSkill (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CH1coolDown;
		range = model.CH1range;
	}


}
