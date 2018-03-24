using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public static DoorManager INSTANCE;

    GameObject[] doors;
    Dictionary<GameObject, bool> doorColorDictionary;
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

        doors = GameObject.FindGameObjectsWithTag("Door");
        doorColorDictionary = new Dictionary<GameObject, bool>();
        doorMaterialDictionary = new Dictionary<Colors, Material>();
        doorMaterialDictionary.Add(Colors.BLUE, blueDoor);
        doorMaterialDictionary.Add(Colors.RED, redDoor);
        doorMaterialDictionary.Add(Colors.YELLOW, yellowDoor);
        doorMaterialDictionary.Add(Colors.GREEN, greenDoor);
        
        GenerateDoors();
    }

    void GenerateDoors ()
    {
        for (int i = 0; i < doors.Length; i++)
        {
            doorColorDictionary.Add(doors[i], false);
        }
        int randomDoorNr;
        Colors[] colors= { Colors.BLUE, Colors.RED, Colors.GREEN, Colors.YELLOW };
        for (int i = 0; i < doors.Length; i += 1)
        {
            Colors thisColor = colors[i % 4];
            do
            {
                randomDoorNr = Random.Range(0, doors.Length);
            } while (doorColorDictionary[doors[randomDoorNr]] == true);
            doors[randomDoorNr].GetComponent<DoorController>()._color = thisColor;
            doorColorDictionary[doors[randomDoorNr]] = true;
            doors[randomDoorNr].GetComponent<DoorController>().ChangeColor(thisColor);
        }
    }

}
