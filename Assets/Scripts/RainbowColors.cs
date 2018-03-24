using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainbowColors : AbstractSingleTargetPowerUp
{
	public override void ExecutePowerUp()
	{
		player = DetermineTarget(TargetColor);
		tempColor = player.Color;
		player.Color = Colors.RAINBOW;
		StartCoroutine(WaitForPlayerReset());
	}
}
