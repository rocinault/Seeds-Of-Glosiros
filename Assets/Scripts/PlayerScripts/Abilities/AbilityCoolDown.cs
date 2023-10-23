using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerController
{
	public class AbilityCoolDown : MonoBehaviour
	{
		private Melee m_Melee;
		private GravityBomb m_GravityBomb;

		private float m_NextReadyTime;
		private float m_CoolDownTimeLeft;

		private bool m_CoolDownComplete;

		private void Start()
		{
			m_Melee = GetComponent<Melee>();

			if (this.gameObject.name == "Ess(Clone)")
			{
				m_GravityBomb = GetComponent<GravityBomb>();
			}
		}

		public void AttackInput(bool attack)
		{
			if (attack)
				m_Melee.MeleeAttack(attack);
		}

		public void AbilityInput(bool ability1)
		{
			if (ability1 && m_CoolDownComplete && m_GravityBomb.enabled)
			{
				// If the multi shot has been unlocked use that function.
				if (m_GravityBomb.unlockMultiShot)
					m_GravityBomb.ActivateMultiShot();

				// Otherwise use the regular shoot.
				else
					m_GravityBomb.Shoot();
			}

		}

		private void Update()
		{
			m_CoolDownComplete = (Time.time > m_NextReadyTime);

			if (m_CoolDownComplete)
			{
				m_CoolDownTimeLeft = 0;
			}
			else
			{
				m_CoolDownTimeLeft -= Time.deltaTime;
			}
		}

		public void SetCoolDown(float coolDown)
		{
			m_NextReadyTime = coolDown + Time.time;
			m_CoolDownTimeLeft = coolDown;
		}
	
	}
}

/*
 				// If air drop is unlcoked and the player is not grounded perform an air drop attack.
				if (m_GravityBomb.unlockAirDrop && !m_GravityBomb.m_Grounded)
					m_GravityBomb.ActivateAirDrop();


 */
