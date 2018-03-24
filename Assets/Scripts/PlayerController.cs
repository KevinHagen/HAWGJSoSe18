using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	public bool IsStunned { get; set; }
	public AbstractPowerUp CurrentPowerUp { get; set; }
	public PlayerUI PlayerUI { get; private set; }
    public bool hasKey { get; set; }

    private Colors _color;
	private bool needsTwoColors;

	private void Awake()
	{
		PlayerUI = GetComponent<PlayerUI>();
	}

    void Update()
	{
		if (IsStunned) return;

		//MovementAbfrage

		if (CurrentPowerUp == null) return;

		needsTwoColors = CurrentPowerUp.GetType() == typeof(AbstractMultipleTargetPowerUp);
		Colors colorPressed = CheckForColorInput();

		if (needsTwoColors)
		{
			AbstractMultipleTargetPowerUp multiTargetPowerUp = (AbstractMultipleTargetPowerUp)CurrentPowerUp;
			if (CurrentPowerUp.TargetColor != Colors.IDLE)
				multiTargetPowerUp.TargetColor = colorPressed;
			else
			{
				multiTargetPowerUp.SecondTargetColor = colorPressed;
				multiTargetPowerUp.ExecutePowerUp();
			}
		}
		else
		{
			CurrentPowerUp.TargetColor = colorPressed;
			CurrentPowerUp.ExecutePowerUp();
		}
	}

	private Colors CheckForColorInput()
	{
		Colors colorPressed = Colors.IDLE;
		if (Input.GetButtonDown("Blue"))
		{
			PlayerUI.HighlightColorField(0);
			colorPressed = Colors.BLUE;
		}
		if (Input.GetButtonDown("Red"))
		{
			PlayerUI.HighlightColorField(1);
			colorPressed = Colors.RED;
		}
		if (Input.GetButtonDown("Yellow"))
		{
			PlayerUI.HighlightColorField(2);
			colorPressed = Colors.YELLOW;
		}
		if (Input.GetButtonDown("Green"))
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
            //set different textures for color here
			if(_color != Colors.BLACK || _color != Colors.RAINBOW || _color != Colors.IDLE)
			{
				PlayerUI.SetKeyColor(_color);
			}
        }
    }
}
