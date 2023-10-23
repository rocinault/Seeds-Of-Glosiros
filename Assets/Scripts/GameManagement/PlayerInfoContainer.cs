using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfoContainer
{
	[HideInInspector] public Color playerColor;

	[HideInInspector] public int playerIndex;
	[HideInInspector] public int characterChoice;
	[HideInInspector] public bool isActive;
}
