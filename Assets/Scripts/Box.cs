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

	// Use this for initialization
	void Awake () {
        index = Index.EMPTY;
	}
	


}
