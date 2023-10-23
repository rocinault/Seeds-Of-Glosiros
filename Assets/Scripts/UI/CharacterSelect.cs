using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
	public GameObject cursorPrefab;
	public Color playerColour;

	private GameObject cursor;
	private CharacterRoster character;

	private string playerID = " ";

	private int characterID;
	private int inputID;

	private bool characterPicked = false;

	// Called when the script gets enabled.
	private void OnEnable()
	{
		cursor = GameObject.Instantiate(cursorPrefab, GameObject.Find("UI/CharacterSelectMenu").transform) as GameObject;
		cursor.GetComponent<CursorInputModule>().playerID = playerID;
		cursor.GetComponent<CursorInputModule>().inputID = inputID;

		Image tint = GetComponent<Image>();
		tint.color = new Color(playerColour.r, playerColour.g, playerColour.b, 1.0f);

		Text join = GetComponentInChildren<Text>();
		join.text = " ";
	}

	public void SetPlayerID(string ID, int input)
	{
		playerID = ID;
		inputID = input;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Submit" + inputID) && !characterPicked)
		{
			characterID = cursor.GetComponent<CursorInputModule>().CharacterSelected();
			DestroyObject(cursor);

			character = GameObject.Find("Character Container").GetComponent<CharacterRoster>();
			character.ViewCharacter(characterID, playerID);

			Text ready = GetComponentInChildren<Text>();
			ready.text = "Ready!";

			characterPicked = true;
		}
	}

	// Called when the script gets disabled.
	public void OnDisable()
	{
		DestroyObject(cursor);

		Image tint = GetComponent<Image>();
		tint.color = new Color(0.0f, 0.0f, 0.0f, 1.0f);

		Text join = GetComponentInChildren<Text>();
		join.text = "Press A to join";

		characterPicked = false;
	}

	public void StopViewingCharacter()
	{
		// The player has pressed cancel so stop viewing the character and allow for a change, sets the character as inactive.
		character.CharacterConfirmed(characterID, playerID);
	}

	public void DestroyCharacter()
	{
		// Stops viewing the character by destroying the instance previously created.
		character.StopViewingCharacter(characterID, playerID);
	}

	public bool IsCharacterPicked()
	{
		return characterPicked;
	}

	public int CharacterChoice()
	{
		return characterID;
	}

	public string GetPlayerID()
	{
		return playerID;
	}
}


