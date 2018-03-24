using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AbstractSingleTargetPowerUp
{
	public Thunder(int _powerUpDuration)
	{
		powerUpDuration = _powerUpDuration;
	}

	public Thunder(GameObject _powerUpPrefab, int _holdTime, float _powerUpDuration)
	{
		powerUpPrefab = _powerUpPrefab;
		holdTime = _holdTime;
		powerUpDuration = _powerUpDuration;
	}

	public override void ExecutePowerUp()
	{
		StopAllCoroutines();
		player = DetermineTarget(TargetColor);
		player.IsStunned = true;
		tempColor = player.Color;
		player.Color = Colors.BLACK;
		DropKey();
		//TODO Animation/Partikel spielen
		StartCoroutine(WaitForPlayerReset());
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
