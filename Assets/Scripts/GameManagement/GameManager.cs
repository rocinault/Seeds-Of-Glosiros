using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UpgradeSystem;

public class GameManager : MonoBehaviour
{
	public int m_NumRoundsToWin = 5;
	public float m_StartDelay = 3.0f;
	public float m_EndDelay = 3.0f;

	[HideInInspector] public int numPlayers;

	[Header("Environment")]
	public GameObject m_Environment;

	[Header("UI Reference to upgrade menu")]
	public GameObject m_PlayerSelectUI;

	[Header("Camera Control Script")]
	public CameraControl m_CameralControl;
	public Text m_MessageText;

    [Header("Spawn points for players")]
	public List<Transform> spawnPoints;

	[Header("Player number sprites")]
	public List<Sprite> playerNumberSprites;

	[Header("Skills trees")]
	public GameObject[] skillTrees;

	[Header("Roster of characters created")]
	public GameObject[] characterRoster;

	private AllPlayerInformation allPlayerInformation;
	[HideInInspector] public List<PlayerManager> m_Players = new List<PlayerManager>();

	private int m_RoundNumber;
	private WaitForSeconds m_StartWait;
	private WaitForSeconds m_EndWait;
	private WaitForSeconds m_GameEndWait;

	private PlayerManager m_RoundWinner;
	private PlayerManager m_GameWinner;
	private SkillModule skillModule;

	private int numFightersLeft;

	private void Start()
	{
		allPlayerInformation = AllPlayerInformation.instance;

		AddPlayers();

        skillModule = GameObject.Find("EventSystem").GetComponent<SkillModule>();
		skillModule.enabled = false;

		m_StartWait = new WaitForSeconds(m_StartDelay);
		m_EndWait = new WaitForSeconds(m_EndDelay);
		m_GameEndWait = new WaitForSeconds(m_EndDelay * 1.5f);

		CreateSkillTrees();
		SpawnPlayers();
		SetCameraTargets();

		StartCoroutine(GameLoop());
	}

	private void AddPlayers()
	{
		for(int i = 0; i < allPlayerInformation.playersInfo.Count; i++)
		{
			if (allPlayerInformation.playersInfo[i].isActive)
			{
				PlayerManager playerManager = new PlayerManager();
				m_Players.Add(playerManager);

				m_Players[i].m_PlayerNumber = allPlayerInformation.playersInfo[i].playerIndex;
				m_Players[i].m_PlayerColour = allPlayerInformation.playersInfo[i].playerColor;
				m_Players[i].m_SpawnPoint = spawnPoints[i];

				numPlayers++;
			}
		}
	}

