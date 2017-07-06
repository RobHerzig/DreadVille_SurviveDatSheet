using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMSecoundSkill : Skill {

	public CMSecoundSkill (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CM2coolDown;
		range = model.CM2range;
	}
}
