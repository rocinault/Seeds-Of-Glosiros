using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UpgradeSystem;

public class GUIManager : MonoBehaviour {
/*

	[Header("Override unity scripts")]
	public EventSystem ES;
	public StandaloneInputModule input;

	public GameManager gameManager;

	[Header("Reference to the environment")]
	public GameObject environment;

	[Header("Player 1 Select Button")]
	public GameObject p1Select;
	public GameObject selectMenu;

	private float horizontalAmount = 1;
	private float submitAmount = 1;


	[HideInInspector] public bool resume;

	private void Start()
	{
		input.verticalAxis = "Vertical" + 1;
		SetControlToAllPlayers();
	}

	public void Update()
	{
		int inActivemenu = 0;

		for (int i = 0; i < gameManager.m_SkillTrees.Length; i++)
		{
			if (gameManager.m_SkillTrees[i].activeSelf == false)
			{
				inActivemenu++;
			}

			if(gameManager.m_SkillTrees[i].activeSelf == true)
			{
				gameManager.m_SkillTrees[i].GetComponent<SkillMenu>().UpdatePlayerLevel();
			}
		}

		if (inActivemenu == gameManager.m_SkillTrees.Length)
		{
			ReadAllSubmitInputs();
			ReadAllHorizontalInputs();
		}

		if (!resume)
		{

		}
	}

	public void SetToDone()
	{
		for (int i = 0; i < gameManager.m_SkillTrees.Length; i++)
		{
			if (gameManager.m_SkillTrees[i].activeSelf)
			{
				Transform transform = gameManager.m_SkillTrees[i].transform.Find("Body/Player Return");
				ES.SetSelectedGameObject(transform.gameObject);
			}
		}
	}

	public void SetToPlayer()
	{
		ES.SetSelectedGameObject(p1Select);
	}

	public void SetControlToPlayer1()
	{
		input.horizontalAxis = "Horizontal" + 1;
		input.verticalAxis = "Vertical" + 1;
		input.submitButton = "Submit" + 1;
	}

	public void SetControlToPlayer2()
	{
		input.horizontalAxis = "Horizontal" + 2;
		input.verticalAxis = "Vertical" + 2;
		input.submitButton = "Submit" + 2;
	}

	public void SetControlToPlayer3()
	{
		input.horizontalAxis = "Horizontal" + 3;
		input.verticalAxis = "Vertical" + 3;
		input.submitButton = "Submit" + 3;
	}

	public void SetControlToPlayer4()
	{
		input.horizontalAxis = "Horizontal" + 4;
		input.verticalAxis = "Vertical" + 4;
		input.submitButton = "Submit" + 4;
	}

	public void SetControlToAllPlayers()
	{
		input.horizontalAxis = "Horizontal" + horizontalAmount.ToString();
		input.submitButton = "Submit" + submitAmount.ToString();
	}

	private void ReadAllHorizontalInputs()
	{
		if (input.input.GetButtonDown("Horizontal" + 1))
		{
			horizontalAmount = 1;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Horizontal" + 2))
		{
			horizontalAmount = 2;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Horizontal" + 3))
		{
			horizontalAmount = 3;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Horizontal" + 4))
		{
			horizontalAmount = 4;
			SetControlToAllPlayers();
		}

	}
	private void ReadAllSubmitInputs()
	{
		if (input.input.GetButtonDown("Submit" + 1))
		{
			submitAmount = 1;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Submit" + 2))
		{
			submitAmount = 2;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Submit" + 3))
		{
			submitAmount = 3;
			SetControlToAllPlayers();
		}
		else if (input.input.GetButtonDown("Submit" + 4))
		{
			submitAmount = 4;
			SetControlToAllPlayers();
		}
	}

	public void SetResumeToTrue()
	{
		resume = true;
	}

	public void SetResumeToFalse()
	{
		resume = false;
	}

	public bool CanResume()
	{
		return resume;
	}

	public void SetPlayer1Menu(bool state)
	{
		gameManager.m_SkillTrees[0].SetActive(state);
	}

	public void SetPlayer2Menu(bool state)
	{
		int numPlayer = 0;

		for(int i = 0; i < gameManager.m_SkillTrees.Length; i++)
		{
			numPlayer++;
		}

		if (numPlayer > 1)
			gameManager.m_SkillTrees[1].SetActive(state);

		else
			selectMenu.SetActive(true);
	}

	public void SetPlayer3Menu(bool state)
	{
		int numPlayer = 0;

		for (int i = 0; i < gameManager.m_SkillTrees.Length; i++)
		{
			numPlayer++;
		}

		if (numPlayer > 2)
			gameManager.m_SkillTrees[2].SetActive(state);

		else
			selectMenu.SetActive(true);
	}

	public void SetPlayer4Menu(bool state)
	{
		int numPlayer = 0;

		for (int i = 0; i < gameManager.m_SkillTrees.Length; i++)
		{
			numPlayer++;
		}

		if (numPlayer > 3)
			gameManager.m_SkillTrees[3].SetActive(state);

		else
			selectMenu.SetActive(true);
	}

	*/
}

