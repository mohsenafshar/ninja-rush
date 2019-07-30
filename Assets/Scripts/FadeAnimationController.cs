using UnityEngine;
using System.Collections;

public class FadeAnimationController : MonoBehaviour {

	public Animator animator;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)) {
			animator.SetTrigger("fade");
		}

		if (Input.GetMouseButtonUp(0)) {
			animator.SetTrigger("fade");
		}

	}
}
