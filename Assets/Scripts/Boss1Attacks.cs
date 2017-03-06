using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attacks : MonoBehaviour {
	private Boss1Controller boss1Controller;

	private void Awake() {
		boss1Controller = gameObject.GetComponent<Boss1Controller>();
	}

	private void SweepFromLeft() {
		
	}

	private void SweepFromRight() {
		
	}

	private void RandomSpawning() {
		 
	}

	public void Up() {
		StartCoroutine(GoUp());
	}

	private IEnumerator GoUp() {
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

