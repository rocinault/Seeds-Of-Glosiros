using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class MultipleSubmitModule : MonoBehaviour {

	private EventSystem eventSystem;

	private void Awake()
	{
		eventSystem = GetComponent<EventSystem>();
	}

	private void Update()
	{
		// For the main menu constantly check for input.
		ConfirmButton();
	}

	public bool ConfirmButton()
	{
		// Checks for any player input.
		if (Input.GetButtonDown("Submit1") || Input.GetButtonDown("Submit2") || Input.GetButtonDown("Submit3") || Input.GetButtonDown("Submit4"))
		{
			// Gets the currently selected object.
			GameObject objectSelected = eventSystem.currentSelectedGameObject;

			if (objectSelected != null)
			{
				// Gets the button component from it.
				Button buttonSelected = objectSelected.GetComponent<Button>();

				// Fires off all onclick's set to it. 
				buttonSelected.onClick.Invoke();

				// And returns true that a button was pressed.
				return true;
			}
		}

		// Otherwise returns that a button was not pressed.
		return false;
	}

	public bool SubmitInput()
	{
		// Checks for any player input.
		if (Input.GetButtonDown("Submit1") || Input.GetButtonDown("Submit2") || Input.GetButtonDown("Submit3") || Input.GetButtonDown("Submit4"))
		{
			// Returns true that a button was pressed.
			return true;
		}
		else
		{
			// Otherwise returns that a button was not pressed.
			return false;
		}
	}

}
