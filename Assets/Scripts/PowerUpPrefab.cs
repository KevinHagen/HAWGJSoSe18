using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpPrefab : MonoBehaviour {

    public Box.Index index;
    public Material[] animationMaterials; 

    private MeshRenderer meshRenderer;

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        StartCoroutine("StartAnimation");
    }

    private IEnumerator StartAnimation()
    {
        int counter=0;
        while(true)
        {
            meshRenderer.material = animationMaterials[counter % animationMaterials.Length];
            counter++;
            yield return new WaitForSeconds(0.2f);
        }
        
    }

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
                    powerUp = new DoorColorSwitcher(7);
                    break;
                case Box.Index.PLAYER_COLOR_SWITCHER:
                    powerUp = new PlayerColorSwitcher(7);
                    break;
                case Box.Index.RAINBOW_COLORS:
                    powerUp = new RainbowColors(5,4);
                    break;
                case Box.Index.THUNDER:
                    powerUp = new Thunder(5,4);
                    break;
            }

			PlayerController player = other.gameObject.GetComponent<PlayerController>();

			if (powerUp != null)
				player.StartCoroutine(player.HoldTimer(powerUp.HoldTime));

            player.CurrentPowerUp = powerUp;
            Destroy(gameObject);
        }
    }
}
