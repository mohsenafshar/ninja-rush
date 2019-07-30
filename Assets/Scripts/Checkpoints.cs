using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour {

	public Transform playerTransform;
	public GameController gameController;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			gameController.LastPos = playerTransform.position;
			print("Check point");
			Destroy(gameObject);
		}

	}
}
