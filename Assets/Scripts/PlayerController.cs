﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	public bool IsStunned { get; set; }
	public AbstractPowerUp CurrentPowerUp { get; set; }
	public PlayerUI PlayerUI { get; private set; }
    public bool HasKey { get; set; }
	public bool activatePressed;
    public Material[] playerMaterials;

	public PlayerMovement playerMovement;
	public PlayerAudioManager playerAudio;
	public ParticleSystem changeColorParticles;
	public ParticleSystem wheelDustLeft;
	public ParticleSystem wheelDustRight;
	public int playerNumber;
    public Key currentKey;
    public GameObject playerBody, playerLatch;
    public GameObject[] playerWheels;
    [Tooltip("Order: Yellow, Red, Green, Blue")]
    public Material[] bodyMaterials,latchMaterials,wheelMaterials;
    public Material[] rainbowMaterials;
    public GameObject thunderAnimationPrefab;
    public Animator animator;

    public bool controlWithKeyboard;

    public int startPosition { get; set; }
    private Colors _color;
	private bool needsTwoColors;
	private int holdTimeLeft;
    private bool isRainbow;

    private string GREEN_BUTTON = "Green";
	private string BLUE_BUTTON = "Blue";
	private string YELLOW_BUTTON = "Yellow";
	private string RED_BUTTON = "Red";
	private string ACTIVATE_BUTTON = "Activate";
	private string HORIZONTAL_AXIS = "Horizontal";
	private string VERTICAL_AXIS = "Vertical";

	public void Init()
	{
		PlayerUI = GetComponentInChildren<PlayerUI>();
		playerAudio = GetComponent<PlayerAudioManager>();
		playerAudio.Init();
        if(controlWithKeyboard)playerNumber = 5;
	}

	public IEnumerator HoldTimer(int holdTime)
	{
		holdTimeLeft = holdTime;
		while (holdTimeLeft > 0)
		{
			PlayerUI.holdTimerText.text = "" + holdTimeLeft;
			yield return new WaitForSeconds(1f);
			holdTimeLeft--;
		}
		PlayerUI.holdTimerText.text = "" + holdTimeLeft;
		if (CurrentPowerUp == null) yield break;
		CurrentPowerUp.TargetColor = _color;
		CurrentPowerUp.PunishPlayer();
		CurrentPowerUp = null;
	}

    public IEnumerator StartThunderAnimation()
    {
        GameObject thunder=Instantiate(thunderAnimationPrefab, transform);
        transform.rotation = PlayerUI.lookAtCamera;
        yield return new WaitForSeconds(2f);
        Destroy(thunder.gameObject);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void Update()
	{
        if (GameManager.INSTANCE.gameOver) return;
        //update Player UI
		PlayerUI.UpdateUI(HasKey, CurrentPowerUp != null); 
        //check if player got stunned by Thunder
		if (IsStunned) return;

        //detect shoulder-buttons and set bool to be used in Box.cs
		activatePressed = false;
		if (CurrentPowerUp==null&&(Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == 1 || Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == -1))
		{
			activatePressed = true;
		} 

        //check if player already collected a Power up; if not, stop here
		if (CurrentPowerUp == null) return;

		needsTwoColors = CurrentPowerUp is AbstractMultipleTargetPowerUp;     //check how many inputs the Power Up needs
		Debug.Log(CurrentPowerUp.GetType());
		Colors colorPressed = CheckForColorInput();                 //check if the Player pressed a collor on the controller, returns IDLE if not
        if (colorPressed == Colors.IDLE) return;    //no button pressed, stop here


		if (needsTwoColors) //power Up needs two inputs/colors
		{
			AbstractMultipleTargetPowerUp multiTargetPowerUp = (AbstractMultipleTargetPowerUp)CurrentPowerUp;
            if (multiTargetPowerUp.TargetColor == Colors.IDLE)  //target color isn't set yet, and gets the value of pressedColor
            {
                multiTargetPowerUp.TargetColor = colorPressed;
            }
            else                                                //target color is already set
            {
                multiTargetPowerUp.SecondTargetColor = colorPressed;        //second target color gets set to colorPressed
				StopCoroutine("HoldTimer");
                multiTargetPowerUp.ExecutePowerUp(_color);
                CurrentPowerUp = null;
            }
		}
		else        //power Up needs only one input/color
		{
			CurrentPowerUp.TargetColor = colorPressed;
			StopCoroutine("HoldTimer");
			CurrentPowerUp.ExecutePowerUp(_color);
			CurrentPowerUp = null;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
        if (GameManager.INSTANCE.gameOver) return;

        if (IsStunned) return;
        playerMovement.Move( Input.GetAxis(HORIZONTAL_AXIS + playerNumber), Input.GetAxis(VERTICAL_AXIS + playerNumber));
	}


	private Colors CheckForColorInput()
	{
		Colors colorPressed = Colors.IDLE;
		if (Input.GetButtonDown(BLUE_BUTTON + playerNumber))
		{
			PlayerUI.HighlightColorField(0);
			colorPressed = Colors.BLUE;
		}
		if (Input.GetButtonDown(RED_BUTTON + playerNumber))
		{
			PlayerUI.HighlightColorField(1);
			colorPressed = Colors.RED;
		}
		if (Input.GetButtonDown(YELLOW_BUTTON + playerNumber))
		{
			PlayerUI.HighlightColorField(2);
			colorPressed = Colors.YELLOW;
		}
		if (Input.GetButtonDown(GREEN_BUTTON + playerNumber))
		{
			PlayerUI.HighlightColorField(3);
			colorPressed = Colors.GREEN;
		}
		return colorPressed;
	}

	public Colors Color
    {
		get
		{
			return _color;
		}
        set
        {
            _color = value;
            gameObject.layer = LayerMask.NameToLayer("Default");
            //set different textures for color here
            isRainbow = false;
            GetComponent<PlayerAudioManager>().PlaySwitchPlayerColorSound();

            if (_color != Colors.IDLE)
			{
                GetComponent<MeshRenderer>().material = playerMaterials[(int)_color];
                playerBody.GetComponent<MeshRenderer>().material = bodyMaterials[(int)_color];
                playerLatch.GetComponent<MeshRenderer>().material = latchMaterials[(int)_color];
                for(int i=0;i<playerWheels.Length;i++)
                {
                    playerWheels[i].GetComponent<MeshRenderer>().material = wheelMaterials[(int)_color];
                }
				changeColorParticles.Play();
                switch (_color)
                {
                    case Colors.BLUE:
                        gameObject.layer = LayerMask.NameToLayer("Blue");
                        break;
                    case Colors.GREEN:
                        gameObject.layer = LayerMask.NameToLayer("Green");
                        break;
                    case Colors.RED:
                        gameObject.layer = LayerMask.NameToLayer("Red");
                        break;
                    case Colors.YELLOW:
                        gameObject.layer = LayerMask.NameToLayer("Yellow");
                        break;
					case Colors.RAINBOW:
						gameObject.layer = LayerMask.NameToLayer("Rainbow");
                        isRainbow = true;
                        StartCoroutine("StartRainbowColors");
						break;
                }
 			}
        }
    }

    public IEnumerator StartRainbowColors()
    {
        MeshRenderer meshRenderer = playerBody.GetComponent<MeshRenderer>();
        int counter=0;
        while(isRainbow)
        {
            meshRenderer.material = rainbowMaterials[counter % rainbowMaterials.Length];
            counter++;
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}
