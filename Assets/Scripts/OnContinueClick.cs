using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class OnContinueClick : MonoBehaviour, IPointerDownHandler{

	public GameManager gameManager;

	#region IPointerDownHandler implementation
	public void OnPointerDown(PointerEventData eventData){
		gameManager.OnClickResetGame ();
	}
	#endregion

}