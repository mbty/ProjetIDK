using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {
	// The teleporter to which this teleporter is currently bounded.
	public Teleporter friend;
    public GameObject player;

    public List<GameObject> waiting;

	void OnTriggerEnter(Collider other) {
		if (!waiting.Contains(other.gameObject))
			waiting.Add (other.gameObject);
	}

    void OnTriggerExit(Collider c) {
		if (waiting.Contains(c.gameObject))
			waiting.Remove (c.gameObject);
	}

	void enter(Teleporter t) {
        Vector3 diff = t.transform.position - this.transform.position;
        foreach (GameObject go in waiting) {
			go.transform.Translate (diff, Space.World);
		}
	}

    public bool containsPlayer() {
        return waiting.Contains(player);
    }

	public void teleport () {
		enter (friend);
        friend.enter(this);

        List<GameObject> temp = this.waiting;
		this.waiting = friend.waiting;
		friend.waiting = temp;
	}
}
