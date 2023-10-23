using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine;

public class CursorInputModule :  MonoBehaviour
{
	[HideInInspector] public string playerID;
	[HideInInspector] public int inputID;

	private float deadZone = 1;
	private float horizontalX;
	private float moveX;

	private int selectedCharacterNum;

	private void Start()
	{
		selectedCharacterNum = 1;

		SetCursorX();

	}

	private void Update()
	{
		// Check for movement input.
		float horizontalInput = Input.GetAxisRaw("Horizontal" + inputID);
		if (horizontalInput < deadZone && horizontalInput > -deadZone)
			horizontalX = 0;
		else
			horizontalX = horizontalInput;

		if (horizontalX == -1 && transform.localPosition.x != -moveX)
		{
			MovePos("Left");
		}

		if (horizontalX == 1 && transform.localPosition.x != moveX)
		{
			MovePos("Right");
		}

	}

	private void MovePos(string direction)
	{
		switch (direction)
		{
			case "Left":
				transform.localPosition = new Vector2(-moveX, transform.localPosition.y);
				selectedCharacterNum = 1;
				break;

			case "Right":
				transform.localPosition = new Vector2(moveX, transform.localPosition.y);
				selectedCharacterNum = 2;
				break;

			default:
				break;
		}
	}

	public int CharacterSelected()
	{
		return selectedCharacterNum;
	}

	private void SetCursorX()
	{
		switch (playerID)
		{
			case "Player1":
				moveX = 220f;
				break;

			case "Player2":
				moveX = 180f;
				break;

			case "Player3":
				moveX = 180f;
				break;

			case "Player4":
				moveX = 220f;
				break;

			default:
				break;
		}
	}
}

