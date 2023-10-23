using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintTrail : MonoBehaviour {

    public ParticleSystem ps;
    private ParticleSystem.MainModule psmain;

    // Use this for initialization
    void Start () {

        psmain = ps.main;
        Color tint = GetComponent<SpriteRenderer>().material.color;
        psmain.startColor = tint;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetColour(Color PassedColour)
    {
        psmain.startColor = PassedColour;
    }
}
