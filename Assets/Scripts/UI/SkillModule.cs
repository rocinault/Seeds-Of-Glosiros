using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;
using UpgradeSystem;

public class SkillModule : MonoBehaviour
{
	[HideInInspector] public bool resume;

	private EventSystem eventSystem;
	private MultipleContinueModule continueModule;
	private MultipleSubmitModule submitModule;
	private StandaloneInputModule inputModule;

	private GameManager gameManager;

	private GameObject menu;

	private bool player1menuFirstTimeOpen = true;
	private bool player2menuFirstTimeOpen = true;
	private bool player3menuFirstTimeOpen = true;
	private bool player4menuFirstTimeOpen = true;

	private void Awake()
	{
		eventSystem = GetComponent<EventSystem>();

		continueModule = GetComponent<MultipleContinueModule>();
		continueModule.enabled = false;

		submitModule = GetComponent<MultipleSubmitModule>();
		submitModule.enabled = false;

		inputModule = GetComponent<StandaloneInputModule>();
		inputModule.enabled = false;

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		menu = GameObject.Find("UI/PlayerSelect");
		menu.SetActive(false);
	}

	private void Start()
	{
		// Set the event system to player 1 skill tree button.
		GameObject start = GameObject.Find("UI/PlayerSelect/Buttons/ButtonPlayer1");
		eventSystem.SetSelectedGameObject(start);
	}

	public void OnEnable()
	{
		continueModule.enabled = true;
		submitModule.enabled = true;
		inputModule.enabled = true;

		ReturnToMenu();
	}

	private void Update()
	{
		// Constantly make sure all players have control if the main menu is active.
		if (menu.activeSelf)
			SetControlToAllPlayers();
	}

	public void SetControlToPlayer(int ID)
	{
		// Disable all players ability to submit.
		submitModule.enabled = false;

		// Stop players from continuing. 
		continueModule.enabled = false;

		// Set the input module to players ID.
		inputModule.horizontalAxis = "Horizontal" + ID;
		inputModule.verticalAxis = "Vertical" + ID;
		inputModule.submitButton = "Submit" + ID;
		inputModule.cancelButton = "Cancel" + ID;
	}

	public void SetControlToAllPlayers()
	{
		// Reset the submit module.
		submitModule.enabled = true;

		// Reset the continue module.
		continueModule.enabled = true;

		// reset the input module to default values.
		inputModule.horizontalAxis = "Horizontal";
		inputModule.verticalAxis = "Vertical";
		inputModule.submitButton = "Submit";
		inputModule.cancelButton = "Cancel";
	}

	public void ActivateSkillTree(int ID)
	{
		// Use switch to detremine which skill tree is getting set as active, check whether there are enough players otherwise ignore input.
		switch (ID)
		{
			case 1:
				// Check whether the player is controlling Grumwald, if so don't activate the skill tree.
				if (gameManager.m_Players[ID - 1].name == "Grumwald")
				{
					// Reset the menu and cursor then if input is recieved.
					menu.SetActive(true);
					ReturnToMenu();
					SetControlToAllPlayers();
					break;
				}
				else
				{
					// Activate the players skill tree.
					gameManager.skillTrees[ID - 1].SetActive(true);

					// If the player is opening the menu for the first time let the menu set up all the nodes.
					if (player1menuFirstTimeOpen)
					{
						// Set the bool to false.
						player1menuFirstTimeOpen = false;
						break;
					}
					// Otherwise update the nodes so the player can purchase the skills that scale with them leveling up.
					else
					{
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateNodes();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdatePlayerLevel();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateSkillPoints();
						break;
					}
				}
			case 2:
				// Check whether the number of players is less than two or if their character is Grumwald.
				if (gameManager.numPlayers < 2 || gameManager.m_Players[ID - 1].name == "Grumwald")
				{
					// Reset the menu and cursor then if input is recieved.
					menu.SetActive(true);
					ReturnToMenu();
					SetControlToAllPlayers();
					break;
				}
				else
				{
					// Otherwise activate the players skill tree.
					gameManager.skillTrees[ID - 1].SetActive(true);

					// If the player is opening the menu for the first time let the menu set up all the nodes.
					if (player2menuFirstTimeOpen)
					{
						player2menuFirstTimeOpen = false;
						break;
					}
					// Otherwise update the nodes so the player can purchase the skills that scale with them leveling up.
					else
					{
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateNodes();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdatePlayerLevel();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateSkillPoints();
						break;
					}
				}

			case 3:
				// Check whether the number of players is less than three or if their character is Grumwald.
				if (gameManager.numPlayers < 3 || gameManager.m_Players[ID - 1].name == "Grumwald")
				{
					// Reset the menu and cursor then if input is recieved.
					menu.SetActive(true);
					ReturnToMenu();
					SetControlToAllPlayers();
					break;
				}
				else
				{
					// Otherwise activate the players skill tree.
					gameManager.skillTrees[ID - 1].SetActive(true);

					// If the player is opening the menu for the first time let the menu set up all the nodes.
					if (player3menuFirstTimeOpen)
					{
						player3menuFirstTimeOpen = false;
						break;
					}
					// Otherwise update the nodes so the player can purchase the skills that scale with them leveling up.
					else
					{
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateNodes();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdatePlayerLevel();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateSkillPoints();
						break;
					}
				}
			case 4:
				// Check whether the number of players is less than four or if their character is Grumwald.
				if (gameManager.numPlayers < 4 || gameManager.m_Players[ID - 1].name == "Grumwald")
				{
					// Reset the menu and cursor then if input is recieved.
					menu.SetActive(true);
					ReturnToMenu();
					SetControlToAllPlayers();
					break;
				}
				else
				{
					// Otherwise activate the players skill tree.
					gameManager.skillTrees[ID - 1].SetActive(true);

					// If the player is opening the menu for the first time let the menu set up all the nodes.
					if (player4menuFirstTimeOpen)
					{
						player4menuFirstTimeOpen = false;
						break;
					}
					// Otherwise update the nodes so the player can purchase the skills that scale with them leveling up.
					else
					{
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateNodes();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdatePlayerLevel();
						gameManager.skillTrees[ID - 1].GetComponent<SkillMenu>().UpdateSkillPoints();
						break;
					}
				}
			default:
				break;
		}
	}

	public void SetCursor()
	{
		for (int i = 0; i < gameManager.skillTrees.Length; i++)
		{
			if (gameManager.skillTrees[i].activeSelf)
			{
				// Sets the players cursor to the done button.
				Transform transform = gameManager.skillTrees[i].transform.Find("Body/Player Return");
				eventSystem.SetSelectedGameObject(transform.gameObject);
			}
		}
	}

	public void ReturnToMenu()
	{
		// Sets the cursor to the player 1 skill tree button.
		GameObject start = GameObject.Find("UI/PlayerSelect/Buttons/ButtonPlayer1");
		eventSystem.SetSelectedGameObject(start);
	}

	public void SetResume(bool state)
	{
		// Set whether or not the gameloop can procced.
		resume = state;
	}

	public bool CanResume()
	{
		// Access current state for whether gameloop can continue.
		return resume;
	}

	public void OnDisable()
	{
		continueModule.enabled = false;
		submitModule.enabled = false;
		inputModule.enabled = false;
	}
}
