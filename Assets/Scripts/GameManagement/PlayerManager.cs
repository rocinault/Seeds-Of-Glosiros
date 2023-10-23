using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerManager
{
	[HideInInspector] public Color m_PlayerColour;                                                          // Colour tint for the character.
	[HideInInspector] public Transform m_SpawnPoint;                                                          // Location to spawn the character.


	[HideInInspector] public int m_PlayerNumber;											// Designated number for the player.
	[HideInInspector] public string m_ColouredPlayerText;									// Tints the text that appears on screen.
	[HideInInspector] public GameObject m_Instance;											// Instance of the character that is being managed.
	[HideInInspector] public int m_Wins;                                                    // Number of wins the player has.

	[HideInInspector] public int m_PlayerLevel;
	[HideInInspector] public string name;

	[HideInInspector] public PlayerController.Character m_Character;
	[HideInInspector] public PlayerController.Player2DUserControl m_PlayerControl;

	private PlayerController.GravityBomb m_GravityBomb;
	private PlayerController.GravityRush m_GravityRush;

	public void SetupEss()
	{
		name = "Ess";

		m_Character = m_Instance.GetComponent<PlayerController.Character>();                                                            // Get a reference to the Character script from the player controller.
		m_PlayerControl = m_Instance.GetComponent<PlayerController.Player2DUserControl>();

		m_GravityBomb = m_Instance.GetComponent<PlayerController.GravityBomb>();
		m_GravityRush = m_Instance.GetComponent<PlayerController.GravityRush>();

		m_GravityBomb.enabled = false;
		m_GravityRush.enabled = false;

		m_Character.m_PlayerNumber = m_PlayerNumber;                                                                                    // Set the player number.

		SpriteRenderer tint = m_Instance.GetComponent<SpriteRenderer>();																// Get a reference to the SpriteRenderer.
		tint.material.color = new Color(m_PlayerColour.r, m_PlayerColour.g, m_PlayerColour.b, 1.0f);					                 // Tint its colour and set its alpha to 1 so it isn't transparent.
        m_Character.GetComponent<SpriteOutline>().color = new Color(m_PlayerColour.r, m_PlayerColour.g, m_PlayerColour.b, 1.0f);        //Tint the SpriteOutline to colour the outline properly

        m_ColouredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColour) + ">PLAYER " + m_PlayerNumber + "</color>";    // Tint the lettering to match the player colour.
    }

	public void SetupGrumwald()
	{
		name = "Grumwald";

		m_Character = m_Instance.GetComponent<PlayerController.Character>();                                                            // Get a reference to the Character script from the player controller.
		m_PlayerControl = m_Instance.GetComponent<PlayerController.Player2DUserControl>();

		m_Character.m_PlayerNumber = m_PlayerNumber;                                                                                    // Set the player number.

		SpriteRenderer tint = m_Instance.GetComponent<SpriteRenderer>();                                                                // Get a reference to the SpriteRenderer.
		tint.material.color = new Color(m_PlayerColour.r, m_PlayerColour.g, m_PlayerColour.b, 1.0f);                                    // Tint its colour and set its alpha to 1 so it isn't transparent.

		m_ColouredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(m_PlayerColour) + ">PLAYER " + m_PlayerNumber + "</color>";    // Tint the lettering to match the player colour.

	}

	public void DisableControl()														// Disables player control.
	{
		m_PlayerControl.enabled = false;
	}
	
	
	public void EnableControl()															// Enables player control.
	{
		m_PlayerControl.enabled = true;
	}


	public void Reset()																	// Resets the player for the start of the round.
	{
		m_Instance.transform.position = m_SpawnPoint.position;							// Set the position to be the spawn point.

		m_Character.m_PlayerLives = 3;                                                  // Reset the player's life.

		m_Instance.SetActive(false);													// Make sure that all player's are set to in active.
		m_Instance.SetActive(true);														// Then make them active.
	}


	public void Idle()
	{
		m_Character.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

		m_Character.gameObject.GetComponent<Animator>().SetFloat("VelocityX", 0);
		m_Character.gameObject.GetComponent<Animator>().SetFloat("VelocityY", 0);
	}

	public void DeathAnim()
	{
		m_Character.PlayDeathAnim();
	}

	public void InActivePlayer(bool state)
	{
		m_Instance.SetActive(state);
	}

	public void WhichSkillIsPurchased(string id)
	{
		switch (id)
		{
			// Unlock gravity bomb ability.
			case "UGB":
				m_GravityBomb.enabled = true;
				break;

			// Increase velocity of projectile, tier 1.
			case "IF1":
				float if1 = m_GravityBomb.m_ProjectileVelocity * 0.1f;
				m_GravityBomb.IncreaseProjectileVelocity(if1);
				break;

			// Increase velocity of projectile, tier 2.
			case "IF2":
				float if2 = m_GravityBomb.m_ProjectileVelocity * 0.1f;
				m_GravityBomb.IncreaseProjectileVelocity(if2);
				break;

			// Increase velocity of projectile, tier 3.
			case "IF3":
				float if3 = m_GravityBomb.m_ProjectileVelocity * 0.1f;
				m_GravityBomb.IncreaseProjectileVelocity(if3);
				break;

			// Decrease cooldown of projectile, tier 1.
			case "DC1":
				float dc1 = m_GravityBomb.cooldownShoot * 0.05f;
				m_GravityBomb.DecreaseCoolDownShoot(dc1);
				break;

			// Decrease cooldown of projectile, tier 2.
			case "DC2":
				float dc2 = m_GravityBomb.cooldownShoot * 0.08f;
				m_GravityBomb.DecreaseCoolDownShoot(dc2);
				break;

			// Unlock the multi shot ability.
			case "UMS":
				m_GravityBomb.UnlockMultiShot();
				break;

			// Add an additional projectile to shoot.
			case "UTS":
				m_GravityBomb.IncreaseProjectileAmount(1);
				break;

			// Unlock the air drop ability.
			case "UAD":
				m_GravityBomb.UnclockAirDrop();
				break;

			default:
				break;
		}
	}


	public void UnlockAllAbilities()
	{
		m_GravityBomb.enabled = true;
		m_GravityBomb.UnlockMultiShot();
		m_GravityBomb.UnclockAirDrop();
		m_GravityRush.enabled = true;
	}
}
