using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondsOffset : MonoBehaviour {

	private GameObject[] diamonds;
	public Vector2 colliderOffset = Vector2.zero;

	void Awake(){
		diamonds = gameObject.GetComponent<Spawner> ().prefabs;

	}

	void Start(){

		var i = diamonds [0].GetComponent<CircleCollider2D> ().bounds.size.x;

		foreach (GameObject diamond in diamonds) {
			diamond.transform.Translate(i, 0f, 0f);
			i *= 2;
		}

	}

	
//	public void Restart(){
//		var renderer = GetComponent<SpriteRenderer> ();
//		renderer.sprite = diamonds [Random.Range(0, diamonds.Length)];
//		
//		var size = renderer.bounds.size;
//		var collider = GetComponent<BoxCollider2D>();
//		collider.size = size;
//		
//		collider.offset = new Vector2 (-colliderOffset.x , collider.size.y / 2 - colliderOffset.y);
//		
//	}
//	
//	public void Shutdown(){
//		
//	}
}
