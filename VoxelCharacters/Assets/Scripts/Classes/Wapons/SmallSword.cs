using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallSword : Wapon {

	public SmallSword (WaponsModel model)
	{
		this.model = model;
		prefab = model.smallSword;
	}
}
