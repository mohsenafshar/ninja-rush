using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Jump : MonoBehaviour {

	public float jumpSpeed = 240f;
	public float forwardSpeed = 20f;

	private Rigidbody2D body2d;
	private InputState inputState;
	private AudioManager audioManager;

	void Awake(){
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		body2d = GetComponent<Rigidbody2D> ();
		inputState = GetComponent<InputState> ();
	}

	// Update is called once per frame
	void Update () {
		if (inputState.standing) {
			if(inputState.activeButton){
				body2d.velocity = new Vector2(transform.position.x < 0 ? forwardSpeed : 0 ,jumpSpeed);
			}
		}

		if (body2d.velocity.y == 240)
			audioManager.AudioPlayer ("jump");
	}
}
