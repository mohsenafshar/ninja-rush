using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

	public float movementSpeed = 10;
	public int dir = -1;

	private Vector3 initPos;

	private Animator animator;

	void Awake(){
		animator = gameObject.GetComponent<Animator> ();
	}


	// Use this for initialization
	void Start () {
		initPos = transform.position;
		animator.SetBool("idle", true);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (transform.position.x <= initPos.x - 100 && !CheckInRange.isInRange) {
			dir = 1;
			flipFace ();
		} else if (transform.position.x >= initPos.x + 100 && !CheckInRange.isInRange) {
			dir = -1;
			flipFace ();
		}

		if (!animator.GetBool ("die")) {
			transform.Translate ((dir) * Vector3.right * movementSpeed * Time.deltaTime);
		}
	}

	public void flipFace(){
		Vector3 scale = transform.localScale;
		scale.x = -1 * dir;
		transform.localScale = scale;
	}
}
