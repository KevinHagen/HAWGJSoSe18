﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour {

    AudioSource audioS;

	// Use this for initialization
	void Awake () {
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

    public void PlaySwitchPlayerColorSound ()
    {
        audioS.clip = AudioManager.INSTANCE.switchPlayerColor[Random.Range(0, AudioManager.INSTANCE.switchPlayerColor.Length)];
        audioS.Play();
    }
}
