using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportController : MonoBehaviour {

	private SpawnEnemies spawnEnemies;
	private Transform[] spawnPoints;
	private GameObject gameManager;
	public ParticleSystem teleportParticles;
	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager");
		spawnEnemies = gameManager.GetComponent<SpawnEnemies>();
		spawnPoints = spawnEnemies.GetSpawnPoints();
		InvokeRepeating("Teleport", 2f, 2.5f);
	}

	void Teleport() {
		int spawnPointIndex = Random.Range(0, spawnPoints.Length);
		Vector3 newPosition = transform.position;
		newPosition.x = spawnPoints[spawnPointIndex].position.x;
		if(newPosition.x == transform.position.x) {
			Teleport();
		}
		else {
			GameObject particle = Instantiate(teleportParticles.gameObject, transform.position, Quaternion.identity);
			teleportParticles.Play();
			Destroy(particle, 2f);
			transform.position = newPosition;
			particle = Instantiate(teleportParticles.gameObject, transform.position, Quaternion.identity);
			Destroy(particle, 2f);
		}
	}
}
