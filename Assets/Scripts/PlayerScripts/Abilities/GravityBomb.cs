using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerController
{
	public class GravityBomb : Character
	{
		[SerializeField] private GameObject m_GravityBomb;                             // GameObject of the projectile being fired.

		// Projectile upgradables.
		[HideInInspector] public float m_ProjectileVelocity = 12.0f;
		[HideInInspector] public float cooldownShoot = 6.0f;
		[HideInInspector] public int maxNumberShots = 2;


		private Transform m_FirePoint;
		private Transform airDropPoint;

		private bool coolDownComplete = false;
		private float coolDownTimeLeft;
		private float nextShot;

		[HideInInspector] public bool unlockMultiShot = false;
		[HideInInspector] public bool unlockAirDrop = false;

		private bool activateMultiShot = false;
		private int numberOfShots = 0;
		private float timeTillNextshot = 0.25f;


		private bool activateAirDrop = false;
		private int numAirDrops = 0;
		private int maxNumAirDrops = 3;
		private float timeTillNextAirDrop = 0.15f;

		private void Start()
		{
			m_FirePoint = transform.Find("FirePoint");
			airDropPoint = transform.Find("AirDrop");
		}

		private void Update()
		{
			// If the multi shot becomes active and the ability has been unlocked.
			if (activateMultiShot && unlockMultiShot)
			{
				// Check if the cooldown is complete.
				coolDownComplete = (Time.time > nextShot);

				// If the cooldown is ready.
				if (coolDownComplete)
				{
					// Check if the number of shots doesn't exceed the allowed amount.
					if (numberOfShots < maxNumberShots)
					{
						// Fire off projectile.
						MultiShot();
					}
					else
					{
						// Otherwise disable the multishot.
						activateMultiShot = false;

						// Set the ability cooldown.
						SetCooldown(cooldownShoot);

						// Reset the number of shots that can be fired.
						numberOfShots = 0;
					}
				}
				else
				{
					// However if the cooldown isn't ready keep subtracting.
					coolDownTimeLeft -= Time.time;
				}
			}


			// If the player has activated the airdrop ability and unlocked it.
			if (activateAirDrop && unlockAirDrop)
			{
				// Check if the player just dashed and are not grounded.
				if(m_PlayerMovement.m_DashCooldown > 1.8f && !m_Grounded)
				{
					// Check if the cooldown is complete.
					coolDownComplete = (Time.time > nextShot);

					// If the cooldown is completed.
					if (coolDownComplete)
					{
						// Check if the number of allowed air drops is less than the max.
						if (numAirDrops < maxNumAirDrops)
						{
							// If all is true then perform an air drop.
							AirDrop();
						}
						else
						{
							// Otherwise disable the airdrop.
							//activateAirDrop = false;

							// Trigger the abilities cooldown.
							SetCooldown(cooldownShoot);

							// Reset the number of shots fired.
							numAirDrops = 0;
						}
					}
					else
					{
						// However if the cooldown isn't ready keep subtracting.
						coolDownTimeLeft -= Time.time;
					}
				}
			}
		}




		public void Shoot()
		{
			if (transform.localScale.x > 0)
			{
				GameObject projectile = Instantiate(m_GravityBomb, m_FirePoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
				projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(m_ProjectileVelocity * 2, 0.0f);
			}
			else if (transform.localScale.x < 0)
			{
				GameObject projectile = Instantiate(m_GravityBomb, m_FirePoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
				projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-m_ProjectileVelocity * 2, 0.0f);
			}

			if(!activateMultiShot)
				SetCooldown(cooldownShoot);
		}


		public void IncreaseProjectileVelocity(float amount)
		{
			m_ProjectileVelocity += amount;
		}


		public void DecreaseCoolDownShoot(float amount)
		{
			cooldownShoot -= amount;
		}


		public void ActivateMultiShot()
		{
			activateMultiShot = true;
		}


		private void MultiShot()
		{
			if (transform.localScale.x > 0)
			{
				GameObject projectile = Instantiate(m_GravityBomb, m_FirePoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
				projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(m_ProjectileVelocity * 2f, 0.0f);
			}
			else if (transform.localScale.x < 0)
			{
				GameObject projectile = Instantiate(m_GravityBomb, m_FirePoint.position, Quaternion.Euler(new Vector3(0f, 0f, 0f))) as GameObject;
				projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(-m_ProjectileVelocity * 2f, 0.0f);
			}

			// Keep track of the number of shots fired.
			numberOfShots++;

			// Set the multi shot timer.
			SetMultiShotTimer();
		}


		private void SetMultiShotTimer()
		{
			nextShot = timeTillNextshot + Time.time;
			coolDownTimeLeft = timeTillNextshot;
		}

		public void UnlockMultiShot()
		{
			unlockMultiShot = true;
		}


		public void IncreaseProjectileAmount(int amount)
		{
			maxNumberShots += amount;
		}




		private void AirDrop()
		{
			GameObject projectile = Instantiate(m_GravityBomb, airDropPoint.position, Quaternion.Euler(new Vector3(0f, 0f, 270f))) as GameObject;
			projectile.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, -m_ProjectileVelocity * 1.2f);

			// Set the timer to manage the time between projectile shots.
			SetAirDropTimer();
		}


		private void SetAirDropTimer()
		{
			nextShot = timeTillNextAirDrop + Time.time;
			coolDownTimeLeft = timeTillNextAirDrop;
		}


		public void UnclockAirDrop()
		{
			unlockAirDrop = true;
			activateAirDrop = true;
		}


		private void SetCooldown(float amount)
		{
			m_AbilityCoolDown.SetCoolDown(amount);
		}
	}
}



