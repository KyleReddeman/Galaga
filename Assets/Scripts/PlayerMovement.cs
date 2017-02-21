using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private Rigidbody2D rigid;
    private float inputMovement;
    public AudioSource audio;
    private bool moving;
    public float speed = 5f;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        inputMovement = Input.GetAxis("Horizontal");
        Engine();
	}

    void Engine() {
        if (Mathf.Abs(inputMovement) > .2f && !moving) {
            moving = true;
            audio.pitch = 1.2f;
            audio.Play();
        }
        else if (Mathf.Abs(inputMovement) <= .2f && moving) {
            audio.pitch = 1f;
            audio.Play();
            moving = false;
        }
    }

    void FixedUpdate() {
        
        if(Mathf.Abs(inputMovement) > .2f) {
            Vector2 movement = transform.right * inputMovement * speed * Time.deltaTime;
            rigid.MovePosition(rigid.position + movement);
        }
        
    }
}
