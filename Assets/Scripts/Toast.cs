using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Toast : MonoBehaviour {

	public AnimationClip animClip;
	private Animator animator;

	void Awake(){
		animator = GetComponent<Animator> ();
	}

	public void ToastMessage(){
		animator.SetTrigger ("fade");
	}
}
