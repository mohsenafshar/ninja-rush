using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour {

	public GameObject loadingImage;
	
	public void LoadScene(int level){
		loadingImage.SetActive (true);
		StartCoroutine(Example(level));
	}

	IEnumerator Example(int level) {
		yield return new WaitForSeconds(10);

		SceneManager.LoadScene (level);
//		Application.LoadLevel (level);
	}

}
