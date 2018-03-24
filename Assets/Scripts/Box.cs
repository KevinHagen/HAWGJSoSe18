using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public enum Index
    {
        DOOR_COLOR_SWITCHER,
        PLAYER_COLOR_SWITCHER,
        RAINBOW_COLORS,
        THUNDER,
        KEY_YELLOW,
        KEY_RED,
        KEY_GREEN,
        KEY_BLUE,
        EMPTY
    }
    
    public Index index;
    public Colors quarter;
    public GameObject powerUpPrefab;
    public GameObject keyPrefab;
    
    

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(other.gameObject.GetComponent<PlayerController>().activatePressed)
            {
                GameObject _spawnObject=null;
                switch(index)
                {
                    case Index.KEY_BLUE:
                        _spawnObject= keyPrefab;
                        TransferData(_spawnObject, index, Colors.BLUE);
                        break;
                    case Index.KEY_GREEN:
                        _spawnObject = keyPrefab;
                        TransferData(_spawnObject, index, Colors.GREEN);
                        break;
                    case Index.KEY_RED:
                        _spawnObject = keyPrefab;
                        TransferData(_spawnObject, index, Colors.RED);
                        break;
                    case Index.KEY_YELLOW:
                        _spawnObject = keyPrefab;
                        TransferData(_spawnObject, index, Colors.YELLOW);
                        break;
                    case Index.EMPTY:
                        break;
                    default: _spawnObject = powerUpPrefab;
                        _spawnObject.GetComponent<PowerUpPrefab>().index = index;
                        other.GetComponent<PlayerController>().PlayerUI.SetPowerUpIcon(index);
                        break;
                }
                
                if (_spawnObject != null)
					Instantiate(_spawnObject, transform.position, Quaternion.Euler(0, 0, 0));
                LevelGenerator.levelGenerator.PositionBox(gameObject);  //set box to new place
                if(index!=Index.EMPTY)  //box contained a key or power up
                {
                    index = Index.EMPTY;    //set box as empty
                    LevelGenerator.levelGenerator.FillEmptyBox(LevelGenerator.levelGenerator.FindPowerUpIndex());   //refill random empty box
                }
            }
        }
    }

    private void TransferData(GameObject spawnObject,Index index,Colors color)
    {
        spawnObject.GetComponent<Key>().index = index;
        spawnObject.GetComponent<Key>().color = color;
    }
}
