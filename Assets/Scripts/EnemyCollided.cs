using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollided : MonoBehaviour {

    public float collideAttack = 1f;
    public AudioSource audio;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
    void OnCollisionEnter2D(Collision2D collision) {
        
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        Destroy(gameObject);
        DamageController damageController = collision.gameObject.GetComponent<DamageController>();
        if(damageController != null) {
            damageController.Damage(collideAttack);
        }
    }
}
