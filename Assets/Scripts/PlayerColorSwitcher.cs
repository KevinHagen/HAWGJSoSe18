﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColorSwitcher : AbstractMultipleTargetPowerUp
{
	private PlayerController secondPlayer;
	
	public override void ExecutePowerUp()
	{
		player = DetermineTarget(TargetColor);
		secondPlayer = DetermineTarget(SecondTargetColor);

		tempColor = player.Color;
		player.Color = secondPlayer.Color;
		secondPlayer.Color = tempColor;
	}
}