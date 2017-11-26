using UnityEngine;

public class Vilain : MonoBehaviour {
	public Rigidbody rb;
	public int health 	= 40;
	public int crippled = 5;

	public void TakeDamage (int dmg, float impact, Vector3 dir)
	{
		rb.AddForce (impact * dir * 5f, ForceMode.Impulse);
		health -= dmg;

		if (health <= crippled) {
			rb.constraints = RigidbodyConstraints.None;

			if (health <= 0)
				Die ();
		}
	}

	void Die () {
		Destroy (gameObject);
	}
}
