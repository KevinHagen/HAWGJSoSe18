using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSwitcher : AbstractPowerUp
{
	private Colors secondTargetColor;
	private PlayerController secondPlayer;

	public PlayerColorSwitcher(Colors _secondTargetColor)
	{
		secondTargetColor = _secondTargetColor;
	}

	public override void ExecutePowerUp()
	{
		player = DetermineTarget(TargetColor);
		secondPlayer = DetermineTarget(secondTargetColor);

		tempColor = player.Color;
		player.Color = secondPlayer.Color;
		secondPlayer.Color = tempColor;
	}
}
