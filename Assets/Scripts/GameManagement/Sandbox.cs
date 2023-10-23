using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UpgradeSystem;

public class Sandbox : MonoBehaviour
{

	public int m_NumRoundsToWin = 5;
	public float m_StartDelay = 3.0f;
	public float m_EndDelay = 3.0f;

	[HideInInspector] public int numPlayers;

	[Header("Environment")]
	public GameObject m_Environment;

	[Header("Various UI References")]
	public GameObject m_UI;
	public GameObject m_PlayerSelectUI;

	[Header("Camera Control Script")]
	public CameraControl m_CameralControl;
	public Text m_MessageText;

	[Header("Player Prefab and number of players")]

	private AllPlayerInformation allPlayerInformation;
	public List<PlayerManager> m_Players;
	public List<Transform> spawnPoints;

	public List<Sprite> playerNumberSprites;

	public GameObject[] characterRoster;

	private int m_RoundNumber;
	private WaitForSeconds m_StartWait;

	private MultipleContinueModule continueModule;

	private int numFightersLeft;

	private void Start()
	{
		allPlayerInformation = AllPlayerInformation.instance;

		AddPlayers();

		continueModule = GameObject.Find("EventSystem").GetComponent<MultipleContinueModule>();

		m_StartWait = new WaitForSeconds(m_StartDelay);

		SpawnPlayers();
		SetCameraTargets();

		StartCoroutine(GameLoop());
	}

	private void AddPlayers()
	{
		for (int i = 0; i < allPlayerInformation.playersInfo.Count; i++)
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

	private void SpawnPlayers()
	{
		for (int i = 0; i < numPlayers; i++)
		{
			if (allPlayerInformation.playersInfo[i].characterChoice == 1)
			{
				m_Players[i].m_Instance = Instantiate(characterRoster[0], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;
				m_Players[i].SetupEss();
			}
			else if (allPlayerInformation.playersInfo[i].characterChoice == 2)
			{
				m_Players[i].m_Instance = Instantiate(characterRoster[1], spawnPoints[i].position, spawnPoints[i].rotation) as GameObject;
				m_Players[i].SetupGrumwald();
			}

			m_Players[i].m_Character.m_PlayerNumberSprite.GetComponent<SpriteRenderer>().sprite = playerNumberSprites[i];
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


		if (continueModule.ContinueButton())
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
		ResetAllPlayers();
		DisablePlayerControl();

		m_Environment.SetActive(true);
		PlayersActive(true);

		UnlockAllAbilities();

		m_CameralControl.enabled = true;
		m_CameralControl.SetStartPositionAndSize();

		m_RoundNumber++;
		m_MessageText.text = "SANDBOX ACTIVATED";

		yield return m_StartWait;
	}


	private IEnumerator RoundPlaying()
	{
		EnablePlayerControl();

		m_MessageText.text = string.Empty;

		OneFighterLeft();

		while (numFightersLeft > 0)
		{
			if(continueModule.ContinueButton())
				SceneManager.LoadScene(0);

			yield return null;
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



	private void ResetAllPlayers()
	{
		for (int i = 0; i < m_Players.Count; i++)
		{
			m_Players[i].Reset();
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

	private void UnlockAllAbilities()
	{
		for (int i = 0; i < m_Players.Count; i++)
		{
			if (m_Players[i].name == "Ess")
			{
				m_Players[i].UnlockAllAbilities();
			}
		}
	}
}


