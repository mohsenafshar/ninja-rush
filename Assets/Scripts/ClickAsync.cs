using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ClickAsync : MonoBehaviour {
	
	public Slider loadingBar;
	public GameObject loadingImage;
	
	private AsyncOperation async;
	
	public void ClickToLoadLevelAsync(int level){
		
		loadingImage.SetActive (true);
		Time.timeScale = 1;
		StartCoroutine (LoadLevelWithBar(level));
		
	}
	
	IEnumerator LoadLevelWithBar(int level){

//		float randomTime = Random.Range (0.1f, 0.6f);

//		for(float i = 0f; i < randomTime; i += Time.deltaTime ){
//			loadingBar.value = i;
//			yield return null;
//		}
//
//		yield return new WaitForSeconds(1);

		async = SceneManager.LoadSceneAsync (level);
//		async = Application.LoadLevelAsync (level);
//		if(async.progress > randomTime)
			while (!async.isDone) {
					loadingBar.value = async.progress;
					yield return null;
			}
	}
}
