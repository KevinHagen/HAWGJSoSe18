using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColorSwitcher : AbstractMultipleTargetPowerUp
{
    public DoorColorSwitcher(int holdTime)
    {
        this.HoldTime = holdTime;
    }

    public override void ExecutePowerUp()
	{
		DoorController targetDoor = DetermineDoorTarget(TargetColor);
		DoorController secondTargetDoor = DetermineDoorTarget(SecondTargetColor);
		tempColor = targetDoor.Color;
		targetDoor.Color = secondTargetDoor.Color;
		secondTargetDoor.Color = tempColor;
	}
}
