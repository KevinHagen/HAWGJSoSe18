using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager INSTANCE;

    public PlayerController[] players;
	public GameObject[] finalDoorPrefabs;
	public List<Transform> finalDoorSpawns;
    public LevelGenerator levelGenerator;
    public DoorManager doorManager;
    public GameObject winScreen;
    public Text winText;
    public bool gameOver {  get; private set; }

	private FinalDoorBehaviour[] finalDoors;
	private int finalDoorIndex;
	private Colors[] playerColors = { Colors.YELLOW, Colors.RED, Colors.GREEN, Colors.BLUE };

    private void Update()
    {
        if(gameOver)
        {
            if (Input.GetButtonDown("Green1"))
            {
                ReturnToMainMenu();
            }
        }
    }

    public  void GameOver(List<Colors> colorList)
    {
        gameOver = true;
        winScreen.SetActive(true);
        winText.text="";
        foreach(Colors color in colorList)
        {
            winText.text += color.ToString().ToUpper();
            winText.text += " wins \n";
        }
    }

    public void ReturnToMainMenu()
	{
		SceneManager.LoadScene(0);
	}

	private void Awake()
	{
		if (INSTANCE != null && INSTANCE != this)
			Destroy(gameObject);
		else
			INSTANCE = this;
		finalDoorIndex = 0;
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
					GenerateFinalDoors(2, colorList, finalDoorPrefabs[1]);
				}
				else
				{
					GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
					GenerateFinalDoors(3, colorList, finalDoorPrefabs[2]);
				}
				break;
			case 3:
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
                GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
                GenerateFinalDoors(2, colorList, finalDoorPrefabs[1]);
				break;
			case 4:
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
				GenerateFinalDoors(1, colorList, finalDoorPrefabs[0]);
				break;
		}

		foreach(FinalDoorBehaviour finalDoor in finalDoors)
		{
			if(finalDoor!=null)
				finalDoor.Init();
		}
	}

	private void GenerateFinalDoors(int amountOfColors, List<Colors> colorList, GameObject doorPrefab)
	{
		GameObject finalDoor = Instantiate(doorPrefab, finalDoorSpawns[0].position, finalDoorSpawns[0].rotation);
		finalDoorSpawns.Remove(finalDoorSpawns[0]);
		finalDoors[finalDoorIndex] = finalDoor.GetComponent<FinalDoorBehaviour>();
		for(int j = 0; j < amountOfColors; j++)
		{
			Colors randomColor = colorList[Random.Range(0, colorList.Count)];
			colorList.Remove(randomColor);
			finalDoors[finalDoorIndex].colorList.Add(randomColor);
		}
		finalDoorIndex++;
	}
}
