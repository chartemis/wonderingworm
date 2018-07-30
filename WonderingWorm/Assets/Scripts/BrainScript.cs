using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainScript : MonoBehaviour {


	private Animator _animator;
	// Use this for initialization
	void Start () {		
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {		
		_animator.SetInteger("Points", GameManager.instance.Points);
	}
}
