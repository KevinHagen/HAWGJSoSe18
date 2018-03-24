using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSwitcher : AbstractMultipleTargetPowerUp
{
	private PlayerController secondPlayer;

    public PlayerColorSwitcher(int holdTime)
    {
        this.HoldTime = holdTime;
		TargetColor = Colors.IDLE;
		SecondTargetColor = Colors.IDLE;
	}

    public override void ExecutePowerUp()
	{
		player = DeterminePlayerTarget(TargetColor);
		secondPlayer = DeterminePlayerTarget(SecondTargetColor);

		tempColor = player.Color;
		player.Color = secondPlayer.Color;
		secondPlayer.Color = tempColor;
	}
}