// TODO this file is being modified, not supposed to work.
// TODO dynamic rebinding (easy despite the relatively scary name)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
	/* open contains a list of the objects that should be visible when
	 * the state is open (i.e. when the door is open) and hidden otherwise.
	 * Try to guess what closed contains.
	 */
	public List<GameObject> open;
	public List<GameObject> closed;

	public GameObject player;
	// The teleporter to which this teleporter is currently bounded.
	public Switch friend;

	// obsolete, will be removed (TODO)
	public Vector3 distance;
	public Vector3 inf_bound;
	public Vector3 sup_bound;

	/* Number of objects contained in open and close that are rendered
	 * Allows seamless transition between the two states of the teleporter.
	 */
	public int counter;

	// state
	public bool is_open;
	public bool is_active;

	private int counter_bk;
	private bool ascending = false;

	void Start() {
		Renderer rd;
		Collider co;

		if (is_open) {
			foreach (GameObject obj in closed) {
				rd = obj.GetComponent<Renderer> ();
				rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
				co = obj.GetComponent<Collider> ();
				co.enabled = !co.enabled;
			}
			counter_bk = closed.Count;
		} else {
			foreach (GameObject child in open) {
				rd = child.GetComponent<Renderer> ();
				rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
				co = child.GetComponent<Collider> ();
				co.enabled = false;
			}
			counter_bk = open.Count;
		}

		counter = 0;
	}

	public void inc_counter() {
		if (is_open) {
			++counter;

			if (counter == counter_bk && ascending) {
				ascending = false;
			}
		}
	}

	public void dec_counter() {
		// Breaks for some mysterious reason without the second condition
		if (is_open && counter > 0) {
			--counter;

			if (counter == 0 && !ascending) {
				toggle ();
				ascending = true;
			}
		}
	}

	public void activate() {
		is_open = true;
	}

	void toggle() {
		Renderer rd;
		Collider co;

		// TODO compute distance between current and friend and translate by result
		if (player.transform.position.x > inf_bound.x && player.transform.position.z > inf_bound.z && player.transform.position.x < sup_bound.x && player.transform.position.z < sup_bound.z) {
			is_open = false;
			player.transform.Translate (distance, Space.World);
			friend.activate ();
			return;
		}

		foreach (GameObject child in closed) {
			rd = child.GetComponent<Renderer> ();
			rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, rd.material.color.a);
			co = child.GetComponent<Collider> ();
			co.enabled = !co.enabled;
		}

		foreach (GameObject child in open) {
			rd = child.GetComponent<Renderer> ();
			rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
			co = child.GetComponent<Collider> ();
			co.enabled = !co.enabled;
		}

	}
}
