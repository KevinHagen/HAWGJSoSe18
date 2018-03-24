using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager INSTANCE;

	public PlayerController[] Players { get; private set; }

	private void Awake()
	{
		if (INSTANCE == null)
			INSTANCE = this;
		if (INSTANCE != this)
			Destroy(this);
	}
}
