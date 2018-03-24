using System.Collections;
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
	public int playerNumber;

    private Colors _color;
	private bool needsTwoColors;
	private int holdTimeLeft;

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
		CurrentPowerUp.TargetColor = _color;
		CurrentPowerUp.PunishPlayer();
		CurrentPowerUp = null;
	}

    void Update()
	{
        //update Player UI
		PlayerUI.UpdateUI(HasKey, CurrentPowerUp != null); 
        //check if player got stunned by Thunder
		if (IsStunned) return;

        //detect shoulder-buttons and set bool to be used in Box.cs
		activatePressed = false;
		if (Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == 1 || Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == -1)
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
            if (multiTargetPowerUp.TargetColor == Colors.IDLE)  //target color isn't set yes, and gets the value of pressedColor
            {
                multiTargetPowerUp.TargetColor = colorPressed;
            }
            else                                                //target color is already set
            {
                multiTargetPowerUp.SecondTargetColor = colorPressed;        //second target color gets set to colorPressed
				StopAllCoroutines();
                multiTargetPowerUp.ExecutePowerUp();
                CurrentPowerUp = null;
            }
		}
		else        //power Up needs only one input/color
		{
			CurrentPowerUp.TargetColor = colorPressed;
			StopAllCoroutines();
			CurrentPowerUp.ExecutePowerUp();
			CurrentPowerUp = null;
		}
	}

	// Update is called once per frame
	void FixedUpdate()
	{
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

            if (_color != Colors.IDLE)
			{
                GetComponent<MeshRenderer>().material = playerMaterials[(int)_color];
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
						break;
                }
 			}
        }
    }
}
