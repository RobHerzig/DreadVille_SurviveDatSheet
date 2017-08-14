using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBook : Wapon {

	public SpellBook (WaponsModel model)
	{
		this.model = model;
		prefab = model.spellBook;
	}
}
