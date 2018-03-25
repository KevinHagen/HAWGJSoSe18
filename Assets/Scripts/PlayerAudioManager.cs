using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

    AudioSource audioS;

	// Use this for initialization
	void Start () {
        audioS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlayPickUpPowerupSound ()
    {
        audioS.clip = AudioManager.INSTANCE.pickUpPowerUp;
        audioS.Play();
    }

    public void PlayPickUpKeySound ()
    {
        audioS.clip = AudioManager.INSTANCE.pickUpKey;
        audioS.Play();
    }
}
