using UnityEngine;
using System.Collections;

public class CollectBonus : MonoBehaviour {

	GameStats gameStats;

	void Awake(){
		gameStats = GameObject.FindWithTag ("GameManager").GetComponent<GameStats> ();
	}
	
	void OnTriggerEnter2D( Collider2D collider){
		if( collider.gameObject.tag == "Player"){
			Pickup ();
		}
	}
	
	void Pickup(){
		gameStats.IncrementBonusCount(gameObject.tag);
		
		var recycledScript = gameObject.GetComponent<RecycleGameObject> ();
		recycledScript.Shutdown ();
	}

}
