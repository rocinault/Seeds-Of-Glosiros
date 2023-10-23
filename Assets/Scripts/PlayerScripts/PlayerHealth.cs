using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
	public class PlayerHealth : Character
	{
		[HideInInspector] public float m_LastHitTime;														// The time at which the player was last hit.

		public float m_RepeatDamagePeriod = 1.0f;                                                           // How frequently the player can be damaged.

		private Transform origin;
		private float force;
		private float duration;

		public void TakeDamage(Transform origin, float force)
		{
			if (Time.time > m_LastHitTime + m_RepeatDamagePeriod)                                           // Check to see if the last hit exceeds the time of the last hit plus the time between hits.
			{
				Vector3 damageVector = new Vector3();

				damageVector.x = transform.position.x - origin.position.x;

				damageVector.Normalize();

				damageVector.y = Vector3.up.y * 0.5f;


				m_Rigidbody2D.AddForce(damageVector * force, ForceMode2D.Impulse);                      // Add a damage force to the player.
				m_Animator.SetTrigger("Recoil");

				m_LastHitTime = Time.time;                                                              // Reset the last hit time.
			}
		}
	}
}
