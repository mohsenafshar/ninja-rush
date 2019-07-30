using UnityEngine;
using System.Collections;

public class TiledBackground : MonoBehaviour {

	public float textureSize = 32;
	public float extender = 1;
	public bool scaleHorizontal = true;
	public bool scaleVertical = true;

	void Start () {

		var newWidth = !scaleHorizontal ? 1 : Mathf.Ceil(Screen.width / (perfectPixelsCamera.scale * textureSize ));
		var newheight = !scaleVertical ? 1 : Mathf.Ceil(Screen.height / (perfectPixelsCamera.scale * textureSize ));

		transform.localScale = new Vector3 (newWidth * textureSize, newheight * textureSize / extender, 1);

		GetComponent<Renderer> ().material.mainTextureScale = new Vector3 (newWidth, newheight, 1);
	}
}
