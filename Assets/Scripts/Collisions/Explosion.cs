using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour {

    [SerializeField] private float m_ExplosivePower = 20.0f;				// Power of the explosion.
	[SerializeField] private float maxRadius = 3.0f;						// Max explosion radius.
	[SerializeField] private float explosionRate = 1.5f;					// Rate of expansion.

	private Collision2D col;												
	private CircleCollider2D explosionRadius;								// Explosion collider.

	private float currentRadius = 0.0f;
	private bool m_StopExpanding = false;                                   // Stops expanding on collision, set to false by default.


	private void Awake()
	{
		explosionRadius = GetComponent<CircleCollider2D>();                 // Reference for the explosion collider. 
	}


	private void FixedUpdate()
    {
        if (currentRadius < maxRadius)										// Keep expanding if radius is less than max amount;
            currentRadius += explosionRate;
        
        else if(m_StopExpanding)											// Stop expanding if the radius has collided with an object.
            Destroy(gameObject);
        
		else																// Otherwise destroy this object.
			Destroy(gameObject);								
		

        explosionRadius.radius = currentRadius;
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
		col = collision;

		if (col.gameObject.tag == "Player" && col.gameObject.GetComponent<PlayerController.PlayerMovement>().m_DashCooldown < 1.5f) // Check to see if the player didn't just dash.
		{
			col.gameObject.GetComponent<PlayerController.PlayerHealth>().TakeDamage(transform, m_ExplosivePower);
		}		
    }
}
