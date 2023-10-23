using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRoster : MonoBehaviour {

	public GameObject essPrefab;
	public GameObject knightPrefab;

	[HideInInspector] public AllPlayerInformation allPlayerInfo;
	[HideInInspector] public List<GameObject> view = new List<GameObject>();

	private GameObject ess1;
	private GameObject ess2;
	private GameObject ess3;
	private GameObject ess4;

	private GameObject knight1;
	private GameObject knight2;
	private GameObject knight3;
	private GameObject knight4;

	private void Start()
	{
		allPlayerInfo = AllPlayerInformation.instance;
	}

	public void ViewCharacter(int characterID, string player)
	{
		switch (characterID)
		{
			case 1:
				InstantiateEss(player);
				break;

			case 2:
				InstantiateKnight(player);
				break;

			default:
				break;
		}
	}

	public void StopViewingCharacter(int characterID, string player)
	{
		switch (characterID)
		{
			case 1:
				DestroyEss(player);
				break;

			case 2:
				DestroyKnight(player);
				break;

			default:
				break;
		}
	}

	public void CharacterConfirmed(int characterID, string player)
	{
		switch (characterID)
		{
			case 1:
				DeactivateEss(player);
				break;

			case 2:
				DeactivateKnight(player);
				break;

			default:
				break;
		}
	}

	private void InstantiateEss(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				// Instantiate the object.
				ess1 = GameObject.Instantiate(essPrefab, GameObject.Find("Character Container").transform) as GameObject;

				// Tint the sprite colour.
				SpriteRenderer tint1 = ess1.GetComponent<SpriteRenderer>();
				Color colRef1 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 1").GetComponent<CharacterSelect>().playerColour;
				tint1.color = new Color(colRef1.r, colRef1.g, colRef1.b, 1.0f);

				// Preview the character on screen.
				view.Add(ess1);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(1, 1);
				break;

			case "Player2":
				// Instantiate the object.
				ess2 = GameObject.Instantiate(essPrefab, GameObject.Find("Character Container").transform) as GameObject;
				ess2.transform.position += new Vector3(18f, 0, 0);
				
				// Tint the sprite colour.
				SpriteRenderer tint2 = ess2.GetComponent<SpriteRenderer>();
				Color colRef2 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 2").GetComponent<CharacterSelect>().playerColour;
				tint2.color = new Color(colRef2.r, colRef2.g, colRef2.b, 1.0f);

				view.Add(ess2);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(2, 1);
				break;

			case "Player3":
				ess3 = GameObject.Instantiate(essPrefab, GameObject.Find("Character Container").transform) as GameObject;
				ess3.transform.position += new Vector3(35.7f, 0, 0);

				SpriteRenderer tint3 = ess3.GetComponent<SpriteRenderer>();
				Color colRef3 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 3").GetComponent<CharacterSelect>().playerColour;
				tint3.color = new Color(colRef3.r, colRef3.g, colRef3.b, 1.0f);

				view.Add(ess3);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(3, 1);
				break;

			case "Player4":
				ess4 = GameObject.Instantiate(essPrefab, GameObject.Find("Character Container").transform) as GameObject;
				ess4.transform.position += new Vector3(53.4f, 0, 0);

				SpriteRenderer tint4 = ess4.GetComponent<SpriteRenderer>();
				Color colRef4 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 4").GetComponent<CharacterSelect>().playerColour;
				tint4.color = new Color(colRef4.r, colRef4.g, colRef4.b, 1.0f);

				view.Add(ess4);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(4, 1);
				break;

			default:
				break;
		}
	}

	private void DestroyEss(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				view.Remove(ess1);
				RemoveFromRoster(1, 1);
				DestroyObject(ess1);
				break;

			case "Player2":
				view.Remove(ess2);
				RemoveFromRoster(2, 1);
				DestroyObject(ess2);
				break;

			case "Player3":
				view.Remove(ess3);
				RemoveFromRoster(3, 1);
				DestroyObject(ess3);
				break;

			case "Player4":
				view.Remove(ess4);
				RemoveFromRoster(4, 1);
				DestroyObject(ess4);
				break;

			default:
				break;
		}
	}

	private void DeactivateEss(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				view.Remove(ess1);
				ess1.SetActive(false);
				break;

			case "Player2":
				view.Remove(ess2);
				ess2.SetActive(false);
				break;

			case "Player3":
				view.Remove(ess3);
				ess3.SetActive(false);
				break;

			case "Player4":
				view.Remove(ess4);
				ess4.SetActive(false);
				break;

			default:
				break;
		}
	}

	private void InstantiateKnight(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				knight1 = GameObject.Instantiate(knightPrefab, GameObject.Find("Character Container").transform) as GameObject;

				SpriteRenderer tint1 = knight1.GetComponent<SpriteRenderer>();
				Color colRef1 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 1").GetComponent<CharacterSelect>().playerColour;
				tint1.color = new Color(colRef1.r, colRef1.g, colRef1.b, 1.0f);

				view.Add(knight1);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(1, 2);
				break;

			case "Player2":
				knight2 = GameObject.Instantiate(knightPrefab, GameObject.Find("Character Container").transform) as GameObject;
				knight2.transform.position += new Vector3(18f, 0, 0);

				SpriteRenderer tint2 = knight2.GetComponent<SpriteRenderer>();
				Color colRef2 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 2").GetComponent<CharacterSelect>().playerColour;
				tint2.color = new Color(colRef2.r, colRef2.g, colRef2.b, 1.0f);

				view.Add(knight2);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(2, 2);
				break;

			case "Player3":
				knight3 = GameObject.Instantiate(knightPrefab, GameObject.Find("Character Container").transform) as GameObject;
				knight3.transform.position += new Vector3(35.7f, 0, 0);

				SpriteRenderer tint3 = knight3.GetComponent<SpriteRenderer>();
				Color colRef3 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 3").GetComponent<CharacterSelect>().playerColour;
				tint3.color = new Color(colRef3.r, colRef3.g, colRef3.b, 1.0f);

				view.Add(knight3);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(3, 2);
				break;

			case "Player4":
				knight4 = GameObject.Instantiate(knightPrefab, GameObject.Find("Character Container").transform) as GameObject;
				knight4.transform.position += new Vector3(53.4f, 0, 0);

				SpriteRenderer tint4 = knight4.GetComponent<SpriteRenderer>();
				Color colRef4 = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player 4").GetComponent<CharacterSelect>().playerColour;
				tint4.color = new Color(colRef4.r, colRef4.g, colRef4.b, 1.0f);

				view.Add(knight4);

				// Add the character to the player roster, first integer is the player number followed by the character index choice - determines what get instantiate in GameManager.
				CreateRoster(4, 2);
				break;

			default:
				break;
		}
	}

	private void DestroyKnight(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				view.Remove(knight1);
				RemoveFromRoster(1, 2);
				DestroyObject(knight1);
				break;

			case "Player2":
				view.Remove(knight2);
				RemoveFromRoster(2, 2);
				DestroyObject(knight2);
				break;

			case "Player3":
				view.Remove(knight3);
				RemoveFromRoster(3, 2);
				DestroyObject(knight3);
				break;

			case "Player4":
				view.Remove(knight4);
				RemoveFromRoster(4, 2);
				DestroyObject(knight4);
				break;

			default:
				break;
		}
	}

	private void DeactivateKnight(string playerID)
	{
		switch (playerID)
		{
			case "Player1":
				view.Remove(knight1);
				knight1.SetActive(false);
				break;

			case "Player2":
				view.Remove(knight2);
				knight2.SetActive(false);
				break;

			case "Player3":
				view.Remove(knight3);
				knight3.SetActive(false);
				break;

			case "Player4":
				view.Remove(knight4);
				knight4.SetActive(false);
				break;

			default:
				break;
		}
	}

	private void CreateRoster(int id, int choice)
	{
		for (int i = 1; i <= 4; i++)
		{
			if (i == id)
			{
				allPlayerInfo.playersInfo[i - 1].playerColor = GameObject.Find("UI/CharacterSelectMenu/Player Selection/Player " + i).GetComponent<CharacterSelect>().playerColour;

				allPlayerInfo.playersInfo[i - 1].playerIndex = id;
				allPlayerInfo.playersInfo[i - 1].characterChoice = choice;
				allPlayerInfo.playersInfo[i - 1].isActive = true;
			}
		}
	}

	private void RemoveFromRoster(int id, int choice)
	{
		for (int i = 1; i <= 4; i++)
		{
			if (i == id)
			{
				allPlayerInfo.playersInfo[i - 1].playerColor = new Color(1f, 1f, 1f, 1f);

				allPlayerInfo.playersInfo[i - 1].playerIndex = 0;
				allPlayerInfo.playersInfo[i - 1].characterChoice = 0;
				allPlayerInfo.playersInfo[i - 1].isActive = false;
			}
		}
	}

}
