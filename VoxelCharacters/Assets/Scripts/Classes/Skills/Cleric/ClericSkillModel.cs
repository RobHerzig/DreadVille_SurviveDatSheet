using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClericSkillModel : SkillModel {

	[Header("Cleric Healer Skills")]
	[Header("Skill 1")]
	public float CH1coolDown;
	public float CH1range;
	public Image CH1img;

	[Header("Skill 2")]
	public float CH2coolDown;
	public float CH2range;
	public Image CH2img;

	[Header("Skill 3")]
	public float CH3coolDown;
	public float CH3range;
	public Image CH3img;

	[Header("Cleric Mage Skills")]
	[Header("Skill 1")]
	public float CM1coolDown;
	public float CM1range;
	public Image SCM1img;

	[Header("Skill 2")]
	public float CM2coolDown;
	public float CM2range;
	public Image CM2img;

	[Header("Skill 3")]
	public float CM3coolDown;
	public float CM3range;
	public Image CM3img;
}