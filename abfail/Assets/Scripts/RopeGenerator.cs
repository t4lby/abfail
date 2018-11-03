﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeGenerator : MonoBehaviour {

    public GameObject ropePrefab;
    public Transform cameraTransform;
    public Vector3 spawnOffset;
    public Vector3 initialSpawnLocation;

    private Vector3 lastSpawnPosition;

	void Start () {
        // spawn initial rope
        lastSpawnPosition = Instantiate(
            ropePrefab,
            initialSpawnLocation,
            Quaternion.identity)
            .transform.
            position;
	}
	
	    
	void Update ()
    {
        //spawn next rope when camera position gets lower than first.
        if (cameraTransform.position.y < lastSpawnPosition.y)
        {
            lastSpawnPosition = Instantiate(
                ropePrefab,
                lastSpawnPosition + spawnOffset,
                Quaternion.identity)
                .transform
                .position;
        }
    }
}
