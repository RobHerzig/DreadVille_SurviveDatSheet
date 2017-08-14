using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerPrototype : NetworkBehaviour {

	[SerializeField] GameObject EnemyRange;
	[SerializeField] GameObject EnemyMeele;
    [SerializeField] GameObject spawnPoint;
    private int counter;
	private int numOfEnemies = 25;
	internal int currNumOFenemies = 0;

    public override void OnStartServer()
    {
        for(int i = 0; i < numOfEnemies; i++)
        {
            counter++;
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
		int  r = Random.Range (0, 2);
		GameObject spawn;
		if (r == 0)
			spawn = EnemyRange;
		else
			spawn = EnemyMeele;
		GameObject instance = Instantiate(spawn, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(instance);
        instance.GetComponent<EnemyID>().enemyID = "Enemy" + counter;
        instance.transform.parent = spawnPoint.transform;
		currNumOFenemies++;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isServer) {
			return;
		}
		if (currNumOFenemies < numOfEnemies)
			SpawnEnemy ();
	}
}
