using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityStandardAssets.CrossPlatformInput;

[DisallowMultipleComponent]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class MovementControllerSatanist : NetworkBehaviour {
    Animator anim;
	
	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (!isLocalPlayer)
        {
            gameObject.tag = "Other_Player";
            gameObject.GetComponentInChildren<Camera>().enabled = false;
            gameObject.GetComponentInChildren<AudioListener>().enabled = false;
            return;
        }
        //turning
        //jumping
        //move
        float horInput = 0f;
        float verInput = 0f;
        if(Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            horInput = Input.GetAxis("Horizontal");
            verInput = Input.GetAxis("Vertical");
            setNormalStrike(Input.GetKey(KeyCode.Space));
            setAOEStrike(Input.GetKey(KeyCode.LeftAlt));
        } else
        {
            horInput = CrossPlatformInputManager.GetAxis("Horizontal");
            verInput = CrossPlatformInputManager.GetAxis("Vertical");
            setNormalStrike(CrossPlatformInputManager.GetButton("NormalHit"));
            setAOEStrike(CrossPlatformInputManager.GetButton("SpecialHit"));
        }

        move(verInput);
        turn(horInput);

       
    }

    void move(float verticalInput)
    {
        anim.SetFloat("Forward", verticalInput);
    }

    void turn(float horizontalInput)
    {
        anim.SetFloat("Turning", horizontalInput);
    }

    void setNormalStrike(bool normalStrike)
    {
        anim.SetBool("NormalStrike", normalStrike);
    }

    void setAOEStrike(bool specialStrike)
    {
        anim.SetBool("SpecialStrike", specialStrike);
    }

    public void StopAllMovement()
    {
        move(0f);
        turn(0f);
        setNormalStrike(false);
        setAOEStrike(false);
    }
}
