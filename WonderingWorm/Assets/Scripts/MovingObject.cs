using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class MovingObject : MonoBehaviour {

    private BoxCollider2D boxCollider;
    protected Rigidbody2D rb2D;
    public LayerMask blockingLayer;         //Layer on which collision will be checked.


    public float moveSpeed = 2f;

    public Text countText;

    protected Orientation _orientation = Orientation.RIGHT;

    
	protected bool _pauseObject;

    // Use this for initialization
    protected virtual void Start() {
        //Get a component reference to this object's BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        //Get a component reference to this object's Rigidbody2D
        rb2D = GetComponent<Rigidbody2D>();

        _pauseObject = false;

        SetCountText();
    }

    protected void PauseObject() {
        rb2D.velocity = Vector2.zero;
        _pauseObject = true;
    }

    protected void UnpauseObject() {
        _pauseObject = false;
    }

	protected virtual void Update() {

	}

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {		
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        
    }

    protected void SetCountText()
    {
        countText.text = "Count: " + GameManager.instance.Points.ToString();
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
 