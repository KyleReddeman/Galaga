using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

    public Camera camera;
    public float minSpawnTime = 2f;
    public float maxSpawnTime = 5f;
    public int numberOfEnemies = 0;
    public GameObject[] enemies;
    private Transform[] spawnPoints = new Transform[5];
    private HealthConroller healthController;
    private int maxEnemies;
	// Use this for initialization
	void Start () {
        healthController = gameObject.GetComponent<HealthConroller>();
		for(int i = 0; i < spawnPoints.Length; i++) {
            Transform transform = new GameObject().transform;
            transform.gameObject.name = "SpawnPoint" + (i + 1);
            transform.position = camera.ViewportToWorldPoint(new Vector3(.2f * i + .1f, 1f, 10f));
            spawnPoints[i] = transform;
        }
    }
	
	// Update is called once per frame
	void Update () {
        
	}

    public void Spawn() {
        numberOfEnemies++;
        int spawnPoint = Random.Range(0, 5);
        GameObject enemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(enemy, spawnPoints[spawnPoint].position, Quaternion.identity);
        float spawnTime = Random.Range(minSpawnTime, maxSpawnTime);
        if(!healthController.GameOver() && numberOfEnemies < maxEnemies) {
            Invoke("Spawn", spawnTime);
        }     
    }

    public void SetMaxEnemies(int max) {
        maxEnemies = max;
    }

    public bool NoEnemies() {
        return numberOfEnemies >= maxEnemies && GameObject.FindGameObjectsWithTag("Enemy").Length <= 0;
    }

    public void ResetWaveEnemies() {
        numberOfEnemies = 0;
    }

    public void SetSpawnDelay(float min, float max) {
        minSpawnTime = min;
        maxSpawnTime = max;
    }
}
