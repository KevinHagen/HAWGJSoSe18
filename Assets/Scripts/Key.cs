using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour {

    public Colors color;
    public Box.Index index;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            if(other.GetComponent<PlayerController>().Color==color)
            {
                other.GetComponent<PlayerController>().hasKey = true;
            }
            else
            {
                LevelGenerator.levelGenerator.ReplacePowerUp(index);
            }
        }
    }
}
