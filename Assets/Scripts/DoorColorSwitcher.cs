using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorColorSwitcher : AbstractMultipleTargetPowerUp
{
    public DoorColorSwitcher(int holdTime)
    {
        this.HoldTime = holdTime;
		TargetColor = Colors.IDLE;
		SecondTargetColor = Colors.IDLE;
    }

    public override void ExecutePowerUp()
	{
		List<DoorController> targetDoor = DetermineDoorTargets(TargetColor);
		List<DoorController> secondTargetDoor = DetermineDoorTargets(SecondTargetColor);
        GameObject.Find("AudioManager").GetComponent<AudioManager>().PlaySwitchDoorColorSound();

		foreach(DoorController door in targetDoor)
		{
			door.ChangeColor(SecondTargetColor);
		}
		foreach(DoorController door in secondTargetDoor)
		{
			door.ChangeColor(TargetColor);
		}
	}
}
