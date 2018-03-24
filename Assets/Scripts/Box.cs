using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

    public enum Index
    {
        EMPTY,
        DOOR_COLOR_SWITCHER,
        PLAYER_COLOR_SWITCHER,
        RAINBOW_COLORS,
        THUNDER,
        KEY_YELLOW,
        KEY_RED,
        KEY_GREEN,
        KEY_BLUE
    }

    public Index index;
    public Colors quarter;
    public GameObject powerUpPrefab;
    public GameObject keyYellowPrefab, keyRedPrefab, keyGreenPrefab, keyBluePrefab;

	// Use this for initialization
	void Awake () {
        index = Index.EMPTY;
	}
    

    private void OnTriggerStay(Collider other)
    {
        if(other.tag=="Player")
        {
            if(Input.GetAxisRaw("Activate")==1 || Input.GetAxisRaw("Activate") == -1)
            {
                GameObject _spawnObject;
                switch(index)
                {
                    case Index.KEY_BLUE:
                        _spawnObject= keyBluePrefab;
                        TransferData(_spawnObject, index, Colors.BLUE);
                        break;
                    case Index.KEY_GREEN:
                        _spawnObject = keyGreenPrefab;
                        TransferData(_spawnObject, index, Colors.GREEN);
                        break;
                    case Index.KEY_RED:
                        _spawnObject = keyRedPrefab;
                        TransferData(_spawnObject, index, Colors.RED);
                        break;
                    case Index.KEY_YELLOW:
                        _spawnObject = keyYellowPrefab;
                        TransferData(_spawnObject, index, Colors.YELLOW);
                        break;
                    default: _spawnObject = powerUpPrefab;
                        _spawnObject.GetComponent<PowerUpPrefab>().index = index;
                        break;
                }

                Instantiate(_spawnObject, transform.position, Quaternion.Euler(0, 0, 0));
                LevelGenerator.levelGenerator.PositionBox(gameObject);
            }
        }
    }

    private void TransferData(GameObject spawnObject,Index index,Colors color)
    {
        spawnObject.GetComponent<Key>().index = index;
        spawnObject.GetComponent<Key>().color = color;
    }
}
