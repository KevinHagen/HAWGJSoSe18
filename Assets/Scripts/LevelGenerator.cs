using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {
    
    public Transform[] startPositions;
    public GameObject[] players;

    private PlayerController _currentPlayer;
    private Dictionary<Transform, bool> startPositionsDictionary;
    private Dictionary<Colors, bool> playerColors;

    // Use this for initialization
    void Start () {
        for (int i=0;i<players.Length;i++)
        {
            startPositionsDictionary[startPositions[i]] = false;
        }
        playerColors[Colors.BLUE] = false;
        playerColors[Colors.GREEN] = false;
        playerColors[Colors.RED] = false;
        playerColors[Colors.YELLOW] = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStartPositions()
    {
        for(int i=0;i<players.Length;i++)
        {
            _currentPlayer = players[i].GetComponent<PlayerController>();
            int randPos;
            do
            {
                randPos = Random.Range(0, 4);

            }while(startPositionsDictionary[startPositions[randPos]]);

            startPositionsDictionary[startPositions[randPos]] = true;
            players[i].transform.position = startPositions[randPos].position;
        }
    }
}
