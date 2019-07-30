using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour {

	public int maxCount = 120;
	public int currentCount;
	public bool facingRight;
	public Vector2 speed;

	public AnimationClip animationClip;

	private Rigidbody2D playerRb;
	private SpriteRenderer spriteRenderer;
	private Animator animator;

	private bool isJumping;
	private bool isAttacking;
	private bool isFlyAttacking;
	private bool isGliding;
	private bool isThrowing;

	private float direction;

	public const KeyCode KEY_ATTACK = KeyCode.Mouse0;
	public const KeyCode KEY_GLIDE = KeyCode.Mouse1;
	public const KeyCode KEY_JUMP = KeyCode.Space;
	public const KeyCode KEY_THROW = KeyCode.Return;

	private float shootTime;

	void Awake(){
		playerRb = gameObject.GetComponent<Rigidbody2D> ();
		spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
		animator = gameObject.GetComponent<Animator> ();
	}

	// Use this for initialization
	void Start () {
		facingRight = true;
		isJumping = false;
		isAttacking = false;
		isFlyAttacking = false;
		isGliding = false;
		isThrowing = false;

		currentCount = maxCount;

		shootTime = 0;

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetKeyDown(KEY_JUMP) && playerRb.velocity.y == 0)
			isJumping = true;

		if(Input.GetKeyDown(KEY_ATTACK) && playerRb.velocity.y == 0)
			isAttacking = true;

		if(Input.GetKeyUp(KEY_ATTACK) || playerRb.velocity.y != 0)
			isAttacking = false;

		if(Input.GetKeyDown(KEY_ATTACK) && playerRb.velocity.y != 0)
			isFlyAttacking = true;
		if (Input.GetKeyUp (KEY_ATTACK) || playerRb.velocity.y == 0) {
			isFlyAttacking = false;
		}

		if (Input.GetKeyDown (KEY_GLIDE) && playerRb.velocity.y <= 0) {
			isGliding = true;
			playerRb.gravityScale = 1;
			playerRb.velocity = new Vector2 (playerRb.velocity.x, -10);
		}
		if (Input.GetKeyUp (KEY_GLIDE) || playerRb.velocity.y == 0) {
			isGliding = false;
			playerRb.gravityScale = 30;
		}

		if (Input.GetKeyDown (KEY_THROW) && playerRb.velocity.y == 0) {
			isThrowing = true;
		}
		if (Input.GetKeyUp (KEY_THROW) || playerRb.velocity.y != 0) {
			isThrowing = false;
		}


		if (playerRb.velocity.y == 0) {
			animator.SetBool ("jump", false);
		}

	}

	void FixedUpdate(){
		direction = Input.GetAxis ("Horizontal");
		playerRb.velocity = new Vector2 (direction * speed.x, playerRb.velocity.y);

		if (direction < 0 && facingRight) {
			Flip ();
		} else if (direction > 0 && !facingRight) {
			Flip ();
		}

		if (isJumping) {
			animator.SetBool ("jump", true);
			playerRb.velocity = new Vector2 (0f, speed.y);
			isJumping = false;
		}

		// check player can run or not
		if (playerRb.velocity.x == 0 || playerRb.velocity.y != 0 || isAttacking || isThrowing) {
			animator.SetBool ("run", false);
		} else {
			animator.SetBool ("run", true);
		}

		animator.SetBool ("attack", isAttacking);

		animator.SetBool ("j-attack", isFlyAttacking);

		animator.SetBool ("glide", isGliding);

		animator.SetBool ("throw", isThrowing);

	}

	void Flip(){
		facingRight = !facingRight;
		spriteRenderer.flipX = !spriteRenderer.flipX;
	}

//	void OnMouseDown(){
//		for (int i = 0; i < animator.parameterCount; i++) {
//			animator.GetParameter (i).defaultBool = false;
//			Debug.Log(animator.GetParameter (i).name);
//		}

//		foreach (AnimatorControllerParameter param in animator.parameters) {
//			param.defaultBool = false;
//			animator.SetBool (param.name, false);
//			Debug.Log(param.name);
//		}
//		animator.SetBool ("attack", true);
//	}

	public void InvokeShoot (){
		shootTime = Time.time + (animationClip.length * 1.1f);
		Invoke ("Fire", animationClip.length * 0.9f);
	}

	public void Fire(){

		if (currentCount < 1) {
			print("Out Of Ammo");
			return;
		}
			
		GameObject projectile = MyObjectPool.myPool.GetProjectile ();

		if (projectile == null)
			return;

		projectile.transform.position = new Vector3(transform.position.x + 25, transform.position.y + 20, transform.position.z);

		projectile.SetActive (true);
		currentCount--;
		print (currentCount.ToString());

	}


}
