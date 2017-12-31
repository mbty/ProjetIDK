using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementMultiScreen : MonoBehaviour {
	public Rigidbody rb;
	public Camera cm;

	// CONSTANTS
	private const float SHORT_INPUT		= 0.15f;
	// Run
	private const float SPEED			= 9.0f;
	private const float FORWARD_SPEED	= 1.0f * SPEED;
	private const float SIDEWARD_SPEED	= 0.9f * SPEED;
	// Jump
	private const float BASE_JUMP_FORCE	= 6.0f;
	private const float JUMP_COOLDOWN	= 0.2f;
	// Camera
	private const float X_SENSITIVITY	= 1.5f;
	private const float Y_SENSITIVITY	= 1.5f;
	private const float MIN_Y			= 280.0f;
	private const float MAX_Y			= 80.0f;
	// Miscellaneous
	enum Direction 	{None, Forward, Backward, Left, Right};
	// Gun (the interpolation functions for [loading]/[force applied in terms of impact] distance are harcoded)
	private const float GUN_COOLDOWN	= 0.1f;
	private const float GUN_MAX_FORCE	= 25.0f;
	private const float GUN_MIN_FORCE	= 5.0f;
	private const float GUN_LOAD_TIME	= 1.0f;
	private const float RAYCAST_RANGE	= 7.0f;

	// STATE
	// jump
	private float last_jump_start;
	// physics
	private float xz_friction			= 1.0f; // 0 max, 1 min
	// state
	private bool is_grounded			= false;
	// gun
	private float gun_start;			// set la première fois?


    public int mult_x;
    public int mult_y;

    void Start () {
		// Make sure no action will be blocked by a countdown on launch (may not be necessary)
		last_jump_start		= Time.time - 2 * JUMP_COOLDOWN;
	}
	
	void Update () {
		UpdateCamera();
		UpdatePosition();
	}

	// This function and the next one determine whether or not the player is grounded
	void OnCollisionStay (Collision collisionInfo) {
		foreach (ContactPoint c in collisionInfo.contacts) {
			if (Vector3.up == c.normal) {
				is_grounded = true;
				break;
			}
		}
	}
		
	void OnCollisionExit () {
		is_grounded = false;
	}
		
	void UpdatePosition () {
		float	input_x	 = Input.GetAxisRaw("Horizontal");
		bool	input_y	 = Input.GetKey("space");
		float	input_z	 = Input.GetAxisRaw("Vertical");

		// Jump
		if (input_y && (Time.time - last_jump_start > JUMP_COOLDOWN) && is_grounded) {
			xz_friction = 0.95f;
			rb.AddForce (transform.up * BASE_JUMP_FORCE + input_z * Vector3.forward * BASE_JUMP_FORCE * 0.3f + input_x * Vector3.right * BASE_JUMP_FORCE * 0.3f, ForceMode.Impulse);
			last_jump_start = Time.time;
		}

		// Movement
		Vector3 mvt;
		if (is_grounded)
			mvt = mult_y * input_z * Vector3.forward * FORWARD_SPEED * Time.deltaTime + mult_x * input_x * Vector3.right * SIDEWARD_SPEED * Time.deltaTime;
		else
			mvt = mult_y * input_z * Vector3.forward * FORWARD_SPEED * Time.deltaTime * 0.8f + mult_x * input_x * Vector3.right * SIDEWARD_SPEED * Time.deltaTime * 0.8f;
		rb.MovePosition (transform.position + mvt);
	}

	void UpdateCamera () {
		float input_x = Input.GetAxis("Mouse X");
		float input_y = Input.GetAxis("Mouse Y");

		rb.MoveRotation(Quaternion.Euler(new Vector3 (0, input_x * X_SENSITIVITY, 0)) * transform.rotation);

		float y_movement = input_y * Y_SENSITIVITY	;
		float y_position = cm.transform.rotation.eulerAngles.x - y_movement;

		if (y_position > MAX_Y && y_position < MIN_Y) {
			if (y_movement > 0 && y_position != MIN_Y)
				y_movement = y_position - MIN_Y;
			else if (y_movement < 0 && y_position != MAX_Y)
				y_movement = y_position - MAX_Y;
			else
				y_movement = 0.0f;
			
		}
		cm.transform.eulerAngles = cm.transform.eulerAngles - new Vector3(y_movement, 0, 0);
	}

	void FixedUpdate () {
		// Manual friction
		int mult_x = (Mathf.Abs(rb.velocity.x) > 5) ? 1 : 0;
		int mult_z = (Mathf.Abs(rb.velocity.z) > 5) ? 1 : 0;
		rb.velocity = new Vector3(xz_friction * rb.velocity.x * mult_x, 1.0f * rb.velocity.y, xz_friction * rb.velocity.z * mult_z);
	}

    public void ChangeMult(int x, int y)
    {
        mult_x = x;
        mult_y = y;
    }
}