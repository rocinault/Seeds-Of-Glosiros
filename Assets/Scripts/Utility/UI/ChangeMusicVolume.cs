using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeMusicVolume : MonoBehaviour {


    public Slider MasterVolume;
//    public AudioSource MasterMusic;

    public Slider MusicVolume;
  public AudioSource MusicMusic;

    public Slider SoundEffectsVolume;
  public AudioSource[] SoundEffectsMusic;

	// Update is called once per frame
	void Update () {
//        MasterMusic.volume = MasterVolume.value;

		MusicMusic.volume = MusicVolume.value;

		for(int i = 0; i < SoundEffectsMusic.Length; i++)
		 SoundEffectsMusic[i].volume = SoundEffectsVolume.value;
    }
}
