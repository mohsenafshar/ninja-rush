using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class QuitGame : MonoBehaviour {

	//private GameManager gameManager;

	void Awake(){
		//gameManager = GameObject.FindWithTag ("GameManager").GetComponent<GameManager> ();
	}

	// Use this for initialization
	public void QutiGame () {
		//gameManager.SaveBonusData ();
		Application.Quit ();
	}

	void Update(){
//		if (Input.GetKeyDown (KeyCode.Escape)) {
//			Time.timeScale = 1;
//			SceneManager.LoadScene (0);
//			Application.LoadLevel(0);
//		}
	}
}
