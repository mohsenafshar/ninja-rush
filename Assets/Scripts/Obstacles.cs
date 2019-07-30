using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Obstacles : MonoBehaviour, IRecycle {

	public Sprite[] sprites;
	public Vector2 colliderOffset = Vector2.zero;

	public void Restart(){
		var renderer = GetComponent<SpriteRenderer> ();
		renderer.sprite = sprites [Random.Range(0, sprites.Length)];

		var size = renderer.bounds.size;
		var collider = GetComponent<BoxCollider2D>();
		collider.size = size;

		collider.offset = new Vector2 (-colliderOffset.x , collider.size.y / 2 - colliderOffset.y);

	}
	
	public void Shutdown(){
		
	}
}
