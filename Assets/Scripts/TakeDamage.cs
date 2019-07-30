using UnityEngine;
using System.Collections;

public class TakeDamage : MonoBehaviour {

	public int hitToDie = 3;
	public AnimationClip animClip;

	public Transform healthBar;

	private int hitCount;
	private float healthLength;
	//private Transform healthBar;
	private Animator animator;

	void Start(){
		animator = GetComponent<Animator>();
		//healthBar = GameObject.Find ("EnemyHp").GetComponent<Transform> ();
		hitCount = hitToDie;
	}
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "projectile" && healthBar.localScale.x > 0) {
			other.gameObject.SetActive(false);

			Vector3 newHealth = healthBar.localScale;
			newHealth.x = newHealth.x - newHealth.x / hitCount;
			healthBar.localScale = newHealth;

			hitCount--;
			if(hitCount <= 0){
				healthBar.localScale = Vector3.zero;
				animator.SetBool("die", true);
				Invoke("SleepEnemy", animClip.length);
			}
//				
		}
	}

	void SleepEnemy(){
		Destroy(gameObject);
	}
}
