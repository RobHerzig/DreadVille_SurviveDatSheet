using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class LevelSystem : MonoBehaviour {

	public float XPBonusScaleForNextLevel = 0.3f;
	public float LevelUPMaxHealthBonus = 5;
	public float LevelUPMaxHealthScaleBonus = 0.3f;
	public float LevelUPDamageBonus = 2;
	public float LevelUPDamageScaleBonus = 0.1f;
	public float LevelUPhealthRegenerationBonus = 0.1f;
	public float LevelUPhealthRegenerationScaleBonus = 0.05f;
	public float LevelUPArmorBonus = 0.001f;
	public float LevelUPArmorScaleBonus = 0.0005f;
	public float LevelUPcooldownReductionBonus = 0.001f;



	// Use this for initialization
	public void LevelUp(Player player){
		player.Level += 1;
		player.XPForLevelUp += XPBonusScaleForNextLevel * player.XPForLevelUp;
		player.MaxHealth += LevelUPMaxHealthBonus + player.Level * LevelUPMaxHealthScaleBonus;
		player.damage += LevelUPDamageBonus + player.Level * LevelUPDamageScaleBonus;
		player.healthRegeneration = LevelUPMaxHealthBonus + player.Level * LevelUPhealthRegenerationScaleBonus;
		player.armor += LevelUPArmorBonus + player.Level * LevelUPArmorScaleBonus;
		player.cooldownReduction += LevelUPcooldownReductionBonus;  


	}

}
