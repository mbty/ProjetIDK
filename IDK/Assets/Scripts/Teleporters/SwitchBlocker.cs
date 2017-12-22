using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlocker : MonoBehaviour {
	public Switch wall;

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
