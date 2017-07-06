using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHSecoundSkill : Skill {

	public CHSecoundSkill (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CH2coolDown;
		range = model.CH2range;
	}
}
