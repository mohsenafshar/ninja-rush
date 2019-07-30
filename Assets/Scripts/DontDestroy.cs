using UnityEngine;
using System.Collections;

public class DontDestroy : MonoBehaviour {

	void Awake(){

		if (GameObject.FindGameObjectsWithTag("audio").Length > 1)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);
	}

	void OnLevelWasLoaded(int level){
		if (level > 1)
			Destroy (GameObject.FindWithTag("audio"));
	}
}
