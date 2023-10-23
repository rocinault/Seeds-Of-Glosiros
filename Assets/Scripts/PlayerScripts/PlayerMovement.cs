using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Summary:
 * This class is responsible for managing the player movement, it focuses on the horizontal movement, jumping and flipping the 
 * character to face the right way, movement speed and jump force is also determined by this class.
 */

namespace PlayerController
{
	public class PlayerMovement : Character
	{
		[SerializeField] private float m_MaxSpeed = 12.0f;												// The fastest the player can travel in the x axis.
		[SerializeField] private float m_JumpForce = 12.0f;												// Amount of force added when the player jumps.
		[SerializeField] private float m_DashForce = 10.0f;

		private bool m_FacingRight = true;																// For determining which way the player is currently facing.
		private bool m_DoubleJump = false;                                                              // For allowing the player to peform a double jump.

		[HideInInspector] public float m_DashCooldown = 0.0f;

		public void Move(float move)
		{
			if (!m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
			{
				m_Animator.SetFloat("VelocityX", Mathf.Abs(move));                                      // The Speed animator parameter is set to the absolute value of the horizontal input.

				m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);

				if (move > 0 && !m_FacingRight)                                                             // If the input is moving the player right and the player is facing left...
				{
																											
					SpriteRenderer sprite = m_PlayerNumberSprite.GetComponent<SpriteRenderer>();			// Get a reference to players number sprite.
					sprite.flipX = false;																	// Flip it before then flipping the player.

					Flip();                                                                                 // Flip the player.
				}
				else if (move < 0 && m_FacingRight)                                                         // Otherwise if the input is moving the player left and the player is facing right...
				{
					SpriteRenderer sprite = m_PlayerNumberSprite.GetComponent<SpriteRenderer>();			// Get a reference to players number sprite.
					sprite.flipX = true;                                                                    // Flip it before then flipping the player.

					Flip();                                                                                 // Flip the player.
				}
			}
		}


		public void Jump(bool jump)
		{
			if (m_Grounded && jump && m_Animator.GetBool("Ground"))                                     // Checks if the player is grounded and the jump input has been pressed.
			{
				m_Grounded = false;
				m_Animator.SetBool("Ground", false);
				m_Rigidbody2D.AddForce(new Vector2(0.0f, m_JumpForce), ForceMode2D.Impulse);            // Adds a vertical force to the player, uses Impulse to account for Rigidbody2D mass.
				m_DoubleJump = true;
			}

			else if (jump && m_DoubleJump && !m_WallJump)												// Checks for jump input, doublejump to be true and for the player to not currently be on a wall.
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);                   // Resets the velocity of the character before performing another jump.
				m_Rigidbody2D.AddForce(new Vector2(0.0f, m_JumpForce), ForceMode2D.Impulse);
				m_DoubleJump = false;																	// Reset the double jump to false.
			}

			else if(jump && m_WallJump)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0.0f);                   // Resets the velocity of the character before performing another jump.

				if(m_FacingRight)
					m_Rigidbody2D.AddForce(new Vector2(-m_JumpForce / 2, m_JumpForce), ForceMode2D.Impulse);

				else if(!m_FacingRight)
					m_Rigidbody2D.AddForce(new Vector2(m_JumpForce / 2, m_JumpForce), ForceMode2D.Impulse);

				m_DoubleJump = true;																	// Allow the player to perform another jump after the intial takeoff.
			}
		}


		public void Dash(bool dash)
		{
			if (dash)
			{
				m_DashCooldown = 2.0f;                                                                  // Set the cooldown until another dash can be performed.

				if (m_FacingRight)
					m_Rigidbody2D.AddForce(new Vector2(m_DashForce, 0.0f), ForceMode2D.Impulse);		// If the player is facing right, dash right.

				else if (!m_FacingRight)
					m_Rigidbody2D.AddForce(new Vector2(-m_DashForce, 0.0f), ForceMode2D.Impulse);		// Otherwise dash in the opposite direction.

				m_SpriteRenderer.color = new Color(1.0f, 1.0f, 1.0f, 0.2f);								// Make the sprite transparent for added affect.
			}
		}


		public void DashTimer()
		{
			if (m_DashCooldown > 0.1)
				m_DashCooldown -= 0.1f * Time.deltaTime;

			else
				m_DashCooldown = 0.0f;
		}


		private void Flip()
		{
			m_FacingRight = !m_FacingRight;                                                             // Switch the way the player is labelled as facing.

			Vector3 theScale = transform.localScale;                                                    // Multiply the player's x local scale by -1.
			theScale.x *= -1;
			transform.localScale = theScale;
		}
	}
}



