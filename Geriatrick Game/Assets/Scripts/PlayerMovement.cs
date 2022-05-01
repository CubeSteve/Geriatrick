using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator anim;
    float horizontalMove = 0f;
    public float runSpeed = 40f;
    bool jump = false;
    bool isRat = false;
    public bool ceilingAbove = false;
	public AudioClip jerryFootsteps;
	
	void AudioManagement ()
	{
		if (anim.GetCurrentAnimatorStateInfo (0).IsName("Player_Run"))
		{
			if (!GetComponent<AudioSource> ().isPlaying)
			{
				GetComponent<AudioSource> ().pitch = 1.0f;
				GetComponent<AudioSource> ().Play ();
			}
		}
		else
		{
			GetComponent<AudioSource> ().Stop ();
		}
	}
	

    private void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
		AudioManagement();

        anim.SetFloat("Speed", Mathf.Abs(horizontalMove));

        if(Input.GetButtonDown("Jump"))
        {
            jump = true;
            anim.SetBool("IsJumping", true);
        }

        if(Input.GetKeyDown("r") && !isRat)
        {
            anim.SetBool("IsRat", true);
            isRat = true;
            controller.setUpRat(true);
        }
        else if(Input.GetKeyDown("r") && isRat && !ceilingAbove)
        {
            anim.SetBool("IsRat", false);
            isRat = false;
            controller.setUpRat(false);
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        jump = false;
    }

    public void OnLanding()
    {
        anim.SetBool("IsJumping", false);
    }
}
