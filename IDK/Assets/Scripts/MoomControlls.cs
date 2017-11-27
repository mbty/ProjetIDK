using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoomControlls : MonoBehaviour {

	public ParticleSystem _particles;
	public Animator _modelAnimator;

	private const float TURN_SPEED = 1.5f;

	float pitchInput=0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		pitchInput += Input.GetAxis("Mouse Y") * TURN_SPEED;
        pitchInput = Mathf.Clamp(pitchInput,-45,60);

        transform.localEulerAngles = new Vector3 (-pitchInput, 0,0);

        if(Input.GetButtonDown("Fire1"))
		{
			_particles.Play();
			_modelAnimator.SetTrigger("Fire");
		}

		if(Input.GetButtonUp("Fire1"))
		{
			_modelAnimator.SetTrigger("Stop");
			_particles.Stop();
		}
	}
}
