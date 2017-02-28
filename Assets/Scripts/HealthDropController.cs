using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropController : MonoBehaviour {

	public int healthBonus;

	void OnTriggerEnter2D(Collider2D collider) {
		
		DamageController damageController = collider.gameObject.GetComponent<DamageController>();

		if(damageController != null) {
			Destroy(gameObject);
			damageController.AddHealth(healthBonus);

		}
		else { 
			Destroy(gameObject, 10f);
		}
	}
}
