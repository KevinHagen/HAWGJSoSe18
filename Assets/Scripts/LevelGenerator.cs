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
        startPositionsDictionary = new Dictionary<Transform, bool>();
        playerColors = new Dictionary<Colors, bool>();
        for (int i=0;i<players.Length;i++)
        {
            startPositionsDictionary.Add(startPositions[i], false);
        }
        playerColors.Add(Colors.BLUE, false);
        playerColors.Add(Colors.GREEN, false);
        playerColors.Add(Colors.RED, false);
        playerColors.Add(Colors.YELLOW, false);

        SetStartPositions();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetStartPositions()
    {
        for(int i=0;i<players.Length;i++)
        {
            _currentPlayer = players[i].GetComponent<PlayerController>();
            int randPos,randColor;
            //get random start position and set Player to this transform.position
            do
            {
                randPos = Random.Range(0, 4);

            }while(startPositionsDictionary[startPositions[randPos]]);
            startPositionsDictionary[startPositions[randPos]] = true;
            players[i].transform.position = startPositions[randPos].position;
            
            //get random Color for player
            do
            {
                randColor = Random.Range(0, 4);

            } while (playerColors[(Colors)randColor]);
            playerColors[(Colors)randColor] = true;
            _currentPlayer.Color = (Colors)randColor;

        }
    }
}
