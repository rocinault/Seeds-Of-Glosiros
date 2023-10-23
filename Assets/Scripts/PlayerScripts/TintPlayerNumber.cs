using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TintPlayerNumber : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Find the playernumber gameobject
        GameObject go = transform.Find("PlayerNumber").gameObject;
        //Get the spriterenderer for it
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        //Find the PlayerNumber's spriteoutline and pass it the full strength colour
        go.GetComponent<SpriteOutline>().color = gameObject.GetComponent<SpriteOutline>().color;
        //Tell the playernumber's spriterendered to change the colour to this
        go.GetComponent<SpriteRenderer>().material.color = sr.color;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
