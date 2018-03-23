using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private Vector3 movement;
    Rigidbody rigid;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) Move();
	}

    private void Move ()
    {
        movement = new Vector3(-Input.GetAxis("Vertical"), 0, Input.GetAxis("Horizontal")) * speed * Time.deltaTime;
        //transform.position += movement;
        rigid.velocity = movement;
    }
}
