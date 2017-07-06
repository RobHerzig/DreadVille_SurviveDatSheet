using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassModel : MonoBehaviour {


	[Header("Solider Tank")]
	public float STmaximumHealth;
	public float STcurrentHealth;
	public float STmovmentspeed;
	public float STarmor;
	public float STmagicResistence;

	[Header("Solider Meele")]
	public float SMmaximumHealth;
	public float SMcurrentHealth;
	public float SMmovmentspeed;
	public float SMarmor;
	public float SMmagicResistence;

	[Header("Hunter Range")]
	public float HRmaximumHealth;
	public float HRcurrentHealth;
	public float HRmovmentspeed;
	public float HRarmor;
	public float HRmagicResistence;

	[Header("Hunter Meele")]
	public float HMmaximumHealth;
	public float HMcurrentHealth;
	public float HMmovmentspeed;
	public float HMarmor;
	public float HMmagicResistence;

	[Header("Cleric Healer")]
	public float CHmaximumHealth;
	public float CHcurrentHealth;
	public float CHmovmentspeed;
	public float CHarmor;
	public float CHmagicResistence;

	[Header("Clearic Mage")]
	public float CMmaximumHealth;
	public float CMcurrentHealth;
	public float CMmovmentspeed;
	public float CMarmor;
	public float CMmagicResistence;

	[Header("Class Prefabs")]
	public GameObject SoliderPrefab;
	public GameObject HunterPrefab;
	public GameObject ClericPrefab;

	[Header("Wapon & Skill Models")]
	public WaponsModel waponModel;
	public SoliderSkillModel soliderSkillModel;
	public ClericSkillModel clericSkillModel;
	public HunterSkillModel hunterSkillModel;

}
