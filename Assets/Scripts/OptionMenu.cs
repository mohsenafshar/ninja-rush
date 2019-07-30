using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class OptionMenu : MonoBehaviour {

	public AudioManager audioManager;
	public AudioSource bgMusic;
	public GameObject fxSound;

	public Toggle musicToggle;
	public Slider musicSlider;
	public Toggle fxToggle;
	public Slider fxSlider;

	private List<AudioSource> audioSources;

	void Awake(){
//		gameObject.SetActive (false);

		audioSources = new List<AudioSource> ();
		
		for (int i = 0; i < fxSound.transform.childCount; i++) {
			audioSources.Add(fxSound.transform.GetChild (i).GetComponent<AudioSource>());
		}

		
		PreLoadVolumes ();
	}

	void Start(){
	}


	public void GetVolumeMusic(float value){
		bgMusic.volume = value;
		PlayerPrefs.SetFloat ("musicVolume", value);
	}


	public void GetVolumeFx(float value){
		foreach (AudioSource aSource in audioSources) {
			aSource.volume = value;
		}
		PlayerPrefs.SetFloat ("fxVolume", value);
	}

	
	
	
	public void SetMusicActivity(bool isOn){
		if(isOn)
			bgMusic.volume = PlayerPrefs.GetFloat("musicVolume");
		else
			bgMusic.volume = 0;
	}


	public void SetFxActivity(bool isOn){
		if (isOn)
		foreach (AudioSource aSource in audioSources) {
			aSource.volume = PlayerPrefs.GetFloat("fxVolume");
		}
		else
		foreach (AudioSource aSource in audioSources) {
			aSource.volume = 0;
		}
	}

	public void PreLoadVolumes ()
	{
		if (PlayerPrefs.HasKey ("musicVolume"))
			bgMusic.volume = PlayerPrefs.GetFloat ("musicVolume");
		if (PlayerPrefs.HasKey ("fxVolume"))
			foreach (AudioSource aSource in audioSources) {
				aSource.volume = PlayerPrefs.GetFloat ("fxVolume");
			}
		fxSlider.value = audioSources [0].volume;
		musicSlider.value = bgMusic.volume;
	}
}
