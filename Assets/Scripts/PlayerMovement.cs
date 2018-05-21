using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float speed;
    private Vector3 movement;
    Rigidbody rigid;
    GameObject mainCamera;
    float verticalPush;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        mainCamera = Camera.main.gameObject;
        //speed = 400f;
        verticalPush = 2.4f;
    }

    public void Move (float inputsX,float inputZ)
    {
        movement = new Vector3(
                (mainCamera.transform.forward.x * verticalPush * inputZ + mainCamera.transform.right.x * inputsX),
                0,
                (mainCamera.transform.forward.z * verticalPush* inputZ + mainCamera.transform.right.z * inputsX))
                * speed * Time.deltaTime;

        rigid.velocity = movement;
        if (movement != new Vector3(0, 0, 0))
            transform.rotation = Quaternion.LookRotation(movement);

        //if(movement!=new Vector3(0,0,0))
        //{
        //    rigid.velocity = movement;
        //    Debug.DrawLine(transform.position, transform.position+movement, Color.red, 0.5f);
        //    transform.rotation = Quaternion.LookRotation(movement);
        //    Debug.DrawLine(transform.position, transform.position+transform.forward, Color.cyan, 0.5f);
        //}
    }



}
