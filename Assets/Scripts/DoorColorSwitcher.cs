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

    public override void ExecutePowerUp(Colors colorOfAttacker)
	{
		List<DoorController> targetDoor = DetermineDoorTargets(TargetColor);
		List<DoorController> secondTargetDoor = DetermineDoorTargets(SecondTargetColor);
        if (!AudioManager.INSTANCE.GetComponent<AudioSource>().isPlaying)
            AudioManager.INSTANCE.PlaySwitchDoorColorSound();

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
