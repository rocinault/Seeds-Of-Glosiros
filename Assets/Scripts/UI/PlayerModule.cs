using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerModule : MonoBehaviour {

	private EventSystem eventSystem;
	private MultipleContinueModule continueModule;
	private MultipleSubmitModule submitModule;

	private GameObject continueIcon;
	private GameObject characterMenu;
	private GameObject levelMenu;
	private GameObject developmentMenu;

	private int numPlayers;

	private CharacterSelect selectP1;
	private CharacterSelect selectP2;
	private CharacterSelect selectP3;
	private CharacterSelect selectP4;

	private bool player1Active = false;
	private bool player2Active = false;
	private bool player3Active = false;
	private bool player4Active = false;

	[HideInInspector] public bool setButton = false;

	private void Awake()
	{
		eventSystem = GetComponent<EventSystem>();

		continueModule = GetComponent<MultipleContinueModule>();
		continueModule.enabled = false;

		submitModule = GetComponent<MultipleSubmitModule>();
		submitModule.enabled = false;

		continueIcon = GameObject.Find("UI/CharacterSelectMenu/ContinueIcon");

		characterMenu = GameObject.Find("UI/CharacterSelectMenu");

		// Get level menu reference BEFORE disabling it.
		levelMenu = GameObject.Find("UI/LevelSelectMenu");
		levelMenu.SetActive(false);

		developmentMenu = GameObject.Find("UI/DevelopmentLevelsMenu");
		developmentMenu.SetActive(false);
	}

	private void Update()
	{
		// Only let players join if the total number is under 4 and the character menu is active.
		if (numPlayers < 4 && characterMenu.activeSelf)
		{
			submitModule.enabled = false;

			PlayerJoin();
		}
		else if (levelMenu.activeSelf)
		{
			submitModule.enabled = true;
		}

		PlayerLeave();

		// Check if the number of players is greater than one and a character has been picked.
		if (numPlayers >= 1 && CheckForCharacterSelection())
		{
			// Run the level select function.
			LevelSelect();
		}
		else if(numPlayers <= 0)
		{
			// Otherwise set the continue icon and module to false.
			continueIcon.SetActive(false);
			continueModule.enabled = false;
		}

		// Development menu only gets activated via the onclick() from the level menu.
		if (developmentMenu.activeSelf)
		{
			DevelopmentMenuInput();
		}
	}

	private void PlayerJoin()
	{
		// Check for player 1 input and if the player is not currently active.
		if (Input.GetButtonDown("Submit1") && !player1Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				// Set player bool to active, stops the player from activating themselves multiple times.
				player1Active = true;

				// Grab a reference to the object holding the player script.
				selectP1 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + 1).GetComponent<CharacterSelect>();

				// Set its playerID string and input number (input corresponds to the input manager setup).
				selectP1.SetPlayerID("Player" + 1, 1);

				// Enable the script AFTER setting it's values so it can instantiate a cursor with the same values.
				selectP1.enabled = true;

				// Increase player number count.
				numPlayers++;
			}
		}

		if (Input.GetButtonDown("Submit2") && !player2Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player2Active = true;

				selectP2 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + 2).GetComponent<CharacterSelect>();
				selectP2.SetPlayerID("Player" + 2, 2);

				selectP2.enabled = true;

				numPlayers++;
			}
		}

		if (Input.GetButtonDown("Submit3") && !player3Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player3Active = true;

				selectP3 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + 3).GetComponent<CharacterSelect>();
				selectP3.SetPlayerID("Player" + 3, 3);

				selectP3.enabled = true;

				numPlayers++;
			}
		}

		if (Input.GetButtonDown("Submit4") && !player4Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player4Active = true;

				selectP4 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + 4).GetComponent<CharacterSelect>();
				selectP4.SetPlayerID("Player" + 4, 4);

				selectP4.enabled = true;

				numPlayers++;
			}
		}
	}

	private void PlayerLeave()
	{
		if (Input.GetButtonDown("Cancel1") && player1Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player1Active = false;
				selectP1.DestroyCharacter();
				selectP1.enabled = false;
				numPlayers--;
			}

			// Run if the level menu is active.
			if (levelMenu.activeSelf)
			{
				// Return to character menu.
				levelMenu.SetActive(false);
				characterMenu.SetActive(true);

				// Deselect the button.
				eventSystem.SetSelectedGameObject(null);
			}
		}

		if (Input.GetButtonDown("Cancel2") && player2Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player2Active = false;
				selectP2.DestroyCharacter();
				selectP2.enabled = false;
				numPlayers--;
			}

			// Run if the level menu is active.
			if (levelMenu.activeSelf)
			{
				// Return to character menu.
				levelMenu.SetActive(false);
				characterMenu.SetActive(true);

				// Deselect the button.
				eventSystem.SetSelectedGameObject(null);
			}
		}

		if (Input.GetButtonDown("Cancel3") && player3Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player3Active = false;
				selectP3.DestroyCharacter();
				selectP3.enabled = false;
				numPlayers--;
			}

			// Run if the level menu is active.
			if (levelMenu.activeSelf)
			{
				// Return to character menu.
				levelMenu.SetActive(false);
				characterMenu.SetActive(true);

				// Deselect the button.
				eventSystem.SetSelectedGameObject(null);
			}
		}

		if (Input.GetButtonDown("Cancel4") && player4Active)
		{
			// Run if the character menu is active.
			if (characterMenu.activeSelf)
			{
				player4Active = false;
				selectP4.DestroyCharacter();
				selectP4.enabled = false;
				numPlayers--;
			}

			// Run if the level menu is active.
			if (levelMenu.activeSelf)
			{
				// Return to character menu.
				levelMenu.SetActive(false);
				characterMenu.SetActive(true);

				// Deselect the button.
				eventSystem.SetSelectedGameObject(null);
			}
		}
	}

	private void LevelSelect()
	{
		// Enable the continue Icon so players know the input.
		continueIcon.SetActive(true);

		// The continue module will check for any input on the 'continue' button, function is a bool that returns true on input.
		continueModule.enabled = true;

		if (continueModule.ContinueButton())
		{
			// Stop the preview's of the characters, check first though if player is currently active.
			if(player1Active)
				selectP1.StopViewingCharacter();

			if(player2Active)
				selectP2.StopViewingCharacter();

			if(player3Active)
				selectP3.StopViewingCharacter();

			if(player4Active)
				selectP4.StopViewingCharacter();

			// Switch menu's.
			characterMenu.SetActive(false);
			levelMenu.SetActive(true);

			GameObject levelButton = GameObject.Find("UI/LevelSelectMenu/Grumwald Level/Grumwald Level Button");
			eventSystem.SetSelectedGameObject(levelButton);
		}
	}

	private void DevelopmentMenuInput()
	{
		if (setButton)
		{
			// Set the button to the sanbox level button.
			GameObject devLevelButton = GameObject.Find("UI/DevelopmentLevelsMenu/Sandbox/SandboxButton");
			eventSystem.SetSelectedGameObject(devLevelButton);

			setButton = false;
		}

		if ((Input.GetButtonDown("Cancel1") && player1Active) || (Input.GetButtonDown("Cancel2") && player2Active) || (Input.GetButtonDown("Cancel3") && player3Active) || (Input.GetButtonDown("Cancel4") && player4Active))
		{
			// Switch back to the main level menu.
			developmentMenu.SetActive(false);
			levelMenu.SetActive(true);

			// Set back the eventsystem.
			GameObject levelButton = GameObject.Find("UI/LevelSelectMenu/Grumwald Level/Grumwald Level Button");
			eventSystem.SetSelectedGameObject(levelButton);
		}
	}

	public void SetDevLevelButton()
	{
		setButton = true;
	}

	private bool CheckForCharacterSelection()
	{
		// Only run if the character menu is active.
		if (characterMenu.activeSelf)
		{
			// Iterate through all the players active.
			for (int i = 1; i <= numPlayers; i++)
			{
				// Grab a temp CharacterSelect script based on value 'i' which equals a player number.
				CharacterSelect selectTemp = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + i).GetComponent<CharacterSelect>();

				// Check if the temp has selected a character and return true if a selection has been made.
				if (selectTemp.IsCharacterPicked())
					return selectTemp.IsCharacterPicked();

				// Otherwise return false.
				else
					return false;
			}
		}

		// return false by default.
		return false;
	}
}
