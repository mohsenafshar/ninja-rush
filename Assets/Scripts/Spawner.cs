using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	public GameObject[] prefabs;
	public float delay = 2.0f;
	public bool active = true;
	public Vector2 range = Vector2.zero;

	void Start () {
		StartCoroutine (GenerateEnemy());
	}

	IEnumerator GenerateEnemy(){

		delay = Random.Range (range.x, range.y);
		yield return new WaitForSeconds(delay);

		if (active) {
			var newTransform = transform;
			Vector3 pos = new Vector3(newTransform.position.x, newTransform.position.y +5, newTransform.position.z);

			if(prefabs[0].name.Equals("spinning coin long time") ){


					for(int i = 0 ; i < 12 ; i++){

						if( i%2 == 0){
							newTransform.position = pos;
							gameObjectUtill.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);
						}else if( i%2 == 1){
							transform.Translate(0, -5, 0);
							gameObjectUtill.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);
						}

//						if(Time.timeScale != 1){
//							if(i%2 == 0)
//								transform.Translate(0, -5, 0);
//							break;
//						}

						yield return new WaitForSeconds(0.2f);

					}

	
			}else 
				gameObjectUtill.Instantiate(prefabs[Random.Range(0, prefabs.Length)], newTransform.position);
		
		
		}

		StartCoroutine (GenerateEnemy());

	}
}
