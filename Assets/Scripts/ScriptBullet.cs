using UnityEngine;
using System.Collections;

public class ScriptBullet : MonoBehaviour {

	public float absoluteSpeed = 100;

	private float speed;
	private bool facingRight;
	private Transform pTransform;

	private SpriteRenderer renderer;

	void Awake(){
		pTransform = GameObject.FindWithTag ("Player").GetComponent<Transform> ();
		renderer = GameObject.FindWithTag ("Player").GetComponent<SpriteRenderer> ();
	}

	void Update(){
		transform.Translate (speed * Time.deltaTime, 0, 0);
	}

	void OnEnable(){
		if (renderer.flipX)
			speed = -absoluteSpeed;
		else
			speed = absoluteSpeed;



		Invoke ("Destroy", 3f);
	}

	void OnDisable(){
		CancelInvoke ();
	}

	void Destroy(){
		gameObject.SetActive (false);
	}
}
