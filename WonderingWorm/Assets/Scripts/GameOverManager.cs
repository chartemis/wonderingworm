using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {
	
	private Animator _animator;
	private float _restartDelay = 5f;
	private float _restartTimer;

	// Use this for initialization
	void Start () {
		_animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.GameOver) {
			_animator.SetTrigger("GameOver");
			
			_restartTimer += Time.deltaTime;
			if (_restartTimer >= _restartDelay) {				
				GameManager.instance.RestartGame();		
			}
		}	
	}	
}
