using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingWorm : MovingObject {

	private Animator _animator;
    public AudioSource audioSlide;


	// Use this for initialization
	protected override void Start () {
		_animator = GetComponent<Animator>();
        _orientation = Orientation.LEFT;
    	base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
			
		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		int horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		int vertical = (int) (Input.GetAxisRaw ("Vertical"));
	
		if (horizontal != 0 || vertical != 0)
		{
			if (horizontal > 0)
			{
				_orientation = Orientation.RIGHT;
			}
			else if (horizontal < 0)
			{
				_orientation = Orientation.LEFT;
			}

			
            _animator.SetInteger("Orientation", (int)_orientation);
		} else {
			rb2D.velocity = Vector2.zero;
		}
	}

    protected override void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            rb2D.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
            audioSlide.Play();
        }
    }


     protected override void OnFinishedMoving()
	{
	}

}
