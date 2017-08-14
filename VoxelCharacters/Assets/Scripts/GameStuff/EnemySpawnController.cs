using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
public class EnemySpawnController : NetworkBehaviour {
	
	public GameObject[] SpawnPoints;
	public Enemy[] normalEnemys;
	public Enemy[] Bosses;
	public int numberOfDifferentEnemies = 2;
	public int ValueOfWaves= 10;

	private int EnemyIDCounter = 0;

	public void SpawnRandomWave(){
		Enemy[] spawnList = GenerateNormalWave (ValueOfWaves);
		foreach (Enemy enemy in spawnList) {
			EnemySpawn (enemy);
		}
		GameController.singleton.WaveCounter++;
	}
	void EnemySpawn(Enemy enemy)
	{
		GameObject SpawnPoint =  SpawnPoints[Random.Range (0, SpawnPoints.Length)];
		GameObject instance = Instantiate(enemy.gameObject, SpawnPoint.transform.position, Quaternion.identity) as GameObject;
		NetworkServer.Spawn(instance);
		instance.GetComponent<EnemyID>().enemyID = "Enemy" + EnemyIDCounter;
		EnemyIDCounter++;
		instance.transform.parent = SpawnPoint.transform;
		GameController.singleton.currNumberOfEnemies++;
	}
	public Enemy[] GenerateNormalWave(int waveValue){
		List<Enemy> SpawnableEnemies = new List<Enemy> ();
		for (int i = 0; i < numberOfDifferentEnemies; i++) {
			int randomIndex = Random.Range (0, normalEnemys.Length);
			if (SpawnableEnemies.Contains (normalEnemys [randomIndex]))
				i--;
			else
				SpawnableEnemies.Add (normalEnemys [randomIndex]);
		}
		int currWaveValue = waveValue;
		SpawnableEnemies = SpawnableEnemies.OrderBy (t => t.value).ToList();
		SpawnableEnemies.Reverse ();
		List<Enemy> SpawnList = new List<Enemy> ();
		foreach (Enemy enemy in SpawnableEnemies) {
			if( currWaveValue- enemy.value >0){
				int numberOfEnemiesSPawnFromThisType = Mathf.FloorToInt(((float)waveValue /(float) numberOfDifferentEnemies) /(float) enemy.value);
				for (int i = 0; i < numberOfEnemiesSPawnFromThisType; i++) {
					SpawnList.Add (enemy);
					currWaveValue -= enemy.value;
				}
			}
		}
		while (waveValue- currWaveValue <= SpawnableEnemies[SpawnableEnemies.Count-1].value) {
			SpawnList.Add (SpawnableEnemies[SpawnableEnemies.Count-1]);
			currWaveValue -= SpawnableEnemies [SpawnableEnemies.Count - 1].value;
		}
		return SpawnList.ToArray();
			
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
