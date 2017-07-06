using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMBasicSkill : Skill {
	public CMBasicSkill (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CM1coolDown;
		range = model.CM1range;
	}

}