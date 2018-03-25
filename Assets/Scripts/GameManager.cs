using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager INSTANCE;

    public PlayerController[] players;
	public FinalDoorBehaviour[] finalDoors;
	public GameObject[] finalDoorPrefabs;
	public List<Transform> finalDoorSpawns;
    public LevelGenerator levelGenerator;
    public DoorManager doorManager;

	private Colors[] playerColors = { Colors.YELLOW, Colors.RED, Colors.GREEN, Colors.BLUE };

	private void Awake()
	{
		if (INSTANCE != null && INSTANCE != this)
			Destroy(gameObject);
		else
			INSTANCE = this;

		InitPlayers();
		doorManager.Init();
		levelGenerator.Init(players);
		BuildTeams();
	}

	private void InitPlayers()
	{
		foreach(PlayerController pc in players)
		{
			pc.Init();
		}
	}

	private void BuildTeams()
	{
		List<Colors> colorList = new List<Colors>(playerColors);
		int amountOfDoors = Random.Range(2, 5);
		finalDoors = new FinalDoorBehaviour[amountOfDoors];

		switch(amountOfDoors)
		{
			case 2:
				bool isTwoVsTwo = Random.Range(0f, 1f) > 0.5f;
				if(isTwoVsTwo)
				{
					GenerateFinalDoors(2, colorList, finalDoorPrefabs[1]);
				}
				else
				{
					GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
					GenerateFinalDoors(1, colorList, finalDoorPrefabs[2]);
				}
				break;
			case 3:
				GenerateFinalDoors(2, colorList, finalDoorPrefabs[0]);
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[1]);
				break;
			case 4:
				GenerateFinalDoors(amountOfDoors, colorList, finalDoorPrefabs[0]);
				break;
		}
	}

	private void GenerateFinalDoors(int amountOfDoors, List<Colors> colorList, GameObject doorPrefab)
	{
		for (int i = 0; i < amountOfDoors; i++)
		{
			GameObject finalDoor = Instantiate(doorPrefab, finalDoorSpawns[i]);
			finalDoorSpawns.Remove(finalDoorSpawns[i]);
			//Transform + Rotate setzen
			finalDoors[i] = finalDoor.GetComponent<FinalDoorBehaviour>();
			Colors randomColor = colorList[Random.Range(0, colorList.Count)];
			colorList.Remove(randomColor);
			finalDoors[i].colorList.Add(randomColor);
		}
	}
}
