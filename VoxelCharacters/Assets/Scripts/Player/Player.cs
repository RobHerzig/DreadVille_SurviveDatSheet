using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class Player :  NetworkBehaviour{

	public int Level = 1;
	public float MaxHealth = 100;
	public float damage = 5;
	public float healthRegeneration = 1f;
	public float armor = 0.1f;
	public float cooldownReduction = 0f;

	internal float currXP= 0;
	public float XPForLevelUp = 100;
	internal int Gold=0;

	public Skill primarySkill;
	public Skill secoundarySkill;
	public Skill teritraySkill;
	public Skill UltimateSkill;
	Skill thisprimarySkill;
	Skill thissecoundarySkill;
	Skill thisteritraySkill;
	Skill thisUltimateSkill;
	public PlayerUI skillBar;
	internal LevelSystem levelSystem;
  
	internal NetworkInstanceId PlayerID;
	// Use this for initialization
	void Start () {
		if (!isLocalPlayer) {
			return;
		}	
		skillBar = Instantiate (skillBar);
		levelSystem = GetComponent<LevelSystem> ();
		thisprimarySkill= InitSKill (primarySkill);
		thissecoundarySkill= InitSKill (secoundarySkill);
		thisteritraySkill= InitSKill (teritraySkill);
		thisUltimateSkill= InitSKill (UltimateSkill);
		//InitButtons ();
    }

	Skill InitSKill(Skill skill){
		System.Type type = skill.GetComponent<Skill> ().GetType ();
		return CopyComponent(skill.GetComponent<Skill> (), gameObject)as Skill;

	}
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		}
		setXpBar (currXP);
		CheckLevelUp ();
		PlayerID = gameObject.GetComponent<NetworkIdentity> ().netId;
		skillBar.UpdateLevel (Level);
		skillBar.UpdateCooldownOnButton (1, thisprimarySkill.getCurrCoodown (), thisprimarySkill.getCooldown ());
		skillBar.UpdateCooldownOnButton (2, thissecoundarySkill.getCurrCoodown (), thissecoundarySkill.getCooldown ());
		skillBar.UpdateCooldownOnButton (3, thisteritraySkill.getCurrCoodown (), thisteritraySkill.getCooldown ());
		skillBar.UpdateCooldownOnButton (4, thisUltimateSkill.getCurrCoodown (), thisUltimateSkill.getCooldown ());


		if (Input.GetKeyDown (KeyCode.Alpha1))
			thisprimarySkill.checkCooldownAndTrigger ();
		if (Input.GetKeyDown (KeyCode.Alpha2))
			thissecoundarySkill.checkCooldownAndTrigger ();
		if (Input.GetKeyDown (KeyCode.Alpha3))
			thisteritraySkill.checkCooldownAndTrigger ();
		if (Input.GetKeyDown (KeyCode.Alpha4))
			thisUltimateSkill.checkCooldownAndTrigger ();
		
		

	}
	void setXpBar(float newRatio)
	{
		float ratioNew =  ((float)currXP / (float)XPForLevelUp );
		skillBar.XPBar.localScale = new Vector3(ratioNew, skillBar.XPBar.localScale.y, skillBar.XPBar.localScale.z);

		//healthBar.localScale = new Vector3(newRatio, healthBar.localScale.y, healthBar.localScale.z);
	}
	void CheckLevelUp(){
		if (currXP >= XPForLevelUp) {
			currXP -= XPForLevelUp;
			levelSystem.LevelUp (this);

		}
			
	}
	void InitButtons(){
		skillBar = gameObject.GetComponentInChildren<PlayerUI> ();


	}

    public void Beat(float directDamage)
    {
       // int actualDamage = (int)directDamage - (int)DEF;
      //  GetComponent<Health>().TakeDamage(actualDamage);

    }
	Component CopyComponent(Component original, GameObject destination)
	{
		System.Type type = original.GetType();
		Component copy = destination.AddComponent(type);
		// Copied fields can be restricted with BindingFlags
		System.Reflection.FieldInfo[] fields = type.GetFields(); 
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(original));
		}
		return copy;
	}

    void refreshHP()
    {
      
    }
}
