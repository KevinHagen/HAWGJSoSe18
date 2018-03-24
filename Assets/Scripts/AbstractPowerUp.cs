using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPowerUp
{
	public Colors TargetColor { get; set; }

	public GameObject powerUpPrefab;
	public int HoldTime { get; protected set; }

	protected PlayerController player;
	protected Colors tempColor;
	protected float powerUpDuration;

	public void PunishPlayer()
	{
		Thunder thunder = new Thunder(5, 2);
		thunder.TargetColor = TargetColor;
		thunder.ExecutePowerUp();
	}

	public abstract void ExecutePowerUp();

	protected PlayerController DeterminePlayerTarget(Colors colorToCheck) 
	{
		for (int i = 0; i < GameManager.INSTANCE.players.Length; i++)
		{
			if (GameManager.INSTANCE.players[i].Color == colorToCheck)
			{
				return GameManager.INSTANCE.players[i];
			}
		}

		return null;
	}

	protected List<DoorController> DetermineDoorTargets(Colors colorToCheck)
	{
		List<DoorController> doorsWithTargetColor = new List<DoorController>();

		for (int i = 0; i < DoorManager.INSTANCE.Doors.Length; i++)
		{
			if (DoorManager.INSTANCE.Doors[i].Color == colorToCheck)
			{
				doorsWithTargetColor.Add(DoorManager.INSTANCE.Doors[i]);
			}
		}

		return doorsWithTargetColor;
	}
}