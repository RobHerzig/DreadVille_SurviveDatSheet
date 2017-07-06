using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HunterSkillModel : SkillModel {

	[Header("Hunter Range Skills")]
	[Header("Skill 1")]
	public float HR1coolDown;
	public float HR1range;
	public Image HR1img;

	[Header("Skill 2")]
	public float HR2coolDown;
	public float HR2range;
	public Image HR2img;

	[Header("Skill 3")]
	public float HR3coolDown;
	public float HR3range;
	public Image HR3img;

	[Header("Hunter Meele Skills")]
	[Header("Skill 1")]
	public float HM1coolDown;
	public float HM1range;
	public Image HM1img;

	[Header("Skill 2")]
	public float HM2coolDown;
	public float HM2range;
	public Image HM2img;

	[Header("Skill 3")]
	public float HM3coolDown;
	public float HM3range;
	public Image HM3img;
}
