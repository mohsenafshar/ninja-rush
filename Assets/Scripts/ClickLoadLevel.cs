using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ClickLoadLevel : MonoBehaviour {

	public void ClickToLoadLevel(int level){
		Time.timeScale = 1;
		SceneManager.LoadScene (level);
	}
}
