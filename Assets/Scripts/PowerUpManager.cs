using UnityEngine;
using System.Collections;

public class PowerUpManager : MonoBehaviour {

	private TimeManager timeManager;

	private Collider2D playerCollider;
	private Rigidbody2D body;

	private float scale;

	[HideInInspector]
	public bool isGrowing = false;
	[HideInInspector]
	public bool isSlowed = false;
	[HideInInspector]
	public bool isInvisible = false;
	[HideInInspector]
	public bool isPoisoned = false;

	void Awake(){
		timeManager = GameObject.FindWithTag ("GameManager").GetComponent<TimeManager> ();

		playerCollider = GetComponent<BoxCollider2D> ();
		body = GetComponent<Rigidbody2D> ();
	}

	private void GetPlayerGameObject(){
		playerCollider = GetComponent<BoxCollider2D> ();
	}

	public IEnumerator ChangeVisibility(){
		isInvisible = true;
		Color temp = GetComponent<SpriteRenderer> ().color;

		float y = playerCollider.bounds.size.y/2;
		float gravity = body.gravityScale;

		float jump = GetComponent<Jump> ().jumpSpeed;
		float forwardSpeed = GetComponent<Jump> ().forwardSpeed;
		GetComponent<Jump> ().jumpSpeed = 0;
		GetComponent<Jump> ().forwardSpeed = 0;

		playerCollider.isTrigger = true;
		body.isKinematic = true;
		body.gravityScale = 0;
		temp.a = 0.5f;
		GetComponent<SpriteRenderer> ().color = temp;

		Transform pTransform = GetComponent<Transform> ();
		Vector3 pos = pTransform.position;

		while (pTransform.position.y > -64 + y) {

			pos.y--;
			GetComponent<Transform> ().position = pos;
			yield return new WaitForSeconds(Time.deltaTime);
		}

		InvokeRepeating ("Blink", 3f, 0.3f);

		yield return new WaitForSeconds (5f);
		CancelInvoke ();

		GetComponent<Jump> ().jumpSpeed = jump;
		GetComponent<Jump> ().forwardSpeed = forwardSpeed;

		playerCollider.isTrigger = false;
		body.isKinematic = false;
		body.gravityScale = gravity;
		temp.a = 1f;
		GetComponent<SpriteRenderer> ().color = temp;

		isInvisible = false;
	}

	private void Blink(){
		Color temp = GetComponent<SpriteRenderer> ().color;

		if (temp.a == 1)
			temp.a = 0.5f;
		else if (temp.a == 0.5f)
			temp.a = 1f;
		else
			print ("error:GameStats/Line:245");

		GetComponent<SpriteRenderer> ().color = temp;

	}

	public IEnumerator ChangeTime(float time){
		isSlowed = true;

		timeManager.ManipulateTime(time, 5f);

		yield return new WaitForSeconds (5f);

		timeManager.ManipulateTime(1f, 5f);

		isSlowed = false;
	}

	public IEnumerator Grow(){
		isGrowing = true;

		scale = transform.localScale.x;
		float tmpScale = scale;

		while (tmpScale < scale*2) {
			transform.localScale = new Vector3 (tmpScale, tmpScale, 1);
			tmpScale++;
			yield return new WaitForSeconds (Time.deltaTime);

		}

		yield return new WaitForSeconds (5f);

		while (tmpScale > scale) {
			transform.localScale = new Vector3 (tmpScale, tmpScale, 1);
			tmpScale--;
			yield return new WaitForSeconds (Time.deltaTime);

		}

		isGrowing = false;

	}

	public IEnumerator Poisoned(){
		isPoisoned = true;

		scale = transform.localScale.x;
		float tmpScale = scale;


		while (tmpScale > scale/2) {
			transform.localScale = new Vector3 (tmpScale, tmpScale, 1);
			tmpScale--;
			yield return new WaitForSeconds (Time.deltaTime);

		}

		yield return new WaitForSeconds (5f);

		while (tmpScale < scale) {
			transform.localScale = new Vector3 (tmpScale, tmpScale, 1);
			tmpScale++;
			yield return new WaitForSeconds (Time.deltaTime);

		}

		isPoisoned = false;
	}
}
