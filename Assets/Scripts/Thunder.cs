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

	public override void ExecutePowerUp(Colors colorOfAttacker)
	{
		player = DeterminePlayerTarget(TargetColor);
		if(player == null)
		{
			return;
		}
        player.IsStunned = true;
		tempColor = player.Color;
		player.Color = Colors.BLACK;
		if(player.HasKey)
			DropKey();
        //TODO Animation/Partikel spielen
        player.StartCoroutine("StartThunderAnimation");
		player.StartCoroutine(WaitForPlayerReset());
	}

	protected override void ResetPlayer()
	{
		player.IsStunned = false;
		base.ResetPlayer();
	}

	private void DropKey()
	{
        Key key = player.GetComponentInChildren<Key>();
        player.currentKey.gameObject.SetActive(true);
        player.currentKey.StartCoroutine("KeyFly");
        player.HasKey = false;
        player.currentKey = null;

	}
	
}
