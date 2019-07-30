using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour {

	public ChoosePotionSound choosePotionSound;
	
	public AudioSource jumpSound;
	public AudioSource coinSound;
	public AudioSource diamondSound;
	public AudioSource heartSound;
	public AudioSource potionSound;
	public AudioSource LooseSound;

	public AudioSource bgMusic;

	public void AudioPlayer(string tag){
		switch (tag) {
		case "jump":
			jumpSound.Play();
			break;
		case "coin":
			coinSound.Play();
			break;
		case "diamond":
			diamondSound.Play();
			break;
		case "heart":
			heartSound.Play();
			break;
		case "loose":
			LooseSound.Play();
			break;
		default :
			choosePotionSound.SetClip(tag);
			potionSound.Play();
			break;

		}
	}

	public void GetVolume(float volume){
		bgMusic.volume = volume;
	}
}
