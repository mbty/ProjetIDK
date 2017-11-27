using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
	public Wall wall;

	void OnBecameVisible () {
		wall.inc_counter();
	}

	void OnBecameInvisible () {
		wall.dec_counter();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
