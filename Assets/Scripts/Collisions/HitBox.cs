using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
	public class HitBox : MonoBehaviour
	{
		public float m_MeleeForce = 15.0f;
		private Collision2D col;

		private GameObject player;

		private void Awake()
		{
			player = this.gameObject;
		}

		void OnCollisionEnter2D(Collision2D collision)
		{
			col = collision;

			if (col.gameObject.tag == "Player"  && col.gameObject.GetComponent<PlayerController.PlayerMovement>().m_DashCooldown < 1.5f)	// Check to see if the player didn't just dash.
			{
				col.gameObject.GetComponent<PlayerController.PlayerHealth>().TakeDamage(player.transform, m_MeleeForce);
			}
		}

	}
}




