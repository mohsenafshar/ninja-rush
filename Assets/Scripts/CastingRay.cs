using UnityEngine;
using System.Collections;

public class CastingRay : MonoBehaviour {
	
	public SpriteRenderer renderer;

	private Vector3 origin = Vector3.zero;
	private Animator animator;
	
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetBool("idle", true);
		origin = transform.position;
		origin.x -= GetComponent<CircleCollider2D> ().radius + 0.001f;

		Debug.DrawRay (origin, Vector2.left * 10, Color.green);

	}
	
	void Update () {
		origin = transform.position;
		RaycastHit2D hit = Physics2D.Raycast (origin, Vector2.left, 30f);
		if (hit.collider != null) {
			Debug.DrawLine(origin, hit.point);
			if (hit.collider.tag == "Player") {
				float distance = Mathf.Abs( hit.point.x - origin.x);
				if(distance < 6f ){
					ChangeAlpha(0.5f);
					hit.rigidbody.AddForce(new Vector2(-150000,0));
					Invoke("ChangeAlpha", 0.5f);
				}
				animator.SetBool("idle", false);
			}else
				animator.SetBool("idle", true);
		}

		//Debug.DrawRay (origin, Vector2.left * 10, Color.green);
		
	} 
	
	void ChangeAlpha(float alpha){

		animator.SetBool("idle", true);
		
		Color color = renderer.color;
		color.a = alpha;
		renderer.color = color;
	}

	void ChangeAlpha(){

		animator.SetBool("idle", true);
		
		Color color = renderer.color;
		color.a = 1;
		renderer.color = color;
	}
}
