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
        ExchangeKey();
	}

    private void ExchangeKey()
    {
        bool firstPlayerHasKey = player.HasKey;
        bool secondPlayerHasKey = secondPlayer.HasKey;
        Key firstPlayerKey = player.currentKey;
        Key secondPlayerKey = secondPlayer.currentKey;

        if(secondPlayerHasKey)
        {
            player.currentKey = secondPlayerKey;
            secondPlayerKey.transform.parent = player.transform;
        }
        else
        {
            player.currentKey = null;
        }
        if(firstPlayerHasKey)
        {
            secondPlayer.currentKey = firstPlayerKey;
            firstPlayerKey.transform.parent = secondPlayer.transform;
        }
        else
        {
            secondPlayer.currentKey = null;
        }

        player.HasKey = secondPlayerHasKey;
        secondPlayer.HasKey = firstPlayerHasKey;
    }
}