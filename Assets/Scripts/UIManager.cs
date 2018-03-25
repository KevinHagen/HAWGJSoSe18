using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class UIManager : MonoBehaviour {

	public static UIManager INSTANCE;

	public Button[] buttons;

	public RectTransform[] menuPositions;
	public Image menuCursor;

	private int currentMenuItemIndex;

	private void Awake()
	{
		if (INSTANCE != null && INSTANCE != this)
			Destroy(gameObject);
		else
			INSTANCE = this;

		currentMenuItemIndex = 0;
		menuCursor.rectTransform.position = menuPositions[currentMenuItemIndex].position;
	}

	private void Update()
	{
		if(Input.GetAxisRaw("Vertical1") == -1)
		{
			currentMenuItemIndex++;
			if (currentMenuItemIndex == menuPositions.Length)
				currentMenuItemIndex--;
		}
		if(Input.GetAxisRaw("Vertical1") == 1)
		{
			currentMenuItemIndex--;
			if (currentMenuItemIndex < 0)
				currentMenuItemIndex = 0;
		}
		if(Input.GetAxisRaw("Activate1") == 1)
		{
			buttons[currentMenuItemIndex].onClick.Invoke();
		}

		menuCursor.rectTransform.position = menuPositions[currentMenuItemIndex].position;
	}

	public void OnNewGame()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void Quit()
	{
#if UNITY_STANDALONE
		Application.Quit();
#endif

#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
	}
}
