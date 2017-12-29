/* For lack of a better place to put this information: global illumination was disabled in this level
 * since it seems to break when walls appear dynamically, ruining the illusion.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour {
    public Switch friend;

    /* open contains a list of the objects that should be visible when
	 * the state is open (i.e. when the door is open) and hidden otherwise.
	 * Try to guess what closed contains.
	 */
    public List<GameObject> open;
	public List<GameObject> closed;

	public Teleporter tp;

	/* Number of objects contained in open and close that are rendered
	 * Allows seamless transition between the two states of the teleporter.
	 */
	private int counter;

	// state
	public bool is_open;
	public bool is_active;

	void Start() {
		Renderer rd;
		Collider co;

		List<GameObject> hide = is_open ? closed : open;

		foreach (GameObject obj in hide) {
			rd = obj.GetComponent<Renderer> ();
			rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
			co = obj.GetComponent<Collider> ();
			co.enabled = false;
		}
		counter = 0;
	}

	public void inc_counter() {
        if (is_active)
		    ++counter;
	}

	public void dec_counter() {
		// Breaks for some mysterious reason without the second condition after teleportation
		if (counter > 0 && is_active) {
			--counter;
			if (counter == 0)
				toggle ();
		}
	}

    public void activate()
    {
        is_active = true;
    }

	void toggle() {
		Renderer rd;
		Collider co;

        if (is_open) {
            if (tp.containsPlayer()) {
                tp.teleport();
                is_active = false;
                friend.activate();
                return;
            }
            else
                tp.teleport();
        }

        is_open = !is_open;

        // for all object, invert transparency and tangibility
		foreach (GameObject child in closed) {
			rd = child.GetComponent<Renderer> ();
			rd.material.color = new Color (rd.material.color.r, rd.material.color.g, rd.material.color.b, 1f - rd.material.color.a);
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
