using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CMUltimate : Skill {

	public CMUltimate (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CM3coolDown;
		range = model.CM3range;
	}
}
