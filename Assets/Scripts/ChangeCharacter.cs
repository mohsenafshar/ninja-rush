using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class ChangeCharacter : MonoBehaviour {

	public List<GameObject> prefabs = new List<GameObject>();

	private GameObject go;
	private Sprite existSp;

	private int newIndex = 0;
	private GameObject gameManager;

	void Awake () {
		go = GameObject.Find ("Character");
		existSp = go.GetComponent<SpriteRenderer>().sprite;
	}

	void Start () {
		string characterName = PlayerPrefs.GetString ("Character");
		
		if ( characterName != null )
		foreach (GameObject prefab in prefabs) {
			
			if( characterName.Equals(prefab.name) ){
				go.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
				go.GetComponent<Animator>().runtimeAnimatorController = prefab.GetComponent<Animator>().runtimeAnimatorController;
				go.GetComponent<Animator>().SetBool("running", false);
				go.transform.localScale = prefab.transform.localScale;
				break;
			}
			
		}
	}

	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.Escape)) {
			SceneManager.LoadScene (0);
//			Application.LoadLevel(0);
		}

	}

	public void ChangePerson(){

		foreach (GameObject prefab in prefabs) {
			Sprite prefabSprite = prefab.GetComponent<SpriteRenderer>().sprite;

			if( existSp == prefabSprite ){
				newIndex = 0 ;
				int index = prefabs.IndexOf(prefab);

				if( index+1 != prefabs.Count) {
					newIndex = index + 1;
				}else {
					newIndex = 0;
				}

				Sprite sp = prefabs[newIndex].GetComponent<SpriteRenderer>().sprite;
				existSp = sp;

				go.GetComponent<SpriteRenderer>().sprite = sp;
				go.GetComponent<Animator>().runtimeAnimatorController = prefabs[newIndex].GetComponent<Animator>().runtimeAnimatorController;
				go.GetComponent<Animator>().SetBool("running", false);
				go.transform.localScale = prefabs[newIndex].transform.localScale;

				break;
			}
		}
	}

	public void SaveCharacter(){
		PlayerPrefs.SetString("Character", prefabs[newIndex].name);
	}
}
