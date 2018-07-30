using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScreenWorm : MonoBehaviour {

    Rigidbody2D rb2d;


	// Use this for initialization
	void Start () {
        rb2d = GetComponent<Rigidbody2D>();


        InvokeRepeating("ResetWorm", 0f, 5f);
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void ResetWorm()
    {
        rb2d.position = new Vector2(-5f, -2f);

        rb2d.velocity = new Vector2(6f, 0f);

    }
}
