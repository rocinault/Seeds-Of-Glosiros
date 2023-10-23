using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject explosionObject;
    public ParticleSystem particleEffect;

	private Rigidbody2D m_Rigidbody2D;

    private float aliveTime = 0.0f;
    private float timeToDie = 4.0f;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	void Update()
    {
        aliveTime = aliveTime + Time.deltaTime;
        if (aliveTime > timeToDie)
        {
            Destroy(this.gameObject);
        }

    }

	private void FixedUpdate()
	{
		m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D col)
    {
        GameObject explosion = Instantiate(explosionObject) as GameObject;
        ParticleSystem explosionParticles = Instantiate(particleEffect) as ParticleSystem;
        explosion.transform.position = gameObject.transform.position;
        explosionParticles.transform.position = gameObject.transform.position;
        explosionParticles.transform.Translate(new Vector3(0.0f, 0.0f, -7.0f));
        
        Destroy(this.gameObject);
    }


}
