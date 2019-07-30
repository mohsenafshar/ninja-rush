using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyObjectPool : MonoBehaviour {

	public static MyObjectPool myPool;

	public int projectileCount = 10;
	public GameObject prefab;
	public bool canGrow = true;
	
	private List<GameObject> instances;

	void Awake(){
		myPool = this;
	}

	void Start(){

		instances = new List<GameObject> ();

		for (int i = 0; i < projectileCount; i++) {
			CreateInstances();
		}
	}

	public GameObject GetProjectile(){

		foreach (GameObject obj in instances) {
			if(!obj.activeInHierarchy){
				return obj;
			}
		}
		
		if(canGrow){
			return CreateInstances();
		}

		return null;

	}

	public GameObject CreateInstances(){

		GameObject obj = (GameObject)Instantiate (prefab);
		obj.transform.parent = transform;
		obj.SetActive (false);
		instances.Add (obj);

		return obj;
	}

}
