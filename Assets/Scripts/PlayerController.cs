using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Vector2 speed = Vector2.zero;
	public bool facingRight;
//	public static bool isForced;

	private Rigidbody2D rb;
	private Animator animator;
	private LayerMask ground;

	private bool isJumping;
	private bool isFalling;
	
	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == ground) {
			animator.SetBool ("jump", false);
			rb.velocity = new Vector2(rb.velocity.x , 0);
		}
	}

	void Awake (){
		rb = GetComponent<Rigidbody2D> ();
		animator = GetComponent<Animator> ();
		ground = LayerMask.NameToLayer ("Ground");
	}

	void Start () {
		facingRight = true;
//		isForced = false;
	}

	void Update () {

		//Jumping And Falling
		if (Input.GetKeyDown (KeyCode.W) && !isJumping ) {
			animator.SetBool ("jump", true);
			Jumping();
		}
		
		if (rb.velocity.y == 0) {
			animator.SetBool ("jump", false);
			isJumping = false;
		}
		
		
		//Check Whethere or not falling
		Falling ();

	}

	void FixedUpdate(){

		//X-Speed - Switch Between Running and Idle
		float move = Input.GetAxis ("Horizontal");
		rb.velocity = new Vector2 (move * speed.x, rb.velocity.y);
		animator.SetFloat ("vSpeed", Mathf.Abs (move * speed.x));


		//Switch Between Left And Right Facing
		if (move < 0 && facingRight)
			Flip ();
		else if(move > 0 && !facingRight)
			Flip ();
	
	}//END OF FIXEDUPDATE METHOD

//	void ToggleForce(){
//		isForced = false;
//		Debug.Log ("unForced");
//	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 ls = transform.localScale;
		ls.x *= -1;
		transform.localScale = ls;
	}
	
	void Jumping(){
		isJumping = true;

		if( rb.velocity.y == 0)
			rb.velocity = new Vector2(0f, speed.y);
	}

	void Falling(){
		if (rb.velocity.y < 0)
			animator.SetBool ("fall", true);
		else 
			animator.SetBool ("fall", false);
	}
}
