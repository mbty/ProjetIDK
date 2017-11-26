using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
	public Rigidbody rb;
	public Camera cm;

	private const float SHORT_INPUT 	= 0.15f;

	// Player
	// + Speed
	private const float SPEED 			= 18.0f;
	private const float FORWARD_SPEED 	= 1.0f * SPEED;
	public float SIDEWARD_SPEED 		= 0.9f * SPEED; // is used to reverse right

	// + Jump
	private const float BASE_JUMP_FORCE	= 8.0f;
	private const float JUMP_COOLDOWN	= 0.2f;

	// + Dash
	private const float DASH_FORCE		= 45.0f;
	private const float DASH_LIMIT	 	= 0.4f;

	// Camera
	private const float X_SENSITIVITY	= 1.5f;
	private const float Y_SENSITIVITY	= 1.5f;
	private const float MIN_Y			= 280.0f;
	private const float MAX_Y			= 80.0f;

	// Fighting
	// I would use classes if I wasn't this lazy
	// Remove?
	private const float RAYCAST_RANGE	= 100.0f;

	private const float PISTOL_COOLDOWN	= 0.8f;
	private const int	PISTOL_DAMAGE	= 5;
	private const float PISTOL_FORCE	= 1.0f;

	private const float HAND_COOLDOWN	= 0.8f;
	private const float HAND_DAMAGE 	= 1.0f;
	private const float HAND_FORCE		= 1.0f;
	private const float HAND_RANGE		= 1.0f;

	private const float SWORD_COOLDOWN	= 0.8f;
	private const float SWORD_DAMAGE 	= 1.0f;
	private const float SWORD_FORCE		= 1.0f;
	private const float SWORD_RANGE		= 1.0f;

	private const float CARBINE_COOLDOWN= 0.8f;
	private const float CARBINE_DAMAGE 	= 1.0f;
	private const float CARBINE_FORCE	= 1.0f;

	private const float RIFLE_COOLDOWN	= 0.8f;
	private const float RIFLE_DAMAGE 	= 1.0f;
	private const float RIFLE_FORCE		= 1.0f;

	private const float GRENADE_COOLDOWN= 0.8f;
	private const float GRENADE_DAMAGE 	= 1.0f;
	private const float GRENADE_FORCE	= 1.0f;
	private const float GRENADE_RADIUS	= 1.0f;

	private const float RPG_COOLDOWN	= 0.8f;
	private const float RPG_DAMAGE		= 1.0f;
	private const float RPG_FORCE		= 1.0f;
	private const float RPG_RADIUS		= 1.0f;

	// Miscellaneous
	enum Direction 	{None, Forward, Backward, Left, Right};
	enum Weapon 	{Hand, Sword, Pistol, Carbine, Rifle, Grenade, RPG};

	// State
	private float last_jump_start;

	private float last_dash;
	private Direction dash_direction	= Direction.Forward;
	private Direction dash_previous		= Direction.None;
	private int dash_state			 	= 0;
	private float previous_down;
	private float current_down;

	private float xz_friction 			= 1.0f; // 0 max, 1 min

	private bool is_grounded 			= false;

	// Fighting
	private float pistol_last;

	// Use this for initialization
	void Start () {
		last_jump_start 	= Time.time - 2 * JUMP_COOLDOWN;
		previous_down 		= Time.time - 2 * SHORT_INPUT;
		current_down 		= Time.time - 2 * SHORT_INPUT;
		pistol_last 		= Time.time - 2 * PISTOL_COOLDOWN;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateCamera();
		UpdatePosition();
		UpdateAction();
	}
		
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
			rb.AddForce (transform.up * BASE_JUMP_FORCE + input_z * transform.forward * BASE_JUMP_FORCE * 0.3f + input_x * transform.right * BASE_JUMP_FORCE * 0.3f, ForceMode.Impulse);
			last_jump_start = Time.time;
		}

		// Dash
		/*
		 * ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
		 * ░░░░Ø░═══|Short input|═══►░1░═══|Short input|═══[Happened in under DASH LIMIT and in the same direction?]═══|Yes|═══►░Dash░░░░
		 * ░░░░░░░░░░░░░░░░░░░░░░░░░░░▲░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
		 * ░░░░░░░░░░░░░░░░░░░░░░░░░░░║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░║░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
		 * ░░░░░░░░░░░░░░░░░░░░░░░░░░░╚════════════════════|No|════════════════════╝░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
		 * ░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░
		 * 
		 * To change the control scheme to "when any two short inputs in under DASH_LIMIT, dash in the direction of the last key pressed",
		 * change the second ifs to else ifs in the single key pressed management blocks.
		 */ 
		if (input_x > 0 && input_z == 0) {
			if (dash_previous == Direction.None) {
				current_down = Time.time;
				dash_previous = Direction.Right;
			}
			if (dash_direction != Direction.Right)
				dash_state = 0;
			dash_direction = dash_previous;
		} else if (input_x < 0 && input_z == 0) {
			if (dash_previous == Direction.None) {
				current_down = Time.time;
				dash_previous = Direction.Left;
			} 
			if (dash_direction != Direction.Left)
				dash_state = 0;
			dash_direction = dash_previous;
		} else if (input_x == 0 && input_z > 0) {
			if (dash_previous == Direction.None) {
				current_down = Time.time;
				dash_previous = Direction.Forward;
			}
			if (dash_direction != Direction.Forward)
				dash_state = 0;
			dash_direction = dash_previous;
		} else if (input_x == 0 && input_z < 0) {
			if (dash_previous == Direction.None) {
				current_down = Time.time;
				dash_previous = Direction.Backward;
			}
			if (dash_direction != Direction.Backward)
				dash_state = 0;
			dash_direction = dash_previous;
		} else if (input_x == 0 && input_z == 0) {
			if (dash_previous != Direction.None) {
				if (Time.time - current_down < SHORT_INPUT) {
					++dash_state;

					if (dash_state == 2) {
						xz_friction = 0.8f;
						if (Time.time - previous_down < DASH_LIMIT) {
							switch (dash_direction) {
								case Direction.Right:
									rb.AddForce (DASH_FORCE * transform.right, ForceMode.Impulse);
									break;
								case Direction.Left:
									rb.AddForce (-DASH_FORCE * transform.right, ForceMode.Impulse);
									break;
								case Direction.Forward:
									rb.AddForce (DASH_FORCE * transform.forward, ForceMode.Impulse);
									break;
								case Direction.Backward:
									rb.AddForce (-DASH_FORCE * transform.forward, ForceMode.Impulse);
									break;
							}
							dash_state = 0;
						} else
							--dash_state;
					}

					previous_down = current_down;
				}
				dash_previous = Direction.None;
			}
		}

		// Movement
		Vector3 mvt;
		if (is_grounded)
			mvt = input_z * transform.forward * FORWARD_SPEED * Time.deltaTime + input_x * transform.right * SIDEWARD_SPEED * Time.deltaTime;
		else
			mvt = input_z * transform.forward * FORWARD_SPEED * Time.deltaTime * 0.8f + input_x * transform.right * SIDEWARD_SPEED * Time.deltaTime * 0.8f;
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

	void UpdateAction() {
		bool input_mouse_1 = Input.GetButtonDown ("Fire1");

		if (input_mouse_1) {
			pistol_last = Time.time;

			RaycastHit hit;
			if (Physics.Raycast (cm.transform.position + 1.5f * cm.transform.forward, cm.transform.forward, out hit, RAYCAST_RANGE)) {
				Debug.Log ("pan");

				//Debug.Ray (hit.transform.name);	

				Vilain vilain = (Vilain)hit.transform.GetComponent<Vilain> ();
				if (vilain != null) {
					Debug.Log ("touché");
					vilain.TakeDamage (PISTOL_DAMAGE, PISTOL_FORCE, cm.transform.forward);
				}
			}
		}

	}

	void FixedUpdate () {
		// Manual friction
		int mult_x = (Mathf.Abs(rb.velocity.x) > 5) ? 1 : 0;
		int mult_z = (Mathf.Abs(rb.velocity.z) > 5) ? 1 : 0;
		rb.velocity = new Vector3(xz_friction * rb.velocity.x * mult_x, 1.0f * rb.velocity.y, xz_friction * rb.velocity.z * mult_z);
	}

}