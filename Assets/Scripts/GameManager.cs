using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager INSTANCE;

    public PlayerController[] players;
    public LevelGenerator levelGenerator;
    public DoorManager doorManager;

	private void Awake()
	{
		if (INSTANCE != null && INSTANCE != this)
			Destroy(gameObject);
		else
			INSTANCE = this;

		doorManager.Init();
		levelGenerator.Init(players);
	}
}
