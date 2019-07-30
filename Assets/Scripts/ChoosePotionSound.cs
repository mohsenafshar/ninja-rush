using UnityEngine;
using System.Collections;

public class ChoosePotionSound : MonoBehaviour {

	public AudioClip invis;
	public AudioClip growUp;
	public AudioClip timeWatch;
	public AudioClip poison;

	private AudioSource audioSource;

	void Awake(){
		audioSource = GetComponent<AudioSource> ();
	}

	public void SetClip(string tag){

		switch (tag) {

		case "invis":
			audioSource.clip = invis;
			break;
		case "growUp":
			audioSource.clip = growUp;
			break;
		case "timeWatch":
			audioSource.clip = timeWatch;
			break;
		case "poison":
			audioSource.clip = poison;
			break;

		}
	}
}
