using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 320f;
    private Vector3 movement;
    Rigidbody rigid;
    GameObject camera;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        camera = Camera.main.gameObject;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) Move();
    }

    private void Move ()
    {
        movement = new Vector3(
                (camera.transform.forward.x * Input.GetAxis("Vertical") + camera.transform.right.x * Input.GetAxis("Horizontal")),
                0,
                (camera.transform.forward.z * Input.GetAxis("Vertical") + camera.transform.right.z * Input.GetAxis("Horizontal"))) 
                * speed * Time.deltaTime;
        rigid.velocity = movement;
    }
}
