using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]

public class LavaCollider : MonoBehaviour {


        public ParticleSystem ps;

        public List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
        public List<ParticleSystem.Particle> exit = new List<ParticleSystem.Particle>();

        private void OnEnable()
        {
            ps = GetComponent<ParticleSystem>();
        }

        void OnParticleTrigger()
        {
            int enterParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
            //int exitParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

            //iterate through the particles that entered the trigger
            for (int i = 0; i < enterParticleNumber; i++)
            {
            gameObject.SetActive(false);
            /*
            ParticleSystem.Particle p = enter[i];
                //changes particles color
                p.startColor = new Color32(255, 0, 0, 255);

                enter[i] = p;
                */
        }

        //iterate through the particles that exited the trigger
        /*
        for (int i = 0; i < exitParticleNumber; i++)
            {
                ParticleSystem.Particle p = exit[i];
                //changes particles color
                p.startColor = new Color32(0, 0, 255, 255);

                exit[i] = p;
            }
            */
            ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
           // ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        }


        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
















    /*
    // Use this for initialization
	//void Start () {
		
	//}
	
    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Before if");

        if (other.name == "Lava")
        {
            Debug.Log("in the if");
        }
        else
        {
            Debug.Log("in the else");
        }


  //      Rigidbody2D body = other.GetComponent<Rigidbody>();
     //   if (body)
       // {
       /*
                if (other.gameObject.GetComponent<PlayerController.Character>().m_PlayerLives <= 1)             // Check if players life count is less than or equal to one (if it checked zero the player would have one extra life).
                {
                    other.gameObject.SetActive(false);
                }
                else
                {

                    other.gameObject.GetComponent<PlayerController.Character>().LooseLife();                    // Player looses a life.
                other.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 0.0f);            // velocity is reset.

                Vector2 position = new Vector2(UnityEngine.Random.Range(-12, 12), 0);                       // Pick a random position to spawn at.
                other.gameObject.transform.position = position;
                }
            }
            /
        //}

    } 


	// Update is called once per frame
	//void Update () {
		
	//}
}
*/
  