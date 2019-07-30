using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SetCharacter : MonoBehaviour {

	public List<GameObject> prefabs = new List<GameObject>();

	private GameObject playerPrefab;

	// Use this for initialization
	void Awake () {
		ChangePlayerPrefab ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void ChangePlayerPrefab(){ 
		string characterName = PlayerPrefs.GetString ("Character");

		if ( characterName != null )
			foreach (GameObject prefab in prefabs) {

			if( characterName.Equals(prefab.name) ){
					gameObjectUtill.name = prefab.name;
					gameObject.GetComponent<GameManager> ().playerPrefab = prefab;
					break;
				}

			}

	}


}
