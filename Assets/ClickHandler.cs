using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler{

	private InputState inputState;

	#region IPointerDownHandler implementation
	public void OnPointerDown(PointerEventData eventData){
		if (GameObject.FindWithTag ("Player") == null)
			return;
		
		inputState = GameObject.FindWithTag("Player").GetComponent<InputState>();
		inputState.activeButton = true;
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
