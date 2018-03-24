using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {

    public static LevelGenerator levelGenerator;
    public Transform[] startPositions,boxPositions;
    public GameObject[] players,boxes;
    [Tooltip("Order: doorColorChange, playerColorChange, rainbow, thunder")]
    public float[] powerUpChance;

    private PlayerController _currentPlayer;
    private Box _currentBox;
    private Dictionary<Transform, bool> startPositionsDictionary, boxPositionsDictionary;
    private Dictionary<Colors, bool> playerColors;
    private Dictionary<Transform, Colors> quarters;

    private void Awake()
    {
        if(levelGenerator==null)
        {
            levelGenerator = this;
        }
        if(levelGenerator!=this)
        {
            Destroy(gameObject);
        }
    }

    // Use this for initialization
    void Start () {
        startPositionsDictionary = new Dictionary<Transform, bool>();
        playerColors = new Dictionary<Colors, bool>();
        boxPositionsDictionary = new Dictionary<Transform, bool>();
        quarters = new Dictionary<Transform, Colors>();
        for (int i=0;i<startPositions.Length;i++)
        {
            startPositionsDictionary.Add(startPositions[i], false);
            quarters.Add(startPositions[i],Colors.BLACK);
        }
        for(int j=0;j<boxPositions.Length;j++)
        {
            boxPositionsDictionary.Add(boxPositions[j], false);
        }
        playerColors.Add(Colors.BLUE, false);
        playerColors.Add(Colors.GREEN, false);
        playerColors.Add(Colors.RED, false);
        playerColors.Add(Colors.YELLOW, false);

        SetStartPositionsPlayers();
        SetStartPositionBoxes();
    }

    public void SetStartPositionsPlayers()
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

            //set values for quarters
            quarters[startPositions[randPos]] = (Colors)randColor;
        }
    }

    public void SetStartPositionBoxes()
    {
        for(int j=0;j<boxes.Length;j++)
        {
            PositionBox(boxes[j]);
        }
        FillEmptyBox(Box.Index.KEY_BLUE);
        FillEmptyBox(Box.Index.KEY_GREEN);
        FillEmptyBox(Box.Index.KEY_RED);
        FillEmptyBox(Box.Index.KEY_YELLOW);
        for (int i = 0; i < boxes.Length-4; i++)
        {
            FillEmptyBox(FindPowerUpIndex());
        }
    }

    //Gets an index of a new powerUp taking into account the chance of each powerUp
    public Box.Index FindPowerUpIndex()
    {
        float randIndex = Random.Range(0f, 1f);
        Box.Index boxIndex = Box.Index.EMPTY;

        for(int i=0;i<powerUpChance.Length;i++)
        {
            float countChance = 0;
            for(int j=0;j<i;j++)
            {
                countChance += powerUpChance[j];
            }
            if (randIndex >= countChance && randIndex < countChance + powerUpChance[i])
            {
                boxIndex = (Box.Index)i;
            }
        }

        return boxIndex;
    }

    public void PositionBox(GameObject box)
    {
        int randBox;
        do
        {
            randBox = Random.Range(0, boxPositions.Length);

        } while (boxPositionsDictionary[boxPositions[randBox]]);
        boxPositionsDictionary[boxPositions[randBox]] = true;
        box.transform.position = boxPositions[randBox].position;

        Colors color=Colors.BLACK;
        float minDistance=int.MaxValue;
        for(int i=0;i<players.Length;i++)
        {
            float distance = Vector3.Distance(box.transform.position, startPositions[i].position);
            if (distance<minDistance)
            {
                color = quarters[startPositions[i]];
                minDistance = distance;
            }
        }
        box.GetComponent<Box>().quarter = color;
    }

    //finds and fills an empty Box with given index
    public void FillEmptyBox(Box.Index index)
    {
        int boxNum = 0;
        do
        {
            boxNum = Random.Range(0, boxes.Length);
            _currentBox = boxes[boxNum].GetComponent<Box>();

        } while (_currentBox.index != Box.Index.EMPTY || ConflictWithKey(_currentBox,index));
        _currentBox.index = index;
        Debug.Log(boxes[boxNum] + ", " + index);
    }

    public void ReplacePowerUp(Box.Index keyIndex)
    {
        int boxNum = 0;
        do
        {
            boxNum = Random.Range(0, boxes.Length);
            _currentBox = boxes[boxNum].GetComponent<Box>();

        } while (_currentBox.index == Box.Index.KEY_BLUE || _currentBox.index == Box.Index.KEY_GREEN ||
                    _currentBox.index == Box.Index.KEY_RED || _currentBox.index == Box.Index.KEY_YELLOW || 
                    ConflictWithKey(_currentBox, keyIndex));
        _currentBox.index = keyIndex;
        Debug.Log(boxes[boxNum] + ", " + keyIndex);
    }

    public bool ConflictWithKey(Box box,Box.Index index)
    {
        if(index==Box.Index.KEY_BLUE && box.quarter==Colors.BLUE)
        {
            return true;
        }
        else if(index == Box.Index.KEY_GREEN && box.quarter == Colors.GREEN)
        {
            return true;
        }
        else if (index == Box.Index.KEY_RED && box.quarter == Colors.RED)
        {
            return true;
        }
        else if (index == Box.Index.KEY_YELLOW && box.quarter == Colors.YELLOW)
        {
            return true;
        }

        return false;
    }
}
