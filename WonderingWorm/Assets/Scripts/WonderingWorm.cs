using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingWorm : MovingObject {

	private Animator _animator;     


	// Use this for initialization
	protected override void Start () {
		_animator = GetComponent<Animator>();		
		_orientation = Orientation.RIGHT;
    	base.Start();
	}
	
	// Update is called once per frame
	void Update () {
		int horizontal = 0;     //Used to store the horizontal move direction.
        int vertical = 0;       //Used to store the vertical move direction.
			
		//Get input from the input manager, round it to an integer and store in horizontal to set x axis move direction
		horizontal = (int) (Input.GetAxisRaw ("Horizontal"));
		
		//Get input from the input manager, round it to an integer and store in vertical to set y axis move direction
		vertical = (int) (Input.GetAxisRaw ("Vertical"));
	
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

			RaycastHit2D hit;
			AttemptMove(horizontal, vertical, out hit);
		}
	}

	 protected override void OnFinishedMoving()
	{
	}

}
