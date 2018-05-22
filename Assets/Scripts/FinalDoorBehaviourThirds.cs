using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehaviourThirds : FinalDoorBehaviour {

    [Tooltip("order: yellow,red,green,blue")]
    public Material[] bottomMaterialsLeft,bottomMaterialsMiddle,bottomMaterialsRight;
    [Tooltip("order: yellow,red,green,blue")]
    public Material[] topMaterialsLeft,topMaterialsMiddle,topMaterialsRight;

    public override void Init()
    {
        base.Init();
        bottomParts[0].GetComponent<MeshRenderer>().material = bottomMaterialsLeft[(int)colorList[0]];
        bottomParts[1].GetComponent<MeshRenderer>().material = bottomMaterialsMiddle[(int)colorList[1]];
        bottomParts[2].GetComponent<MeshRenderer>().material = bottomMaterialsRight[(int)colorList[2]];

        topParts[0].GetComponent<MeshRenderer>().material = topMaterialsLeft[(int)colorList[0]];
        topParts[1].GetComponent<MeshRenderer>().material = topMaterialsMiddle[(int)colorList[1]];
        topParts[2].GetComponent<MeshRenderer>().material = topMaterialsRight[(int)colorList[2]];
    }
}
