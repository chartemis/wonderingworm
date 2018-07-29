using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;    

	
    public int Points;
	public bool GameOver;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void Awake()
	{
		//Check if instance already exists
		if (instance == null)

			//if not, set instance to this
			instance = this;

		//If instance already exists and it's not this:
		else if (instance != this)

		//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
		Destroy(gameObject);

		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		InitGame();
	}

	void InitGame()
	{
		// Do any initialization here
		Points = 0;
		GameOver = false;
	}

	public void RestartGame() {
		SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);		
		InitGame();
	}
}
