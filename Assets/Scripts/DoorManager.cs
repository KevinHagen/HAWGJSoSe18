using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager INSTANCE;

    public DoorController[] Doors { get; private set; }

    Dictionary<DoorController, bool> doorColorDictionary;
    public Dictionary<Colors, Material> doorMaterialDictionary;
    public Material redDoor;
    public Material greenDoor;
    public Material blueDoor;
    public Material yellowDoor;

    // Use this for initialization
    void Start()
    {
        if (INSTANCE == null) INSTANCE = this;
        if (INSTANCE != this) Destroy(this);

        Doors = GameObject.Find("Doors").GetComponentsInChildren<DoorController>();
        doorColorDictionary = new Dictionary<DoorController, bool>();
        doorMaterialDictionary = new Dictionary<Colors, Material>();
        doorMaterialDictionary.Add(Colors.BLUE, blueDoor);
        doorMaterialDictionary.Add(Colors.RED, redDoor);
        doorMaterialDictionary.Add(Colors.YELLOW, yellowDoor);
        doorMaterialDictionary.Add(Colors.GREEN, greenDoor);
        
        GenerateDoors();
    }

    void GenerateDoors ()
    {
        for (int i = 0; i < Doors.Length; i++)
        {
            doorColorDictionary.Add(Doors[i], false);
        }
        int randomDoorNr;
        Colors[] colors= { Colors.BLUE, Colors.RED, Colors.GREEN, Colors.YELLOW };
        for (int i = 0; i < Doors.Length; i += 1)
        {
            Colors thisColor = colors[i % 4];
            do
            {
                randomDoorNr = Random.Range(0, Doors.Length);
            } while (doorColorDictionary[Doors[randomDoorNr]] == true);
            Doors[randomDoorNr].GetComponent<DoorController>().Color = thisColor;
            doorColorDictionary[Doors[randomDoorNr]] = true;
            Doors[randomDoorNr].GetComponent<DoorController>().ChangeColor(thisColor);
        }
    }

}
