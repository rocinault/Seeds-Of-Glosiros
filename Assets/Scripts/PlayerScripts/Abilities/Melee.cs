using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
	public class Melee : Character
	{
		private HitBox[] m_HitBox;
	
		private void Start()
		{
			m_HitBox = GameObject.Find("Attack_Collider").GetComponentsInChildren<HitBox>();
		}

		public void MeleeAttack(bool attack)
		{
			if (attack && !m_Animator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))       // Check to see if the player isn't currently attacking and input has been pressed.
			{
				if (m_Grounded)
				{
					m_Rigidbody2D.velocity = new Vector2(0.0f, 0.0f);                       // Stop the player from moving if they are grounded.
				}

				m_Animator.SetTrigger("Attack");                                            // Set the animation to attack.
			}
		}

		public void IncreaseMeleeForce(int amount)
		{
			for(int i = 0; i < m_HitBox.Length; i++)
			{
				m_HitBox[i].m_MeleeForce += amount;
			}
		}
	}
}
