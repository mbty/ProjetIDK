using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchBlocker : MonoBehaviour {
	public Switch master;

	void OnBecameVisible () {
		master.inc_counter();
	}

	void OnBecameInvisible () {
		master.dec_counter();
	}
}
