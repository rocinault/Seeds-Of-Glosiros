using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{
	private GameManager manager;
	private WaitForSeconds deathAnim = new WaitForSeconds(2);
	private GameObject obj;
	private bool side = true;
	private float posX;

	private void Awake()
	{
		manager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	private void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.tag == "Player")
		{
			obj = other.gameObject;

			if (other.gameObject.GetComponent<PlayerController.Character>().m_PlayerLives <= 1)             // Check if players life count is less than or equal to one (if it checked zero the player would have one extra life).
			{
				PlayDeathAnim();

				other.gameObject.SetActive(false);
				manager.PlayerIsDead(other.gameObject.GetComponent<PlayerController.Character>().m_PlayerNumber);
			}
			else
			{
				other.gameObject.GetComponent<PlayerController.Character>().LooseLife();                    // Player looses a life.
				other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);            // velocity is reset.
				if (side)
					posX = UnityEngine.Random.Range(-20, -30);

				else
					posX = UnityEngine.Random.Range(12, 18);

				other.gameObject.transform.position = new Vector2(posX, 15);                                    // Set that for the players position.

				FlipSide();                                       
			}
		}
	}

	private IEnumerator PlayDeathAnim()
	{
		obj.GetComponent<PlayerController.Character>().PlayDeathAnim();

		yield return deathAnim;
	}

	private void FlipSide()
	{
		side = !side;
	}
}
