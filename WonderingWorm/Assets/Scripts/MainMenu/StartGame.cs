using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {

    public AudioSource gameStart;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseUp()
	{
        gameStart.Play();
		SceneManager.LoadScene("Main", LoadSceneMode.Single);
	}
}
