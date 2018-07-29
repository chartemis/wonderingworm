﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour {

	public float SpawnInterval; 
	// private DateTime _lastSpawn;
	private List<Vector2> spawnPoints;   

	public GameObject[] BookSpawns;

	// Use this for initialization
	void Start () {
		// SpawnInterval = 3f;
		// _lastSpawn = DateTime.Now;

		InvokeRepeating ("SpawnBook", SpawnInterval, SpawnInterval);
		spawnPoints = new List<Vector2>();
		spawnPoints.Add(new Vector2(-10f, 2.5f));
	}
	
	private void SpawnBook () {

        int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Count);		
        int bookIndex = UnityEngine.Random.Range (0, BookSpawns.Length);

		var spawnedBook = Instantiate (BookSpawns[bookIndex], spawnPoints[spawnPointIndex], Quaternion.identity);

		var rigidBody = spawnedBook.GetComponent<Rigidbody2D>();

		rigidBody.velocity = new Vector2(3, -1);


		if (bookIndex == (int)BookTypes.SMUT) {
			spawnedBook.tag = "Smut";
		}

        float delay = 10; //delay is in seconds
        Destroy(spawnedBook, delay); //destroys object after 10 seconds
    }

}

public enum BookTypes
{
	MATH = 0,
	ART = 1,
	SCIENCE = 2,
	SMUT = 3,
	ETIQUETTE = 4
}
