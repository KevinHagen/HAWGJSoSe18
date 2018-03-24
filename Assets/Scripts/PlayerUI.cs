using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour {

	public Canvas canvas;
	[Tooltip("Must have same order as Colors Enum")]
	public Sprite[] powerupIcons; //Must Have same order as Colors Enum
	public Sprite[] keySprites; //-||-

	public GameObject powerupHolder;
	public Image keyHolderIcon;
	public Image currentPowerupIcon;
	public Image[] colorFields;

	public Text holdTimerText;

	public void SetKeyColor(Colors newColor)
	{
		keyHolderIcon.sprite = keySprites[(int)newColor];
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
