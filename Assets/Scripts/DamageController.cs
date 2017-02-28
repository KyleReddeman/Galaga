using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageController : MonoBehaviour {

	public int startingHealth;
	public int health;
	public int points;
	public string name = "";
	private HealthConroller healthController;
	private GameController gameController;
	private GameObject gameManager;
	public ParticleSystem explosion;

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

	public void Damage(int damage) {
		health -= damage;
		healthController.UpdateUI();
		string tag = gameObject.tag;
		if(health <= 0f && tag != "Ground") {
			Explode();
			Destroy(gameObject);
			if(tag == "Enemy") {
				int enemiesShot = gameController.GetEnemiesKilled(name);
				gameController.UpdateEnemiesKilled(name, enemiesShot + 1);
				gameController.AddPoints(points);
				gameController.SetScoreText("        Score: " + gameController.GetScore());
			}
		}
	}

	public void Explode() {
		GameObject particle = Instantiate(explosion.gameObject, gameObject.transform.position, Quaternion.identity);
		explosion.Play();
		Destroy(particle, 1f);
	}

	public void AddHealth(int healthBonus) {
		health = Mathf.Min(health + healthBonus, startingHealth);
		healthController.UpdateUI();
	}
}
