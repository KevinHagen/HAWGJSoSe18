using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

    MeshRenderer meshRend;
    public Colors Color { get; set; }

	// Use this for initialization
	void Start () {
        meshRend = GetComponent<MeshRenderer>();
	}

    public void ChangeColor (Colors color)
    {
        meshRend.material = DoorManager.INSTANCE.doorMaterialDictionary[color];
        Color = color;
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
