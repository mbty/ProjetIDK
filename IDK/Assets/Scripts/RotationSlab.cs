using UnityEngine;

public class RotationSlab : MonoBehaviour {
	public const float rotation = 90f;
	
	// Mirrors movement
	void OnTriggerEnter(Collider cd) {
		PlayerMovement pm = cd.gameObject.GetComponent<PlayerMovement>();
		if (pm != null) {
			pm.SIDEWARD_SPEED = -pm.SIDEWARD_SPEED;
		}
	}
}
