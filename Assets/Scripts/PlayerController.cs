using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    private Colors _color;


    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public Colors Color
    {
        set
        {
            _color = value;
            //set different textures for color here
            Debug.Log(this+", "+_color);
        }
    }
}
