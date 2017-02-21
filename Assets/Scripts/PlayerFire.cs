using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour {

    private bool fired = false;
    public GameObject projectile;
    public float meh = 6;
    public Transform bulletSpawnPosition;
    public AudioSource audio;
    public float fireDelay = .2f;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        float input = Input.GetAxis("Fire1");
        if(input > .2f && !fired) {
            fired = true;
            Instantiate(projectile, bulletSpawnPosition.position, bulletSpawnPosition.rotation);
            audio.Play();
            Invoke("CanFire", fireDelay);
        }
	}

    void CanFire() {
        fired = false;
    }
}
