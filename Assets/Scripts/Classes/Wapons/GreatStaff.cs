using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreatStaff : Wapon{

	public GreatStaff (WaponsModel model)
	{
		this.model = model;
		prefab = model.greatStaff;
	}
}
