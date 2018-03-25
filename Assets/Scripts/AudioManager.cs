using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager INSTANCE;

    public AudioClip menuSelection;
    public AudioClip menuConfirmation;
    public AudioClip pickUpPowerUp;
    public AudioClip pickUpKey;
    public AudioClip[] switchPlayerColor;
    public AudioClip[] switchDoorColor;

    private void Awake()
    {
        if (INSTANCE != null && INSTANCE != this)
            Destroy(gameObject);
        else
            INSTANCE = this;
    }

}
