using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour {

    private BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    public LayerMask blockingLayer;         //Layer on which collision will be checked.

    private int count;

    public float moveTime;         //Time it will take object to move, in seconds.

    public float moveSpeed;

    protected Orientation _orientation = Orientation.LEFT;

    // Use this for initialization
    protected virtual void Start() {
        moveTime = 0.075f;
        moveSpeed = 2f;

        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //Sets counter
        count = 0;
    }

	protected virtual void Update() {

	}

    // Update is called once per frame
    void FixedUpdate()
    {		
		float horizontal = Input.GetAxisRaw("Horizontal");		
		float vertical = Input.GetAxisRaw("Vertical");


        if (horizontal != 0 || vertical != 0) {
		    rb2D.velocity = new Vector2(horizontal * moveSpeed, vertical * moveSpeed);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
        }
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
 