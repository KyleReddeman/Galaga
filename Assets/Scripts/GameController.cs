using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private SpawnEnemies spawnEnemies;
	private HealthConroller healthController;
	private int roundNumber = 1;
	private WaitForSeconds startWait;
	private WaitForSeconds endWait;
	private float totalPoints = 0f;
	private Dictionary<string, int> enemiesKilled;
     
	public Text message;
	public Text enemyStats;
	public Text highScore;
	public Text score;
	public float startDelay;
	public float endDelay;

	// Use this for initializat;ion
	void Start() {
		enemiesKilled = new Dictionary<string, int>();
		highScore.text += PlayerPrefs.GetInt("highScore");
		spawnEnemies = GetComponent<SpawnEnemies>();
		healthController = GetComponent<HealthConroller>();
		startWait = new WaitForSeconds(startDelay);
		endWait = new WaitForSeconds(endDelay);
		StartCoroutine(GameLoop());
		Reset();
	}
	
	// Update is called once per frame
	void Update() {
		
	}

	private IEnumerator GameLoop() {
		// Start off by running the 'RoundStarting' coroutine but don't return until it's finished.
		yield return StartCoroutine(WaveStarting());

		// Once the 'RoundStarting' coroutine is finished, run the 'RoundPlaying' coroutine but don't return until it's finished.
		yield return StartCoroutine(WavePlaying());

		// Once execution has returned here, run the 'RoundEnding' coroutine, again don't return until it's finished.
        
		if(!healthController.GameOver()) {
			yield return StartCoroutine(WaveEnding());
			StartCoroutine(GameLoop());
		} else {
			message.color = Color.red;
			message.fontStyle = FontStyle.Bold;
           
			float highScore = PlayerPrefs.GetInt("highScore");
			highScore = Mathf.Max(highScore, totalPoints);
            
			message.text += "GAME OVER";
			message.text += "\nReached Wave: " + roundNumber;
			message.text += "\n\nHighScore: " + highScore;
			message.text += "\nScore: " + totalPoints;

			PlayerPrefs.SetInt("highScore", (int)highScore);
		}
	}

	private IEnumerator WaveStarting() {
		message.text = "Wave " + roundNumber;
		int numEnemies = 2 * roundNumber + 3;
		float min = spawnEnemies.minSpawnTime - .2f;
		float max = spawnEnemies.maxSpawnTime - .1f;
		min = Mathf.Max(min, .5f);
		max = Mathf.Max(max, min + .1f);

		spawnEnemies.SetSpawnDelay(min, max);
		spawnEnemies.SetMaxEnemies(numEnemies);
		yield return startWait;
	}

	private IEnumerator WavePlaying() {
		roundNumber++;
		message.text = "";
		Invoke("Spawn", 2f);
		while(!healthController.GameOver() && !spawnEnemies.NoEnemies()) {
			yield return null;
		}
		spawnEnemies.ResetWaveEnemies();
	}

	private IEnumerator WaveEnding() {
		enemyStats.text = "Score: " + totalPoints + "\n";
		List<string> keys = new List<string>(enemiesKilled.Keys);
		foreach(string key in keys) {
			enemyStats.text += key + ": " + enemiesKilled[key] + "\n";
		}
		yield return endWait;
		enemiesKilled.Clear();
		enemyStats.text = "";
	}

	void Spawn() {
		spawnEnemies.Spawn();
	}

	public void AddPoints(float points) {
		totalPoints += points;
	}

	public void SetScoreText(string text) {
		score.text = text;
	}

	public int GetScore() {
		return (int)totalPoints;
	}

	public void UpdateEnemiesKilled(string enemy, int count) {
		enemiesKilled[enemy] = count;
	}

	public int GetEnemiesKilled(string key) {
		if(!enemiesKilled.ContainsKey(key)) {
			enemiesKilled.Add(key, 0);
		}
		return enemiesKilled[key];
	}

	void Reset() {
		totalPoints = 0;
		message.color = Color.white;
		message.fontStyle = FontStyle.Normal;
	}
}
