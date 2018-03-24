using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPrefab : MonoBehaviour {

    public Box.Index index;
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            AbstractPowerUp powerUp=null;
            switch(index)
            {
                case Box.Index.DOOR_COLOR_SWITCHER:
                    powerUp = new DoorColorSwitcher();
                    break;
                case Box.Index.PLAYER_COLOR_SWITCHER:
                    powerUp = new PlayerColorSwitcher();
                    break;
                case Box.Index.RAINBOW_COLORS:
                    powerUp = new RainbowColors();
                    break;
                case Box.Index.THUNDER:
                    powerUp = new Thunder(4);
                    break;
            }

            other.GetComponent<PlayerController>().CurrentPowerUp = powerUp;
        }
    }
}
