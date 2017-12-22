/* This file script allows the creation of a render texture
 * whose resolution depends of the dimensions of the window.
 */

using UnityEngine;

public class RescaleRenderTexture : MonoBehaviour {
	public RenderTexture rt;

	void Start () {
		rt.width  = Screen.width;
		rt.height = Screen.height;
		Cursor.lockState = CursorLockMode.Locked; 
	}
}
