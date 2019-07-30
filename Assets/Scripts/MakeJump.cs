using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class MakeJump : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

	private InputState inputState;

	#region IPointerDownHandler implementation
	public void OnPointerDown(PointerEventData eventData){
		if (GameObject.FindWithTag ("Player") != null) {
			inputState = GameObject.FindWithTag ("Player").GetComponent<InputState> ();
			inputState.activeButton = true;
		}
		
//		if (!gameManager.gameStarted && Time.timeScale == 0 && gameStats.HeartsCount > 0) {
//			gameManager.isStarted = true;
//			gameManager.timeManager.ManipulateTime (1f, 1f);
//			gameManager.ResetGame ();
//		}
	}
	#endregion
	
	#region IPointerUpHandler implementation
	public void OnPointerUp(PointerEventData eventData){
		if (GameObject.FindWithTag ("Player") == null)
			return;
		
		inputState = GameObject.FindWithTag("Player").GetComponent<InputState>();
		inputState.activeButton = false;
	}
	#endregion
}
