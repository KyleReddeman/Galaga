using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollided : MonoBehaviour {

    public float collideAttack;
    public AudioSource audio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnCollisionEnter2D(Collision2D collision) {
        
        
        DamageController damageController = collision.gameObject.GetComponent<DamageController>();
        if(damageController != null) {
            damageController.Damage(collideAttack);
        }
		AudioSource.PlayClipAtPoint(audio.clip, transform.position);
		if (transform.position.y > collision.transform.position.y) {
			gameObject.GetComponent<DamageController>().Explode();
			Destroy(gameObject);
		}

    }
}
