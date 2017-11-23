using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Rigidbody rb;
	public Camera cm;

	// Camera
	// Properties
	private const float MIN_FOV = 15.0f;
	private const float MAX_FOV = 90.0f;

	// State


	//-----------

	// Player
	// Properties
	private const float HEIGHT = 0.5f;

	// Abilities
	private const float SPEED = 5.0f;
	private const float FORWARD_SPEED = 1.0f * SPEED;
	private const float SIDEWARD_SPEED = 0.7f * SPEED;

	private const float BASE_JUMP_FORCE = 3.2f;
	private const float BOOST_JUMP_FORCE = 14.0f;

	private const float BOOST_END = 0.28f;
	private const float BOOST_START = 0.08f;
	private const float JUMP_COOLDOWN = 0.05f;
	private const float JUMP_XY = 0.5f;

	// State
	private float last_jump_start;
	private bool is_jumping;
	private Vector3 jump_direction;

	// Use this for initialization
	void Start () {
		last_jump_start = Time.time - 2 * JUMP_COOLDOWN;
		is_jumping = false;
	}
		
	// Update is called once per frame
	void Update () {
		float input_x = Input.GetAxisRaw("Horizontal");
		float input_z = Input.GetAxisRaw("Vertical");
		bool input_y = Input.GetKey("space");

		if (input_y && !is_jumping && Time.time - last_jump_start > JUMP_COOLDOWN && Physics.Raycast (transform.position, -Vector3.up, 0.15f + HEIGHT)) {
			if (input_x == 1.0f && input_z == 1.0f)
				jump_direction = new Vector3(JUMP_XY * 0.5f, 1.0f, JUMP_XY * 0.5f);
			else
				jump_direction = new Vector3(input_x * JUMP_XY, 1.0f, input_z * JUMP_XY);
			rb.AddForce (jump_direction * BASE_JUMP_FORCE, ForceMode.Impulse);
			is_jumping = true;
			Debug.Log ("start");
			last_jump_start = Time.time;
		} else if (input_y && is_jumping && Time.time - last_jump_start > BOOST_START && Time.time - last_jump_start < BOOST_END) {
			rb.AddForce (jump_direction * BOOST_JUMP_FORCE * Time.deltaTime, ForceMode.Impulse);
		} else if (is_jumping && Time.time - last_jump_start > BOOST_END) {
			is_jumping = false;
			Debug.Log ("end");
			Debug.Log (transform.position.y);

		}

		if (true) {
			Vector3 mvt = new Vector3 (input_x * FORWARD_SPEED * Time.deltaTime, 0, input_z * SIDEWARD_SPEED * Time.deltaTime);
			rb.MovePosition (transform.position + mvt);
		}
	}
		
}
