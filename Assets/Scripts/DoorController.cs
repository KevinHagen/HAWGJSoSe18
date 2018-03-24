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
        switch(color)
        {
            case Colors.BLUE:
                gameObject.layer = LayerMask.NameToLayer("Blue");
                break;
            case Colors.GREEN:
                gameObject.layer = LayerMask.NameToLayer("Green");
                break;
            case Colors.RED:
                gameObject.layer = LayerMask.NameToLayer("Red");
                break;
            case Colors.YELLOW:
                gameObject.layer = LayerMask.NameToLayer("Yellow");
                break;
        }
    }
}
