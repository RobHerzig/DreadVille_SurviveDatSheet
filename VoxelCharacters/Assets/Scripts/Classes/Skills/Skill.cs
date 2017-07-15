using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;


public abstract class Skill : NetworkBehaviour  {

	protected float currCooldown = 0;
	public float Cooldown = 2f;
	internal float damage;
	public float damageRatio = 0.5f;
	private Player player;
	void Awake(){
		player = GetComponent<Player> ();
	}
	public virtual void TriggerSKill(){
		
	}
	public  void checkCooldownAndTrigger(){
		if (currCooldown == 0) {
			TriggerSKill ();
			SetCooldown ();
		}
	}
	private void SetCooldown(){
		currCooldown = Cooldown;
	}
	public void UpdateCooldown()
	{
		if (currCooldown > 0)
			currCooldown -= Time.deltaTime;
		else
			currCooldown = 0;

	}
	public float getCurrCoodown(){
		return currCooldown;
	}
	public float getCooldown(){
		return Cooldown;
	}
	void Update(){
		
		damage = player.damage * damageRatio;
		UpdateCooldown ();

	}
}
