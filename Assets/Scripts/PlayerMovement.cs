using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed = 320f;
    private Vector3 movement;
    Rigidbody rigid;
    GameObject mainCamera;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        mainCamera = Camera.main.gameObject;
	}

    public void Move (float inputsX,float inputZ)
    {
        movement = new Vector3(
                (mainCamera.transform.forward.x * inputZ + mainCamera.transform.right.x * inputsX),
                0,
                (mainCamera.transform.forward.z * inputZ + mainCamera.transform.right.z * inputsX))
                * speed * Time.deltaTime;
        rigid.velocity = movement;
    }



}
