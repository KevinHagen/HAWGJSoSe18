using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    MeshRenderer meshRend;
    public Colors _color;

	// Use this for initialization
	void Start () {
        meshRend = GetComponent<MeshRenderer>();
	}

    public void ChangeColor (Colors color)
    {
        meshRend.material = DoorManager.INSTANCE.doorMaterialDictionary[color];
        _color = color;
    }
}
