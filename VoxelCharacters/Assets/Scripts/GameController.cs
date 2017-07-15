using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameController : NetworkBehaviour {

	public int TimeTillStart = 5;
	public EnemySpawnController EnemySpawnController;
	static public GameController singleton;
	internal int currNumberOfEnemies = 0;
	internal int WaveCounter = 0;
	int GameState = 0;
	public	int TimeBetweenWaves = 10;
	public int  bonusValueForNextWave = 2;
	float time;


	void Awake()
	{
		singleton = this;

//		if (!isServer)
//		{
//			return;
//		}
//
	
	}

	void Update()
	{
		GameFlow ();
	}

	void GameFlow(){
		Debug.Log (GameState);
	//state 0 : wait till Player Spawned
	// state 1: wave spaw
	// state 2: wait till wave is killed
	// state 3 : pause between waves then state 1
		switch (GameState) {
		case 0:
			
			break;
		case 1:
			EnemySpawnController.SpawnRandomWave ();	
			GameState = 2;
			break;
		case 2:
			if (currNumberOfEnemies == 0) 
					GameState = 3;
			
			break;
		case 3:
			time += Time.deltaTime;
			if (time >= TimeBetweenWaves) {
				time = 0;
				GameState = 1;
				EnemySpawnController.ValueOfWaves += bonusValueForNextWave;
			}
			break;


		}
	}
	public override void OnStartServer (){
	
		if (!isServer) {
			return;
		}
			
		Debug.Log ("game init");
		StartCoroutine (StartGameRoutine (TimeTillStart));

	}

	IEnumerator WaitGameRoutine (int secounds)
	{

		yield return new WaitForSeconds (secounds);
		WaveCounter++;
	}


	IEnumerator StartGameRoutine (int secounds)
	{

		yield return new WaitForSeconds (secounds);
		GameObject[] Gates = GameObject.FindGameObjectsWithTag ("Gate");
		foreach (GameObject gate in Gates) {
			gate.GetComponent<OpenGate> ().Open ();

		}
		GameState = 1;
	}
}
