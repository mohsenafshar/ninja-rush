using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class gameObjectUtill {

	public static float theTime = 0;
	public static int level;
	public static string name;
	 
	public gameObjectUtill(float time){
		theTime = time;
	}

	public static Dictionary<RecycleGameObject ,ObjectPool> pools = new Dictionary<RecycleGameObject, ObjectPool> ();

	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;

		var recycledScript = prefab.GetComponent<RecycleGameObject> ();

		if (recycledScript != null) {
			var pool = GetObjectPool(recycledScript);
			instance = pool.NextObject(pos).gameObject;
		} else {

			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;


		}

		if (name == null)
			if (level == 1) 
				name = "player";
			else if(level == 2)
				name = "Modern Player";
			else 
				name = "player";

		if (!instance.name.Equals (name+"(Clone)") && !instance.name.Equals ("spinning coin long time(Clone)")) {
			float speed = SpeedUp();
//			if (instance.name.Equals ("Zombies(Clone)") || instance.name.Equals ("Foxes(Clone)"))
			if (instance.tag == "runnerEnemy")
				instance.GetComponent<instantVelocity> ().velocity.x = speed - 35;
			else 
				instance.GetComponent<instantVelocity> ().velocity.x = speed;
		}
		return instance;

	}

	public static void Destroy(GameObject gameObject){

		var recycleGameObject = gameObject.GetComponent<RecycleGameObject> ();

		if (recycleGameObject != null) {
			recycleGameObject.Shutdown();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	private static ObjectPool GetObjectPool(RecycleGameObject reference){
		ObjectPool pool = null;

		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else {
			var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool" );
			pool = poolContainer.AddComponent<ObjectPool>();
			pool.prefab = reference;
			pools.Add(reference, pool);
		}

		return pool;
	}

	static float SpeedUp(){

		switch((int)theTime){
		case 0:
			return -95f;
			//break;
		case 20:
			return -110f;
			//break;
		case 40:
			return -130f;
			//break;
		case 60:
			return -140f;
			//break;
		case 80:
			return -150f;
			//break;
		case 100:
			return -180f;
			//break;
		case 120:
			return -200f;
			//break;
		case 150:
			return -220f;
			//break;
		default :
			return -220f;
			//break;
		}

	}

}
