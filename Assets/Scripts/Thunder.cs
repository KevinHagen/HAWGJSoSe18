﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thunder : AbstractPowerUp
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
		player = DetermineTarget(TargetColor);
		player.IsStunned = true;
		tempColor = player.Color;
		player.Color = Colors.BLACK;
		DropKey();
		//TODO Animation/Partikel spielen
		StartCoroutine(WaitForPlayerReset());
	}

	protected override void ResetPlayer()
	{
		player.IsStunned = false;
		player.Color = tempColor;
	}

	private void DropKey()
	{
		//TODO
	}
	
}
