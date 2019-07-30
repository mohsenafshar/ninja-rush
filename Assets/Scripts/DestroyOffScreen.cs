﻿using UnityEngine;
using System.Collections;

public class DestroyOffScreen : MonoBehaviour {

	public delegate void OnDestroy();
	public event OnDestroy DestroyCallback;

	public float offset = 16f;

	private bool offscreen;
	private float offscreenX;
	private Rigidbody2D body2d;

	void Awake(){
			body2d = GetComponent<Rigidbody2D> ();
	}

	// Use this for initialization
	void Start () {
		offscreenX = (Screen.width / perfectPixelsCamera.pixelsToUnits) / 2.0f + offset;
	}
	
	// Update is called once per frame
	void Update () {

		var posX = transform.position.x;
		var dirX = body2d.velocity.x;

		if( Mathf.Abs(posX) > offscreenX ){

			if( dirX < 0 && posX < -offset){
				offscreen = true;
			}else if(dirX > 0 && posX > offset){
				offscreen = true;
			}

		}
		else {
			offscreen = false;
		}

		if (offscreen) {
			OnOutOfBounds();
		}
	}

	void OnOutOfBounds(){
		offscreen = false;
		gameObjectUtill.Destroy (gameObject);

		if (DestroyCallback != null) {
			DestroyCallback();
		}
	}
}
