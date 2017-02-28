using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1Controller : MonoBehaviour {

	public float attackMovementSpeed;
	public float enteranceMovermentSpeed;
	public float glowCycles;
	public GameObject[] enemies;

	private Light shipGlow;
	private Light coreGlow;
	private Rigidbody2D rigid;

	void Awake() {
		rigid = gameObject.GetComponent<Rigidbody2D>();
		Light[] lights = gameObject.GetComponentsInChildren<Light>();
		foreach(Light light in lights) {
			if(light.gameObject.transform.parent != null) {
				coreGlow = light;
			}
			else {
				shipGlow = light;
			}
		}
	}

	void Start () {
		StartCoroutine(BossLoop());
	}
	
	void Update () {
		
	}

	public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue) {
		float OldRange = (OldMax - OldMin);
		float NewRange = (NewMax - NewMin);
		float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

		return(NewValue);
	}

	private IEnumerator BossLoop() {
		rigid.velocity = Vector2.down * .15f;
		yield return StartCoroutine(EnterScene());

		yield return StartCoroutine(BattleEnemy());
	}

	private IEnumerator EnterScene() {
		while(!EnteredScene()) {
			
			yield return null;
		}
		rigid.velocity = Vector3.zero;
	}

	private IEnumerator BattleEnemy() {
		yield return new WaitForSeconds(2f);
		while(true) {
			yield return StartCoroutine(SpawnEnemy());
			yield return new WaitForSeconds(2f);
		}
	}

	private IEnumerator SpawnEnemy() {
		while(Spawning()) {
			yield return null;
		}
		coreGlow.range = .3f;
		GameObject enemy = enemies[Random.Range(0, enemies.Length)];
		Instantiate(enemy, coreGlow.gameObject.transform.position, Quaternion.identity);
		while(Despawning()) {
			yield return null;
		}
		coreGlow.range = .25f;
		coreGlow.intensity = 0f;
		yield return null;
	}

	private bool Spawning() {
		coreGlow.intensity += 35f * Time.deltaTime;
		return coreGlow.intensity < 2f;
	}

	private bool Despawning() {
		coreGlow.intensity -= 20f * Time.deltaTime;	
		return coreGlow.intensity > 0f;
	}

	private bool EnteredScene() {
		
		float y = Mathf.Max(transform.position.y, 1.4f);
		float x = Scale(0f, 1.3f, 0f, 2 * Mathf.PI, 2.7f - transform.position.y);
		float rangeValue = Scale(-1f, 1f, 1.5f, 3f, Mathf.Sin(glowCycles * x));
		shipGlow.range = rangeValue;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		return transform.position.y <= 1.4f;
	}


}
