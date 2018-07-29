using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MovingObject : MonoBehaviour {

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    public LayerMask blockingLayer;         //Layer on which collision will be checked.

    private float inverseMoveTime;          //Used to make movement more efficient.
    private int count;

    public float moveTime;         //Time it will take object to move, in seconds.

    public float moveSpeed;

    public Text countText;

    protected Orientation _orientation = Orientation.DOWN;

    // Use this for initialization
    protected virtual void Start() {
        moveTime = 0.075f;
        moveSpeed = .05f;

        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        //Initialize count to zero.
        count = 0;
        SetCountText ();

        //By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
        inverseMoveTime = 1f / moveTime;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);

            //Add one to the current value of our count variable.
            count = count + 1;
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }

    //Move returns true if it is able to move and false if not. 
    //Move takes parameters for x direction, y direction and a RaycastHit2D to check collision.
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        //Store start position to move from, based on objects current transform position.
        Vector2 start = transform.position;

        // Calculate end position based on the direction parameters passed in when calling Move.
        Vector2 end = start + new Vector2(xDir * moveSpeed, yDir * moveSpeed);

        //Disable the boxCollider so that linecast doesn't hit this object's own collider.
        boxCollider.enabled = false;

        //Cast a line from start point to end point checking collision on blockingLayer.
        hit = Physics2D.Linecast(start, end, blockingLayer);

        //Re-enable boxCollider after linecast
        boxCollider.enabled = true;

        //Check if anything was hit
        if (hit.transform == null)
        {
            rb2D.MovePosition(end);

            //Return true to say that Move was successful
            return true;
        }

        //If something was hit, return false, Move was unsuccesful.
        return false;
    }

	//AttemptMove takes a generic parameter T to specify the type of component we expect our unit to interact with if blocked (Player for Enemies, Wall for Player).
	protected virtual bool AttemptMove(int xDir, int yDir, out RaycastHit2D hit)
	{
		//Hit will store whatever our linecast hits when Move is called.

		//Set canMove to true if Move was successful, false if failed.
		bool canMove = Move(xDir, yDir, out hit);

		//Check if nothing was hit by linecast
		if (hit.transform == null)
			//If nothing was hit, return and don't execute further code.
			return canMove;

		return canMove;
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
 