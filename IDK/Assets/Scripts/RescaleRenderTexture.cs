using UnityEngine;

public class RescaleRenderTexture : MonoBehaviour {
	public RenderTexture rt;

	void Start () {
		rt.width  = Screen.width;
		rt.height = Screen.height;
		Cursor.lockState = CursorLockMode.Locked; 
	}
}
