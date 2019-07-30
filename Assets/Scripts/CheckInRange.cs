using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInRange : MonoBehaviour {
	public float forceValue = 50000;
	public float forceTime = 0.5f;
	public static bool isForced;
	public SpriteRenderer renderer;

	public static bool isInRange = false;

	private Animator animator;
	private EnemyMovement enemyMovemonet;

	private Collision2D playerCollision;
	private Rigidbody2D playerRb;
	private GameObject player;

	void Awake(){
		animator = GetComponent<Animator> ();
		enemyMovemonet = GetComponent<EnemyMovement> ();
	}

	// Use this for initialization
	void Start () {
		isForced = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate(){
		
		if (isForced) {
			Vector2 direction = playerCollision.contacts [0].point - new Vector2 (transform.position.x, transform.position.y);
			direction = -direction.normalized;
			direction = Vector2.left * enemyMovemonet.dir * -1;
			playerRb.AddForce(direction * forceValue);
			if(!isInRange)
				Invoke("ToggleForce", forceTime);
			Debug.Log ("forced");
		}
	}

	void ToggleForce(){
		isForced = false;
		Debug.Log ("unForced");
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			isInRange = true;
			animator.SetBool("idle", false);
			Debug.Log ("enter");

			if (enemyMovemonet.dir == 1 && other.transform.position.x <= transform.position.x) {
				enemyMovemonet.dir = -1;
				enemyMovemonet.flipFace ();
			} else if (enemyMovemonet.dir == -1 && other.transform.position.x >= transform.position.x) {
				enemyMovemonet.dir = 1;
				enemyMovemonet.flipFace ();
			}

		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			isInRange = false;
			Debug.Log ("exit");
			animator.SetBool("idle", true);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		

		if (other.gameObject.tag == "Player") {
			playerCollision = other;
			player = other.gameObject;
			playerRb = other.gameObject.GetComponent<Rigidbody2D>();
			isForced = true;

			ChangeAlpha(0.5f);
			Invoke("ChangeAlpha", 0.5f);
		}
	}

	void OnCollisionExit2D(Collision2D other){

	}

	void ChangeAlpha(float alpha){
		Color color = renderer.color;
		color.a = alpha;
		renderer.color = color;
	}

	void ChangeAlpha(){
		Color color = renderer.color;
		color.a = 1;
		renderer.color = color;
	}
}
