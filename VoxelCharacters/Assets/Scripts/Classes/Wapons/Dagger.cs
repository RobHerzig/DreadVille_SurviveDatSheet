using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Wapon {

	public Dagger (WaponsModel model)
	{
		this.model = model;
		prefab = model.dagger;
	}

}
