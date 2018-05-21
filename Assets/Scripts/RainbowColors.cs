using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColors : AbstractSingleTargetPowerUp
{
    public RainbowColors(int holdTime,float powerUpDuration)
    {
        this.HoldTime = holdTime;
        this.powerUpDuration = powerUpDuration;
    }

	public override void ExecutePowerUp(Colors colorOfAttacker)
	{
        if(TargetColor==colorOfAttacker)
        {
            PunishPlayer();
            return;
        }
		player = DeterminePlayerTarget(TargetColor);
		if (player == null) return;
		player.PlayerUI.powerupHolder.SetActive(false);
		tempColor = player.Color;
		player.Color = Colors.RAINBOW;
		player.StartCoroutine(WaitForPlayerReset());
	}
}
