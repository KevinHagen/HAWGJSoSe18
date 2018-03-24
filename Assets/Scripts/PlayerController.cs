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

	private void Awake()
	{
		PlayerUI = GetComponentInChildren<PlayerUI>();
	}

    void Update()
	{
        if(Input.GetButtonDown(BLUE_BUTTON+playerNumber))
        {
            Debug.Log(Input.GetJoystickNames());
            Debug.Log("blue button pressed");
        }
		PlayerUI.UpdateUI(HasKey, CurrentPowerUp != null);
		if (IsStunned) return;

		activatePressed = false;
		if (Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == 1 || Input.GetAxisRaw(ACTIVATE_BUTTON + playerNumber) == -1)
		{
			activatePressed = true;
		} 

		if (CurrentPowerUp == null) return;

		needsTwoColors = CurrentPowerUp.GetType() == typeof(AbstractMultipleTargetPowerUp);
		Colors colorPressed = CheckForColorInput();

        if (colorPressed == Colors.IDLE) return;

		if (needsTwoColors)
		{
			AbstractMultipleTargetPowerUp multiTargetPowerUp = (AbstractMultipleTargetPowerUp)CurrentPowerUp;
			if (CurrentPowerUp.TargetColor != Colors.IDLE)
				multiTargetPowerUp.TargetColor = colorPressed;
			else
			{
				multiTargetPowerUp.SecondTargetColor = colorPressed;
                if (colorPressed == Colors.IDLE) return;
                multiTargetPowerUp.ExecutePowerUp();
				CurrentPowerUp = null;
			}
		}
		else
		{
			CurrentPowerUp.TargetColor = colorPressed;
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

            if (_color != Colors.BLACK && _color != Colors.RAINBOW && _color != Colors.IDLE)
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
                }
                PlayerUI.SetKeyColor(_color);
			}
        }
    }
}
