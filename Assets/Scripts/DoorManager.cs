using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager INSTANCE;

    public DoorController[] Doors { get; private set; }

    Dictionary<DoorController, bool> doorColorDictionary;
    public Dictionary<Colors, Material> doorMaterialDictionary;
    public Dictionary<Colors, Material> roofMaterialDictionary;
    public Material redDoor,redRoof;
    public Material greenDoor,greenRoof;
    public Material blueDoor,blueRoof;
    public Material yellowDoor,yellowRoof;

    public void Init()
    {
        if (INSTANCE != this && INSTANCE != null)
			Destroy(this);
        else
			INSTANCE = this;

        Doors = GameObject.Find("Doors").GetComponentsInChildren<DoorController>();
		InitializeDictionaries();
        GenerateDoors();
    }

	private void InitializeDictionaries()
	{
        doorColorDictionary = new Dictionary<DoorController, bool>();
        doorMaterialDictionary = new Dictionary<Colors, Material>();
        roofMaterialDictionary = new Dictionary<Colors, Material>();
        doorMaterialDictionary.Add(Colors.BLUE, blueDoor);
        doorMaterialDictionary.Add(Colors.RED, redDoor);
        doorMaterialDictionary.Add(Colors.YELLOW, yellowDoor);
		doorMaterialDictionary.Add(Colors.GREEN, greenDoor);
        roofMaterialDictionary.Add(Colors.BLUE, blueRoof);
        roofMaterialDictionary.Add(Colors.RED, redRoof);
        roofMaterialDictionary.Add(Colors.YELLOW, yellowRoof);
        roofMaterialDictionary.Add(Colors.GREEN, greenRoof);
    }

	private void GenerateDoors ()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            doorColorDictionary.Add(Doors[i], false);
			Doors[i].Init();
        }
        int randomDoorNr;
        Colors[] colors= { Colors.BLUE, Colors.RED, Colors.GREEN, Colors.YELLOW };
        for (int i = 0; i < Doors.Length; i += 1)
        {
            Colors thisColor = colors[i % 4];
            do
            {
                randomDoorNr = Random.Range(0, Doors.Length);
            } while (doorColorDictionary[Doors[randomDoorNr]]);
            Doors[randomDoorNr].Color = thisColor;
            doorColorDictionary[Doors[randomDoorNr]] = true;
            Doors[randomDoorNr].ChangeColor(thisColor);
        }
    }
}
