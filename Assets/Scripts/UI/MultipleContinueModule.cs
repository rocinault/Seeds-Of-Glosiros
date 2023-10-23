using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class MultipleContinueModule : MonoBehaviour {

	private void Update()
	{
		ContinueButton();
	}

	public bool ContinueButton()
	{
		// Checks for any player input.
		if (Input.GetButtonDown("Continue1") || Input.GetButtonDown("Continue2") || Input.GetButtonDown("Continue3") || Input.GetButtonDown("Continue4"))
		{
			// And returns true that a button was pressed.
			return true;
		}
		else
		{
			// Otherwise returns that a button was not pressed.
			return false;
		}
	}
}
