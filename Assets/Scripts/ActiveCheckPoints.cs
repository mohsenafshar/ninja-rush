using UnityEngine;
using System.Collections;

public class ActiveCheckPoints : MonoBehaviour {

	public Rigidbody2D rb;
	public GameController gameController;
	
	void Awake(){
	}
	
	void Start () {
	}
	
	void OnTriggerExit2D(Collider2D other){
		if (other.attachedRigidbody == rb) {
			gameController.goToLastPos();
		}
	}
}
