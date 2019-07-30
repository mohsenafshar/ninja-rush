using UnityEngine;
using System.Collections; 

public class FollowPlayer : MonoBehaviour {

	public Transform playerTrans;

	[Range(3,10000)]
	private float xPos;
	[Range(0,10000)]
	private float yPos;

	private Vector3 newPos;

	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
//		xPos = playerTrans.position.x;
//		yPos = playerTrans.position.y;
		newPos = playerTrans.position;
		newPos.z = -10;

		if (newPos.x < 3) {
			newPos.x = 3;
		}

		if (newPos.y < 0) {
			newPos.y = 0;
		}

		transform.position = newPos;
	}
}
