using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickListener : MonoBehaviour , IPointerDownHandler, IPointerUpHandler{

	#region IPointerDownHandler implementation
	public void OnPointerDown(PointerEventData eventData){
		print ("OnPointerDown");
	}
	#endregion

	#region IPointerUpHandler implementation
	public void OnPointerUp(PointerEventData eventData){
		print ("OnPointerUp");
	}
	#endregion

//	void Update () {
//		if (Input.GetMouseButtonDown (0))
//			print ("GetMouseButtonDOwn");
//	}

//	void OnMouseDown(){
//		print ("MouseDown");
//	}
}
