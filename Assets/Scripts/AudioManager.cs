using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public static AudioManager INSTANCE;

    public AudioClip menuSelection;
    public AudioClip menuConfirmation;
    public AudioClip pickUpPowerUp;
    public AudioClip pickUpKey;
    public AudioClip destoyBox;
    public AudioClip gameOver;
    public AudioClip[] switchPlayerColor;
    public AudioClip[] switchDoorColor;

    AudioSource audioS;

    private void Awake()
    {
        if (INSTANCE == null)
            INSTANCE = this;
        else if (INSTANCE != this)
            Destroy(this);
        audioS = GetComponent<AudioSource>();
    }

    public void PlaySwitchDoorColorSound()
    {
        audioS.clip = switchDoorColor[Random.Range(0, switchDoorColor.Length)];
        audioS.Play();
    }

    public void PlayWinSound()
    {
        audioS.clip = gameOver;
        audioS.Play();
    }
}
