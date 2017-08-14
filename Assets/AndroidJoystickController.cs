using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AndroidJoystickController : MonoBehaviour
{
	[Range (0, 100)]
	public float moveSpeed;
	Animator anim;
	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate ()
	{
		float horizontalInput = CrossPlatformInputManager.GetAxis ("Horizontal");
		float verticalInput = CrossPlatformInputManager.GetAxis ("Vertical");
		Vector2 axisInputs = new Vector2 (horizontalInput, verticalInput) * moveSpeed;
		if (Mathf.Abs (axisInputs.x) > 0.01f || Mathf.Abs (axisInputs.y) > 0.01f)
			Walk (axisInputs);

		bool hit = CrossPlatformInputManager.GetButtonDown ("NormalHit");
		if (hit)
			NormalStrike ();

        bool aoeHit = CrossPlatformInputManager.GetButtonDown("AOEHit");
        if (aoeHit)
            AOEStrike();
    }

    private void AOEStrike()
    {
        Debug.LogWarning("AOE ANIMATION MISSING");
    }

    void Walk (Vector2 directions)
	{
		GetComponent<Rigidbody> ().AddForce (new Vector3 (directions.x, 0f, directions.y));
        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Run"))
		    anim.Play ("Run");
	}

	void NormalStrike ()
	{
		anim.Play ("GSSlash");
	}
}
