using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WonderingWorm : MovingObject {

	private Animator _animator;
    public AudioSource audioSlide;
    public AudioClip GoodBookGrab;


	public int Health;
	// Use this for initialization
	protected override void Start () {
		_animator = GetComponent<Animator>();
        _orientation = Orientation.LEFT;
		Health = 3;
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
            
            if(!audioSlide.isPlaying)
            {
                audioSlide.Play();
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
		switch (other.tag) {
				case "Smut": 
					TakeDamage();
					EatBook(other.gameObject);		
					break;		
				case "PickUp":					    
					EatBook(other.gameObject);				
					break;			
				case "Etiquette":					    
					EatBook(other.gameObject, 5);									
					break;			
		}  
    }

	private void TakeDamage() {
		Health--;

		if (Health <= 0) {
			// game over

		}
	}

	protected void EatBook(GameObject book, int increment = 1) {
		Destroy(book);
		//Add one to the current value of our count variable.
		count = count + increment;
		SetCountText();      
	}

    protected override void OnFinishedMoving()
	{
	}

}
