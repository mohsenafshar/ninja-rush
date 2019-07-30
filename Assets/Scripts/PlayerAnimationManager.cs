using UnityEngine;
using System.Collections;

public class PlayerAnimationManager : MonoBehaviour {

	private Animator animator;
	private InputState inputState;

	void Awake () {
		animator = GetComponent<Animator> ();
		inputState = GetComponent<InputState>();
	}
	
	// Update is called once per frame
	void Update () {

		var running = false;
		var jumping = false;
		var idle = false;

		if (animator.parameterCount > 1) {
			if (inputState.absVelX <= 0 ) { //&& inputState.absVelY < inputState.thresholdStanding
				running = true;
			}else if (inputState.absVelX > 0 && inputState.absVelY < inputState.thresholdStanding){
				idle = true;
			}

			if (!running && !idle) {
				jumping = true;
			}
				

			animator.SetBool ("running", running);
			animator.SetBool ("jumping", jumping);
			animator.SetBool ("idle", idle);
		} else {
			running = true;

			if (inputState.absVelX > 0 && inputState.absVelY < inputState.thresholdStanding) {
				running = false;
			}

			animator.SetBool ("running", running);

		}

	}
}
