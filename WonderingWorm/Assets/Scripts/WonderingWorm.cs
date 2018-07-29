using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonderingWorm : MovingObject {

	private Animator _animator;
    public AudioSource audioSlide;
    public AudioClip GoodBookGrab;
	
	public int PointsToWin;
	public int Health;

	// Use this for initialization
	protected override void Start () {
		_animator = GetComponent<Animator>();
        _orientation = Orientation.LEFT;
		Health = 3;
		PointsToWin = 100;
		_pauseObject = false;
    	base.Start();
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (!_pauseObject) {
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
	}

    protected override void FixedUpdate()
    {
		if (!_pauseObject) {
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
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
		if (!_pauseObject) {
			switch (other.tag) {
					case "Smut": 
						TakeDamage();
						EatBook(other.gameObject, 0);		
						break;		
					case "PickUp":					    
						EatBook(other.gameObject);				
						break;			
					case "Etiquette":					    
						EatBook(other.gameObject, 5);									
						break;			
			}  

			if (GameManager.instance.Points >= PointsToWin) {
				// win
				TriggerWin();
			}
		}
    }

	private void TakeDamage() {
		Health--;

		if (Health <= 0) {
			// game over
			TriggerLoss();
		}
	}

	protected void EatBook(GameObject book, int increment = 1) {
		Destroy(book);
		//Add one to the current value of our count variable.
		GameManager.instance.Points = GameManager.instance.Points + increment;
		SetCountText();      
	}



	protected void TriggerWin(){
		PauseObject();
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
	}


	protected void TriggerLoss(){
		PauseObject();


		// SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

	}


	

    protected override void OnFinishedMoving()
	{
	}

}
