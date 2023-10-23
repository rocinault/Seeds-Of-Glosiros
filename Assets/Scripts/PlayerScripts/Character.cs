using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Summary:
 * This class is responsible for managing the player actions, all the player actions have their own scripts that
 * inherit from this class, therefore any GetComponents (i.e. RigidBody2D) should be called here and labelled as protected.
 */

namespace PlayerController
{
	public class Character : MonoBehaviour
	{
		[HideInInspector] public int m_PlayerNumber;											// For determing the player.
		[HideInInspector] public int m_PlayerLives;                                             // Lives of the player before they loose the round.
		[HideInInspector] public int m_SkillPoints;
		[HideInInspector] public GameObject m_PlayerNumberSprite;

		[HideInInspector] protected PlayerMovement m_PlayerMovement;							// Reference to the player movement script.
		private PlayerHealth m_PlayerHealth;                                                    // Reference to the player health script.

        [HideInInspector] protected LayerMask m_Environment;									// A mask determining what is ground to the character

		[HideInInspector] protected Transform m_GroundCheck;									// A position marking where to check if the player is grounded.
		const float m_GroundedRadius = 0.2f;                                                    // Radius of the overlap circle to determine if grounded
		protected bool m_Grounded;																// Whether or not the player is grounded.

		private Transform m_WallCheck;															// A position marking where to check for walls.
		const float m_WallRadius = 0.6f;                                                        // Radius of the overlap, large enough to poke out at sides but not register ground.
		protected bool m_WallJump = false;														// Whether or not the player is on a wall.
		private Collider2D m_WallCollision;

		protected Animator m_Animator;															// Reference to the player's animator component.
		protected Rigidbody2D m_Rigidbody2D;													// Reference to the player's Rigidbody2D.
		protected SpriteRenderer m_SpriteRenderer;                                              // Reference to the player's SpriteRenderer.
		protected AbilityCoolDown m_AbilityCoolDown;

		private void Awake()																	// Setting up all the references for the character.
		{
			m_PlayerNumberSprite = transform.Find("PlayerNumber").gameObject;

			m_PlayerMovement = GetComponent<PlayerMovement>();
			m_PlayerHealth = GetComponent<PlayerHealth>();

			m_AbilityCoolDown = GetComponent<AbilityCoolDown>();

			m_Animator = GetComponent<Animator>();
			m_Rigidbody2D = GetComponent<Rigidbody2D>();
			m_SpriteRenderer = GetComponent<SpriteRenderer>();

			m_Environment.value = LayerMask.GetMask("Environment");
			m_GroundCheck = transform.Find("GroundCheck");
			m_WallCheck = transform.Find("WallCheck");
		}


		private void Update()
		{
			if (m_PlayerMovement.m_DashCooldown > 0)
				m_PlayerMovement.DashTimer();                                                   // Resets the dash timer.

			if (m_PlayerMovement.m_DashCooldown < 1.8f && m_PlayerMovement.m_DashCooldown > 0)  // Waits a short amount of time.
			{
				
				m_SpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);                     // Before return the player's alpha to one.
			}
		}


		private void FixedUpdate()
		{
			m_Grounded = false;
			m_WallJump = false;

			Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, m_GroundedRadius, m_Environment);
			for (int i = 0; i < colliders.Length; i++)
			{
				if (colliders[i].gameObject != gameObject)
					m_Grounded = true;
			}

			if (!m_Grounded)
			{
				m_WallCollision = Physics2D.OverlapCircle(m_WallCheck.position, m_WallRadius, m_Environment);

				if (m_WallCollision)
						m_WallJump = true;
			}

			if (m_PlayerMovement.m_DashCooldown > 1.8f)
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);								// Have the dash fly horizontally.
			
			m_Animator.SetBool("Ground", m_Grounded);																// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground.

			m_Animator.SetFloat("VelocityY", m_Rigidbody2D.velocity.y);												// Set the vertical animation.
		}


		public void UpdateState(float velocityX, bool jump, bool dash)										
		{
			if (m_PlayerMovement.m_DashCooldown < 1.8f)																// If the player just performed a dash don't move them.
			{
				if (Time.time > m_PlayerHealth.m_LastHitTime + (m_PlayerHealth.m_RepeatDamagePeriod / 2))			// If the player was recently hit then don't let them move.
				{
					m_PlayerMovement.Move(velocityX);																// Runs the player movement function.
				}
			}

			if (jump)
				m_PlayerMovement.Jump(jump);                                                                        // Runs the player Jump function.;

			if (dash && m_PlayerMovement.m_DashCooldown == 0)
				m_PlayerMovement.Dash(dash);

			m_Animator.SetFloat("VelocityX", Mathf.Abs(velocityX));													// Reset the animator so the character doesn't appear to be running when the input isn't pressed.
		}


		public void LooseLife()
		{
			m_PlayerLives -= 1;
		}

		public void PlayDeathAnim()
		{
			m_Animator.SetTrigger("Death");
		}
	}
}