using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawnerPrototype : NetworkBehaviour {

    [SerializeField] GameObject enemyPrototype;
    [SerializeField] GameObject spawnPoint;
    private int counter;
    private int numOfEnemies = 25;

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
        GameObject instance = Instantiate(enemyPrototype, spawnPoint.transform.position, Quaternion.identity) as GameObject;
        NetworkServer.Spawn(instance);
        instance.GetComponent<EnemyID>().enemyID = "Enemy" + counter;
        instance.transform.parent = spawnPoint.transform;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
