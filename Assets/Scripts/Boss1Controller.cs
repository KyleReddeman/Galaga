using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1Controller : MonoBehaviour {

	public float attackMovementSpeed;
	public float enteranceTime;
	public float doorMovementTime;
	public float glowCycles;
	public Transform leftDoor;
	public Transform rightDoor;
	public GameObject[] enemies;
	public AudioClip battleMusic;
	public AudioClip spaceShip;

	private AudioSource music;
	private GameObject gameManager;
	private Light shipGlow;
	private Light coreGlow;
	private Rigidbody2D rigid;
	private Rigidbody2D leftDoorRigid;
	private Rigidbody2D rightDoorRigid;
	private bool defeatedBoss;
	public bool DefeatedBoss {
		get {
			return defeatedBoss;
		}
	}

	void Awake() {
		gameManager = GameObject.FindWithTag("GameController");
		music = gameManager.GetComponent<AudioSource>();
		rigid = gameObject.GetComponent<Rigidbody2D>();
		leftDoorRigid = leftDoor.gameObject.GetComponent<Rigidbody2D>();
		rightDoorRigid = rightDoor.gameObject.GetComponent<Rigidbody2D>();

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
		music.volume = 1f;
		music.clip = spaceShip;
		music.Play();
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
		yield return StartCoroutine(EnterScene());
		music.clip = battleMusic;
		music.Play();
		yield return StartCoroutine(BattleLoop());
	}

	private IEnumerator EnterScene() {
		while(!EnteredScene()) {
			yield return null;
		}
	}

	private IEnumerator BattleLoop() {
		yield return new WaitForSeconds(2f);
		while(true) {
			yield return StartCoroutine(OpenDoors());
			yield return StartCoroutine(SpawnEnemy());
			yield return new WaitForSeconds(1f);
			yield return StartCoroutine(CloseDoors());
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

	private IEnumerator OpenDoors() {
		
		while(leftDoor.position.x > -.55f && rightDoor.position.x < .49f) {
			rightDoor.position += Vector3.right * .2f / doorMovementTime * Time.deltaTime;
			leftDoor.position += Vector3.left * .2f / doorMovementTime * Time.deltaTime;
			float rightX = Mathf.Min(rightDoor.localPosition.x, .49f);
			float leftX = Mathf.Max(leftDoor.localPosition.x, -.55f);
			rightDoor.localPosition = new Vector3(rightX, rightDoor.localPosition.y, rightDoor.localPosition.z);
			leftDoor.localPosition = new Vector3(leftX, leftDoor.localPosition.y, leftDoor.localPosition.z);
			yield return null;
		}
		yield return null;
	}

	private IEnumerator CloseDoors() {
		while(leftDoor.position.x < -.35f && rightDoor.position.x > .29f) {
			rightDoor.position += Vector3.left * .2f / doorMovementTime * Time.deltaTime;
			leftDoor.position += Vector3.right * .2f / doorMovementTime * Time.deltaTime;
			float rightX = Mathf.Max(rightDoor.localPosition.x, .29f);
			float leftX = Mathf.Min(leftDoor.localPosition.x, -.35f);
			rightDoor.localPosition = new Vector3(rightX, rightDoor.localPosition.y, rightDoor.localPosition.z);
			leftDoor.localPosition = new Vector3(leftX, leftDoor.localPosition.y, leftDoor.localPosition.z);
			yield return null;
		}
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
		transform.position += Vector3.down * 1.3f / enteranceTime * Time.deltaTime;
		float y = Mathf.Max(transform.position.y, 1.4f);
		float x = Scale(0f, 1.3f, 0f, 2 * Mathf.PI, 2.7f - transform.position.y);
		float rangeValue = Scale(-1f, 1f, 1.5f, 3f, Mathf.Sin(glowCycles * x));
		shipGlow.range = rangeValue;
		transform.position = new Vector3(transform.position.x, y, transform.position.z);
		return transform.position.y <= 1.4f;
	}

	private void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("Yeah");
	}


}
