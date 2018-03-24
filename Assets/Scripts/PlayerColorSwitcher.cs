using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSwitcher : AbstractMultipleTargetPowerUp
{
	private PlayerController secondPlayer;

    public PlayerColorSwitcher(int holdTime)
    {
        this.HoldTime = holdTime;
    }

    public override void ExecutePowerUp()
	{
		player = DetermineTarget(TargetColor);
		player.StopAllCoroutines();
		secondPlayer = DetermineTarget(SecondTargetColor);

		tempColor = player.Color;
		player.Color = secondPlayer.Color;
		secondPlayer.Color = tempColor;
		player.CurrentPowerUp = null;
	}
}
