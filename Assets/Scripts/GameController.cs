using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject player;

	private Vector3 lastPos;

	void Awake(){
		lastPos = player.transform.position;
	}

	public Vector3 LastPos{
		get{ return lastPos;}
		set{ lastPos = value;}	
	}

	public void goToLastPos(){
		player.transform.position = lastPos;
	}
}
