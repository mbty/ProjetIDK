using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRight : MonoBehaviour {
	public Camera cam;

	private const float FOV = 60;



	/*
	void OnRenderImage(RenderTexture src, RenderTexture dest) {
		GUI.DrawTexture
	}
	*/


	void Start() {
//		float near 		= cam.nearClipPlane;
//		float far 		= cam.farClipPlane;
//		float right		= 0;//Screen.width/Screen.height  * 0.3f;
//		float left 		= -2f * Screen.width/Screen.height * 0.3f;
//		float top 		=  0.17f;
//		float bottom 	= -0.17f;
//
//		float x = 2.0F * near / (right - left);
//		float y = 2.0F * near / (top - bottom);
//		float a = (right + left) / (right - left);
//		float b = (top + bottom) / (top - bottom);
//		float c = -(far + near) / (far - near);
//		float d = -(2.0F * far * near) / (far - near);
//		float e = -1.0F;
//
//		Matrix4x4 m = new Matrix4x4();
//		m[0, 0] = x;
//		m[0, 1] = 0;
//		m[0, 2] = a;
//		m[0, 3] = 0;
//		m[1, 0] = 0;
//		m[1, 1] = y;
//		m[1, 2] = b;
//		m[1, 3] = 0;
//		m[2, 0] = 0;
//		m[2, 1] = 0;
//		m[2, 2] = c;
//		m[2, 3] = d;
//		m[3, 0] = 0;
//		m[3, 1] = 0;
//		m[3, 2] = e;
//		m[3, 3] = 0;
//		cam.projectionMatrix = m;
//
//
////		Debug.Log(cam.projectionMatrix.ToString());
////		Matrix4x4 p = cam.projectionMatrix;
////		p [0, 0] = 2.0f * n / ( r - l );//(Mathf.Tan (FOV / 2.0f * Mathf.PI / 180.0f)); //;
//////		p[0, 2] = ( r + l ) / ( r - l );
////		p [1, 1] = 1.0f / (Mathf.Tan (FOV / 2.0f * Mathf.PI / 180.0f)); //2.0f * n / ( t - b );
//////		p[1, 2] = ( t + b ) / ( t - b );
//////		p[2, 2] = ( f + n ) / ( n - f );
//////		p[2, 3] = 2.0f * f * n / ( n - f );
//////		p[3, 2] = -1.0f;
////
////		cam.projectionMatrix = p;
////		//cam.projectionMatrix.SetColumn(1, new Vector4(0.0f, 1.0f/(Mathf.Tan(FOV/2.0f * Mathf.PI/180.0f)), 0.0f, 0.0f));
////
////		Debug.Log(cam.projectionMatrix.ToString());


	}
}
