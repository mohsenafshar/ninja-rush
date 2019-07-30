using UnityEngine;
using UnityEngine.Analytics;
using System.Collections.Generic;
using System.Collections;


public class FireProjectile : MonoBehaviour {

	public int maxCount = 120;
	public AnimationClip animationClip;

	private int currentCount;
	private Animator animator;

	void Start(){
		animator = GetComponent<Animator> ();
		currentCount = maxCount;
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Return)) {
			Debug.Log ("isDown");
			InvokeShoot ();
		}
	}

	void InvokeShoot (){
		if (animator.GetBool ("jump")) {
			animator.SetBool ("jump", false);
		}
		animator.SetBool ("throw", true);
		
		Invoke ("Shoot", animationClip.length * 0.8f);
	}
	
	void Shoot(){
		Fire ();
		animator.SetBool ("throw", false);
	}

	public void Fire(){

		if (currentCount < 1) {
			print("Out Of Ammo");
			return;
		}


		GameObject projectile = MyObjectPool.myPool.GetProjectile ();

		if (projectile == null)
			return;

		projectile.transform.position = transform.position;

		projectile.SetActive (true);
		currentCount--;
		print (currentCount.ToString());

		//ANALYTICS CALL
//		Analytics.CustomEvent("projectile count", new Dictionary<string, object>
//			{
//				{"projectile value", currentCount},
//				{"coins count", 25}
//			});

	}
}
