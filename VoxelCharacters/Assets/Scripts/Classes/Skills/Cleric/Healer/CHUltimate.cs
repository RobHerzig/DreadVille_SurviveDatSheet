using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHUltimate : Skill {
	
	public CHUltimate (ClericSkillModel model)
	{
		this.model = model;
		coolDown = model.CH3coolDown;
		range = model.CH3range;
	}
}

