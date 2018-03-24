using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AbstractSingleTargetPowerUp
{
	public Thunder(int holdTime,float _powerUpDuration)
	{
		powerUpDuration = _powerUpDuration;
        this.HoldTime = holdTime;
    }

	public override void ExecutePowerUp()
	{
		player = DetermineTarget(TargetColor);
        player.StopAllCoroutines();
        player.IsStunned = true;
		tempColor = player.Color;
		player.Color = Colors.BLACK;
		DropKey();
		//TODO Animation/Partikel spielen
		player.StartCoroutine(WaitForPlayerReset());
		player.CurrentPowerUp = null;
	}

	protected override void ResetPlayer()
	{
		base.ResetPlayer();
		player.IsStunned = false;
	}

	private void DropKey()
	{
		//TODO
		player.HasKey = false;
	}
	
}
