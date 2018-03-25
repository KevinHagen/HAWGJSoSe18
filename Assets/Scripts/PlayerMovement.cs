using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float speed;
    private Vector3 movement;
    Rigidbody rigid;
    GameObject mainCamera;
    float verticalPush;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        mainCamera = Camera.main.gameObject;
        speed = 400f;
        verticalPush = 2.4f;
    }

    public void Move (float inputsX,float inputZ)
    {
        movement = new Vector3(
                (mainCamera.transform.forward.x * verticalPush * inputZ + mainCamera.transform.right.x * inputsX),
                0,
                (mainCamera.transform.forward.z * verticalPush * inputZ + mainCamera.transform.right.z * inputsX))
                * speed * Time.deltaTime;
        Debug.DrawLine(Vector3.zero, mainCamera.transform.right);
        rigid.velocity = movement;
    }



}
