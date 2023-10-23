using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxLava : MonoBehaviour {

	private bool side = true;
	private float posX;

	private void OnParticleCollision(GameObject other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<PlayerController.Character>().LooseLife();                    // Player looses a life.
			other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);            // velocity is reset.
			if (side)
				posX = UnityEngine.Random.Range(-18, -24);

			else
				posX = UnityEngine.Random.Range(18, 24);

			other.gameObject.transform.position = new Vector2(posX, 0);                                    // Set that for the players position.

			FlipSide();


		}
	}

	private void FlipSide()
	{
		side = !side;
	}
}
