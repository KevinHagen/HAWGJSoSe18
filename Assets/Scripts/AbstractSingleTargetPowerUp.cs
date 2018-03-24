using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractSingleTargetPowerUp : AbstractPowerUp {
	
	protected IEnumerator WaitForPlayerReset()
	{
		yield return new WaitForSeconds(powerUpDuration);
		ResetPlayer();
	}

	protected virtual void ResetPlayer()
	{
		player.Color = tempColor;
		TargetColor = Colors.IDLE;
		player = null;
	}
}
