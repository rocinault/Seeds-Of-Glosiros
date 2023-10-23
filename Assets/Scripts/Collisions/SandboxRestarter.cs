using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandboxRestarter : MonoBehaviour {

	private bool side = true;
	private float posX;

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);            // velocity is reset.

			if (side)
				posX = UnityEngine.Random.Range(-18, -24);

			else
				posX = UnityEngine.Random.Range(18, 24);

			other.gameObject.transform.position = new Vector2(posX, 0);

			FlipSide();

		}
	}

	private void FlipSide()
	{
		side = !side;
	}
}
