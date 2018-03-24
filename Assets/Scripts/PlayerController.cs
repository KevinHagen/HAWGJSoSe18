using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	public bool IsStunned { get; set; }
    private Colors _color;

    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public Colors Color
    {
		get
		{
			return _color;
		}
        set
        {
            _color = value;
            //set different textures for color here
            Debug.Log(this+", "+_color);
        }
    }
}
