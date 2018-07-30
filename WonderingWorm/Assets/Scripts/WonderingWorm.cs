using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WonderingWorm : MovingObject {

	private Animator _animator;
    public AudioSource audioSlide;
	private BoxCollider2D _boxCollider;
    public AudioSource GoodBookGrab;
    public AudioSource SmutGrab;
	
	public int PointsToWin;
	public int Health;

	public float Level1Speed = 2f;
	public float Level2Speed = 4f;
	public float Level3Speed = 6f;
	
	private int _level;

	// Use this for initialization
	protected override void Start () {
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();
        _orientation = Orientation.LEFT;
		Health = 3;
		PointsToWin = 100;
		_pauseObject = false;
		moveSpeed = Level1Speed;
		_level = 1;
				
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
                    SmutGrab.Play();
						break;		
					case "PickUp":					    
						EatBook(other.gameObject,10);
                        GoodBookGrab.Play();
                    break;			
					case "Etiquette":					    
						UpgradeWorm();
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

	protected void UpgradeWorm() {
		switch (_level) {
			case 1:
				_level++;
				moveSpeed = Level2Speed;
				_boxCollider.size = new Vector3(.83f, .92f, 0);
				break;
			case 2:
				_level++;
				moveSpeed = Level3Speed;
				_boxCollider.size = new Vector3(1.05f, 1.05f, 0);
				break;
		}
		
		_animator.SetInteger("Level", _level);
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

		GameManager.instance.GameOver = true;
	}


	

    protected override void OnFinishedMoving()
	{
	}

}
