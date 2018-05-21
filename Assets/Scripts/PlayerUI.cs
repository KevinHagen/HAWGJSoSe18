using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

    [Tooltip("Order: DOOR_COLOR_SWITCHER,PLAYER_COLOR_SWITCHER,RAINBOW_COLORS,THUNDER")]
    public Sprite[] powerupIcons; //Must Have same order as Index Enum
    [Tooltip("Order: YELLOW,RED,GREEN,BLUE")]
    public Sprite[] keySprites; //-||-

	public GameObject powerupHolder;
	public Image keyHolderIcon;
	public Image currentPowerupIcon;
	public Image[] colorFields;

	public Text holdTimerText;

    public Quaternion lookAtCamera { get; private set; }
    

    public void SetUIRotation()
    {
        transform.LookAt(Camera.main.transform);
        transform.Rotate(Vector3.up, 180);
        transform.rotation = Quaternion.LookRotation(transform.forward, Camera.main.transform.up);
        lookAtCamera = transform.rotation;
    }

    private void Update()
	{
        transform.rotation = lookAtCamera;
	}

	public void SetKeyColor(Colors newColor)
	{
		keyHolderIcon.sprite = keySprites[(int)newColor];
	}

    public void SetPowerUpIcon(Box.Index index)
    {
        currentPowerupIcon.sprite = powerupIcons[((int)index)]; 
    }

	public void HighlightColorField(int index)
	{
		//Change the color here
		//TODO
		Debug.Log("Changed color of: " + index);
	}

	public void UpdateUI(bool hasKey, bool hasPowerup)
	{
		keyHolderIcon.gameObject.SetActive(hasKey);
		powerupHolder.SetActive(hasPowerup);
	}
}
