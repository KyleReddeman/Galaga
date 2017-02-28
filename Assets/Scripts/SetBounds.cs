using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBounds : MonoBehaviour {


    public Transform leftBound;
    public Transform righBound;
    public Camera camera;
	// Use this for initialization
	void Start () {
		Vector3 posistion = camera.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        posistion = new Vector3(posistion.x - .1f, posistion.y + .15f, posistion.z);

        leftBound.position = posistion;
        posistion = new Vector3(posistion.x * -1, posistion.y, posistion.z);
        righBound.position = posistion;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
