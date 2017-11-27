using UnityEngine;

public class Wall : MonoBehaviour {
	public GameObject door;
	public GameObject player;
	public Wall friend;
	public Vector3 distance;

	public Vector3 inf_bound;
	public Vector3 sup_bound;

	public int counter;

	public bool open_initially;
	public bool active;

	private int counter_bk;
	private bool ascending = false;

	void Start() {
		Renderer rd;
		Collider co;
		counter_bk = 1;

		if (open_initially) {
			rd = gameObject.GetComponent<Renderer> ();
			rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
			co = gameObject.GetComponent<Collider> ();
			co.enabled = !co.enabled;
		}

		foreach (Transform child in door.transform) {
			if (!open_initially) {
				rd = child.GetComponent<Renderer> ();
				rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
				co = child.GetComponent<Collider> ();
				co.enabled = false;
			}
			++counter_bk;
		}

		counter = 0;
	}

	void OnBecameVisible () {
		inc_counter();
	}

	void OnBecameInvisible () {
		dec_counter();
	}

	public void inc_counter() {
		if (active) {
			++counter;

			if (counter == counter_bk && ascending) {
				ascending = false;
			}
		}
	}

	public void dec_counter() {
		// Breaks for some mysterious reason without the second condition
		if (active && counter > 0) {
			--counter;

			if (counter == 0 && !ascending) {
				toggle ();
				ascending = true;
			}
		}
	}

	public void activate() {
		active = true;
	}

	void toggle() {
		Renderer rd;
		Collider co;

		if (player.transform.position.x > inf_bound.x && player.transform.position.z > inf_bound.z && player.transform.position.x < sup_bound.x && player.transform.position.z < sup_bound.z) {
			active = false;
			player.transform.Translate (distance, Space.World);
			friend.activate ();
			return;
		}

		rd = gameObject.GetComponent<Renderer> ();
		rd.material.color = new Color(rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
		co = gameObject.GetComponent<Collider> ();
		co.enabled = !co.enabled;

		foreach (Transform child in door.transform) {
			rd = child.GetComponent<Renderer> ();
			rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
			co = child.GetComponent<Collider> ();
			co.enabled = !co.enabled;
		}

	}
}
