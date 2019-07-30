using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PauseGame : MonoBehaviour {

	public GameObject pauseMenu;
	public GameObject optionsMenu;
	public GameManager gameManager;

	private float timeScale;

	void Awake(){
		pauseMenu.SetActive (false);
	}



	private void DisplayMenu(){
		if (gameManager.isStarted && Time.timeScale != 1 && Time.timeScale != 0)
			return;

		if (!gameManager.isStarted && Time.timeScale != 0)
			return;

		Time.timeScale = 0;
		pauseMenu.SetActive (true);
	}

	public void DisAppearMenu(){
		pauseMenu.SetActive (false);
		Time.timeScale = timeScale;
	}



	public void DisplayOptions(){
		DisAppearMenu ();
		Time.timeScale = 0;
		optionsMenu.SetActive (true);
	}
	
	public void DisAppearOptions(){
		optionsMenu.SetActive (false);
		DisplayMenu ();

	}


	public void GetTime(){
		timeScale = Time.timeScale;
		DisplayMenu ();
	}
}
