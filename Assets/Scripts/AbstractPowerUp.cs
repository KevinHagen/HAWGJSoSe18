using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp : MonoBehaviour
{
	public Colors TargetColor { get; set; }

	public GameObject powerUpPrefab;
	public int holdTime;

	protected PlayerController player;
	protected Colors tempColor;
	protected float powerUpDuration;
	protected int holdTimeLeft;

	public void PunishPlayer()
	{
		new Thunder(2).ExecutePowerUp();
	}

	public abstract void ExecutePowerUp();

	protected PlayerController DetermineTarget(Colors colorToCheck)
	{
		for (int i = 0; i < GameManager.INSTANCE.Players.Length; i++)
		{
			if (GameManager.INSTANCE.Players[i].Color == colorToCheck)
			{
				return GameManager.INSTANCE.Players[i];
			}
		}

		return null;
	}

	public IEnumerator HoldTimer()
	{
		holdTimeLeft = holdTime;
		while(holdTimeLeft > 0)
		{
			yield return new WaitForSeconds(1f);
			holdTimeLeft--;
			player.PlayerUI.holdTimerText.text = "" + holdTimeLeft;
		}
	}
}