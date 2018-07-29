using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MovingObject : MonoBehaviour {

    private BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    public LayerMask blockingLayer;         //Layer on which collision will be checked.

    protected int count;

    public float moveTime;         //Time it will take object to move, in seconds.

    public float moveSpeed;

    public Text countText;

    protected Orientation _orientation = Orientation.DOWN;

    // Use this for initialization
    protected virtual void Start() {
        moveTime = 0.075f;
        moveSpeed = 2f;
        
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //Initialize count to zero.
        count = 0;
        
        SetCountText();
    }

	protected virtual void Update() {

	}

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {		
		float horizontal = Input.GetAxisRaw("Horizontal");		
		float vertical = Input.GetAxisRaw("Vertical");


        if (horizontal != 0 || vertical != 0) {
		    rb2D.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;
            SetCountText();
        }
    }

    protected void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
	
    protected abstract void OnFinishedMoving();

}


public enum Orientation
{
	DOWN = 0,
	UP = 1,
	LEFT = 2,
	RIGHT = 3
}
 