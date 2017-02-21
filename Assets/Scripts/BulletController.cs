using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private Rigidbody2D rigid;
    public AudioSource audio;
    public float speed = 3f;
    public float damage = 1f;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, 2f);
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    void FixedUpdate() {
        Vector2 movement = transform.up * speed * Time.deltaTime;
        rigid.MovePosition(rigid.position + movement);
    }

    void OnTriggerEnter2D(Collider2D collider) {
        AudioSource.PlayClipAtPoint(audio.clip, transform.position);
        Destroy(gameObject);
        DamageController damageController = collider.GetComponent<DamageController>();
        if(damageController != null) {
            if(!collider.CompareTag("Ground")) {
                damageController.Damage(damage);
            }
        }
    }
}
