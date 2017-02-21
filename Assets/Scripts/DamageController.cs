using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	public float startingHealth;
	public float health;
	public float points;
	public string name = "";
	private HealthConroller healthController;
	private GameController gameController;
	private GameObject gameManager;

	// Use this for initialization
	void Start() {
		health = startingHealth;
		gameManager = GameObject.Find("GameManager");
		healthController = gameManager.GetComponent<HealthConroller>();
		gameController = gameManager.GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	public void Damage(float damage) {
		health -= damage;
		healthController.UpdateUI();
		string tag = gameObject.tag;
		if(health <= 0f && tag != "Ground") {
			Destroy(gameObject);
			if(tag == "Enemy") {
				int enemiesShot = gameController.GetEnemiesKilled(name);
				gameController.UpdateEnemiesKilled(name, enemiesShot + 1);
				gameController.AddPoints(points);
				gameController.SetScoreText("        Score: " + gameController.GetScore());
			}
		}
	}
}
