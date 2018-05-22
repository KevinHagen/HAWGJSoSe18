using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoorBehaviourHalfs : FinalDoorBehaviour {

    [Tooltip("order: yellow,red,green,blue")]
    public Material[] bottomMaterials;
    [Tooltip("order: yellow,red,green,blue")]
    public Material[] topMaterials;

    public override void Init()
    {
        base.Init();
        for (int i = 0; i < bottomParts.Length; i++)
        {
            //color each part in the right color; if there is only one color in the list, give all parts that color
            bottomParts[i].GetComponent<MeshRenderer>().material = colorList.Count > 1 ? bottomMaterials[(int)colorList[i]] : bottomMaterials[(int)colorList[0]];
            topParts[i].GetComponent<MeshRenderer>().material = colorList.Count > 1 ? topMaterials[(int)colorList[i]] : topMaterials[(int)colorList[0]];
        }
    }
}
