﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookSpawner : MonoBehaviour {

	public float SpawnInterval; 
	// private DateTime _lastSpawn;
	private List<Vector2> spawnPoints;   

	public GameObject[] BookSpawns;

    public GameObject ProperEtiquitte;

	// Use this for initialization
	void Start () {
		// SpawnInterval = 3f;
		// _lastSpawn = DateTime.Now;

		InvokeRepeating ("SpawnBook", SpawnInterval, SpawnInterval);
		spawnPoints = new List<Vector2>();
		spawnPoints.Add(new Vector2(-10f, 2.5f));
	}
	
	private void SpawnBook () {

        //int spawnPointIndex = UnityEngine.Random.Range (0, spawnPoints.Count);		old coordinate generation
        int bookIndex = UnityEngine.Random.Range (0, BookSpawns.Length);

        float spawnRadius = 15;
        double spawnPointTheta = UnityEngine.Random.Range(0, 360);
        double spawnPointX = spawnRadius * System.Math.Cos(spawnPointTheta);
        double spawnPointY = spawnRadius * System.Math.Sin(spawnPointTheta);

        var spawnedBook = Instantiate (BookSpawns[bookIndex], new Vector3((float)spawnPointX, (float)spawnPointY), Quaternion.identity);

		var rigidBody = spawnedBook.GetComponent<Rigidbody2D>();

        double xvar = UnityEngine.Random.Range(-(spawnRadius / 2), (spawnRadius / 2));
        double yvar = UnityEngine.Random.Range(-(spawnRadius / 2), (spawnRadius / 2));

        rigidBody.velocity = new Vector2((float)(-spawnPointX + xvar) / 3, (float)(-spawnPointY + yvar) / 3);


		if (bookIndex == (int)BookTypes.SMUT) {
			spawnedBook.tag = "Smut";
		}

		SpawnPE();

        float delay = 10; //delay is in seconds
        Destroy(spawnedBook, delay); //destroys object after 10 seconds
    }

	private void SpawnPE () {
        int propetiquettespawn = UnityEngine.Random.Range(0, 10);

        if (propetiquettespawn == 9)
        {

            int spawnRadius = 15;
            double spawnPointTheta = UnityEngine.Random.Range(0, 360);
            double spawnPointX = spawnRadius * System.Math.Cos(spawnPointTheta);
            double spawnPointY = spawnRadius * System.Math.Sin(spawnPointTheta);

            var spawnedBook = Instantiate(ProperEtiquitte, new Vector3((float)spawnPointX, (float)spawnPointY, 0), Quaternion.identity);

            double xvar = UnityEngine.Random.Range(-(spawnRadius / 2), (spawnRadius / 2));
            double yvar = UnityEngine.Random.Range(-(spawnRadius / 2), (spawnRadius / 2));

            var rigidBody = spawnedBook.GetComponent<Rigidbody2D>();

            rigidBody.velocity = new Vector2((float)(-spawnPointX + xvar) / 2, (float)(-spawnPointY + yvar) / 2);

			spawnedBook.tag = "Etiquette";

            float delay = 10; //delay is in seconds
            Destroy(spawnedBook, delay); //destroys object after 10 seconds
        }
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
