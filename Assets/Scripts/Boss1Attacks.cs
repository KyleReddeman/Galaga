using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attacks : MonoBehaviour {
	private Boss1Controller boss1Controller;

	private void Awake() {
		boss1Controller = gameObject.GetComponent<Boss1Controller>();
	}

	public IEnumerator SweepFromLeft() {
		yield return null;
	}

	public IEnumerator SweepFromRight() {
		yield return StartCoroutine(GoToLocation(new Vector3(0f, 2.5f, 0f), 2f));
		StartCoroutine(boss1Controller.OpenDoors());
		yield return StartCoroutine(GoToLocation(new Vector3(-4f, 2.5f, 0f), 1f));
		yield return StartCoroutine(GoToLocation(new Vector3(-4f, 1.4f, 0f), .1f));
		StartCoroutine(GoToLocation(new Vector3(4f, 1.4f, 0f), 10f));
		for(int i = 0; i < 10; i++) {
			yield return StartCoroutine(boss1Controller.SpawnEnemy());
			yield return new WaitForSeconds(1f);
		}
		yield return StartCoroutine(GoToLocation(new Vector3(0f, 1.4f, 0f), .5f));
		yield return StartCoroutine(boss1Controller.CloseDoors());
	}

	public IEnumerator RandomSpawning() {
		yield return null;
	}

	public IEnumerator GoToLocation(Vector3 destination, float time) {
		Vector3 direction = (destination - transform.position);
		float distance = direction.magnitude;
		direction.Normalize();
		while(transform.position != destination) {
			transform.position += direction * distance / time * Time.deltaTime; 
			if((destination - transform.position).normalized != direction) {
				transform.position = destination;
			}
			Debug.Log(transform.position + "..." + destination);
			yield return null;
		}
		yield return null;
	}
	public IEnumerator GoUp() {
		float height = 2.5f;
		float distance = Mathf.Abs(height - transform.position.y);
		float time = 1.8f;

		while(transform.position.y < height) {
			transform.position += Vector3.up * distance / time * Time.deltaTime;
			float y = Mathf.Min(transform.position.y, height);
//			float x = Scale(0f, distance, 0f, 2 * Mathf.PI, 2.7f - transform.position.y);
//			float rangeValue = Scale(-1f, 1f, 1.5f, 3f, Mathf.Sin(glowCycles * x));
//			shipGlow.range = rangeValue;
			transform.position = new Vector3(transform.position.x, y, transform.position.z);
			yield return null;
		}
		yield return null;
	}
}

