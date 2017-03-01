using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Attacks : MonoBehaviour {
	private Boss1Controller boss1Controller;

	private void Awake() {
		boss1Controller = gameObject.GetComponent<Boss1Controller>();
	}
}
