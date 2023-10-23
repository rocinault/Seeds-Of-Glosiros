using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Summary:
 * This class is responsible for checking player inputs, it then passes on the inputs into the
 * main Character class that then determines what action needs to be performed.
 */

namespace PlayerController
{
	public class Player2DUserControl : MonoBehaviour
	{
		private Character m_Character;                                      // Reference to the current Character.
		private AbilityCoolDown m_AbilityCoolDown;

		private float m_Horizontal;                                         // Horizontal movement for the character.
		private float deadZone = 1.0f;

		private bool m_Jump;                                                // Jump input for the character.
		private bool m_Attack;                                              // Attack input.
		private bool m_Ability1;                                            // Ability input.
		private bool m_Dash;                                                // Dash Input.

        private void Awake()
		{
			m_Character = GetComponent<Character>();

			m_AbilityCoolDown = GetComponent<AbilityCoolDown>();
		}


		private void Update()
		{
			if (!m_Jump)
				m_Jump = Input.GetButtonDown("Jump" + m_Character.m_PlayerNumber);				// Reads for jump inputs.

			if (!m_Attack)
				m_Attack = Input.GetButtonDown("Attack" + m_Character.m_PlayerNumber);           // Reads for attack inputs.			

			if (!m_Dash)
				m_Dash = Input.GetButtonDown("Dash" + m_Character.m_PlayerNumber);

			if (!m_Ability1)
				m_Ability1 = Input.GetButtonDown("Ability" + m_Character.m_PlayerNumber);                // Reads for button held down for shot inputs.

			float horizontalInput = Input.GetAxisRaw("Horizontal" + m_Character.m_PlayerNumber);
			if (horizontalInput < deadZone && horizontalInput > -deadZone)
				m_Horizontal = 0;
			else
				m_Horizontal = horizontalInput;
		}


		private void FixedUpdate()
		{
			
			m_Character.UpdateState(m_Horizontal, m_Jump, m_Dash);


			m_AbilityCoolDown.AttackInput(m_Attack);

			if (this.gameObject.name == "Ess(Clone)")
			{
				m_AbilityCoolDown.AbilityInput(m_Ability1);
			}

			ResetInputs();
		}


		private void ResetInputs()
		{
			m_Jump     = false;
			m_Attack   = false;
			m_Ability1 = false;
			m_Dash     = false;
		}
	}
}