	private void CreateSkillTrees()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			skillTrees[i] = Instantiate(skillTrees[i], GameObject.Find("UI").transform);
			skillTrees[i].SetActive(false);
		}
	}

	private void SpawnPlayers()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			if(allPlayerInformation.playersInfo[i].characterChoice == 1)
			{
				m_Players[i].m_Instance = Instantiate(characterRoster[0], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;
				m_Players[i].SetupEss();

			}
			else if(allPlayerInformation.playersInfo[i].characterChoice == 2)
			{
				m_Players[i].m_Instance = Instantiate(characterRoster[1], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;
				m_Players[i].SetupGrumwald();
			}

			m_Players[i].m_Character.m_PlayerNumberSprite.GetComponent<SpriteRenderer>().sprite = playerNumberSprites[i];


			skillTrees[i].GetComponentInChildren<SkillTree>().SetPlayerNumber(m_Players[i].m_PlayerNumber);
			skillTrees[i].GetComponentInChildren<SkillTree>().SetPlayerLevel(m_Players[i].m_PlayerLevel);
		}
	}

	private void SetCameraTargets()
    {                                     
        Transform[] targets = new Transform[m_Players.Count];

		for (int i = 0; i < targets.Length; i++)
		{
			targets[i] = m_Players[i].m_Instance.transform;
		}

		m_CameralControl.targets = targets;
	}


	private IEnumerator GameLoop()
	{
		yield return StartCoroutine(RoundStarting());

		yield return StartCoroutine(RoundPlaying());

		yield return StartCoroutine(RoundEnding());


		yield return StartCoroutine(Upgrade());
		

		if (m_GameWinner != null)
		{
			SceneManager.LoadScene(0);
		}
		else
		{
			StartCoroutine(GameLoop());
		}
	}


	private IEnumerator RoundStarting()
	{
		skillModule.enabled = false;

		ResetAllPlayers();
		DisablePlayerControl();

		m_Environment.SetActive(true);
		PlayersActive(true);

		m_CameralControl.enabled = true;
		m_CameralControl.SetStartPositionAndSize();

		m_RoundNumber++;
		m_MessageText.text = "ROUND " + m_RoundNumber;

		yield return m_StartWait;
	}


	private IEnumerator RoundPlaying()
	{
		EnablePlayerControl();

		m_MessageText.text = string.Empty;

		while (!OneFighterLeft())
		{

			yield return null;
		}
		
	}


	private IEnumerator RoundEnding()
	{
		DisablePlayerControl();

		for (int i = 0; i < m_Players.Count; i++)
		{
			if(m_Players[i].m_Instance.activeSelf)
				MakePlayersIdle();
		}

		m_RoundWinner = null;

		m_RoundWinner = GetRoundWinner();

		if (m_RoundWinner != null)
			m_RoundWinner.m_Wins++;

		m_GameWinner = GetGameWinner();

		string message = EndMessage();
		m_MessageText.text = message;

		if (m_GameWinner != null)
			yield return m_GameEndWait;

		else
			yield return m_EndWait;
	}


	private IEnumerator Upgrade()
	{
		if (m_GameWinner == null)
		{
			PlayersActive(false);

			skillModule.enabled = true;
			skillModule.SetResume(false);

			m_Environment.SetActive(false);
			m_PlayerSelectUI.SetActive(true);

			m_MessageText.text = string.Empty;
			m_CameralControl.enabled = false;

			while (!skillModule.CanResume())
			{
				yield return null;
			}
		}
	}


	private bool OneFighterLeft()
	{
		numFightersLeft = 0;
        
        for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_Instance.activeSelf)
				numFightersLeft++;
		}

		return numFightersLeft <= 1;
	}


	private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_Instance.activeSelf)
			{
				IncreasePlayerLevel(1, i);
				m_Players[i].DeathAnim();
				return m_Players[i];
			}
		}

		return null;
	}


	private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].m_Wins == m_NumRoundsToWin)
				return m_Players[i];
		}

		return null;
	}


	private string EndMessage()
	{
		string message = "DRAW!";

		if (m_RoundWinner != null)
			message = m_RoundWinner.m_ColouredPlayerText + " WINS THE ROUND! ";

		message += "\n\n\n\n";
                    
        for (int i = 0; i < m_Players.Count; i++)
		{
			message += m_Players[i].m_ColouredPlayerText + ": " + m_Players[i].m_Wins + " WINS\n";
		}

		if (m_GameWinner != null)
			message = m_GameWinner.m_ColouredPlayerText + " WINS THE GAME!";

		return message;
	}


	private void ResetAllPlayers()
    {               
        for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].Reset();
		}
	}


	private void MakePlayersIdle()
    {
        for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].Idle();
		}
	}


	private void EnablePlayerControl()
    {
        for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].EnableControl();
		}
	}


	private void DisablePlayerControl()
    {
        for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].DisableControl();
		}
	}

	private void PlayersActive(bool state)
	{ 
        for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].InActivePlayer(state);
		}
	}

	public void PlayerIsDead(int playerNum)
	{
		for (int i = 0; i < skillTrees.Length; i++)
		{
			if (skillTrees[i].GetComponentInChildren<SkillTree>().playerNumber == playerNum)
			{
				skillTrees[i].GetComponentInChildren<SkillTree>().IncreaseSkillPoints(numFightersLeft);
			}
		}
	}

	public void SkillPurchased(string id, int playerNum)
	{
		m_Players[playerNum - 1].WhichSkillIsPurchased(id);
	}

	public void IncreasePlayerLevel(int amount, int playerNum)
	{
		m_Players[playerNum].m_PlayerLevel += amount;

		skillTrees[playerNum].GetComponentInChildren<SkillTree>().SetPlayerLevel(m_Players[playerNum].m_PlayerLevel);
	}
}

