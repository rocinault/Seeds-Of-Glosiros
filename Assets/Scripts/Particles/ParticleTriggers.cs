using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]

public class ParticleTriggers : MonoBehaviour {

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
        int exitParticleNumber = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

        //iterate through the particles that entered the trigger
        for(int i = 0; i < enterParticleNumber; i++)
        {
            ParticleSystem.Particle p = enter[i];
            //changes particles color
            p.startColor = new Color32(255, 0, 0, 255);

            enter[i] = p;
        }

        //iterate through the particles that exited the trigger
        for (int i = 0; i < exitParticleNumber; i++)
        {
            ParticleSystem.Particle p = exit[i];
            //changes particles color
            p.startColor = new Color32(0, 0, 255, 255);

            exit[i] = p;
        }

        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);
        ps.SetTriggerParticles(ParticleSystemTriggerEventType.Exit, exit);

    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
