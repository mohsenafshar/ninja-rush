using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpGroundCollision : MonoBehaviour {

	private Collider2D coll;

	// Use this for initialization
	void Start () {
		coll = gameObject.GetComponent<EdgeCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(){
		Debug.Log ("enter");
		coll.isTrigger = true;
	}

	void OnTriggerExit2D(){
		Debug.Log ("exit");
		coll.isTrigger = false;
	}
}
