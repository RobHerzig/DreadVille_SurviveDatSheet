using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Wapon {

	public Shield (WaponsModel model)
	{
		this.model = model;
		prefab = model.shield;
	}
}
