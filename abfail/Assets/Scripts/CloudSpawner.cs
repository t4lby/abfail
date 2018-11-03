using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour {

    public GameObject cloudPrefab;
    public Vector3 meanRelativeSpawnPosition;
    public Transform cameraTransform;
    public float spawnFrequency;
    public float spawnRadius;
    public float cloudDepth = 1;
    public float maxCloudSize;
    public float minCloudSize;

    private float nextSpawnDistance;
    private Vector3 lastCamPosition;

	void Start () {
        lastCamPosition = cameraTransform.position;
        nextSpawnDistance = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if ((cameraTransform.position - lastCamPosition).magnitude > nextSpawnDistance)
        {
            var cloud = Instantiate(
                cloudPrefab,
                cameraTransform.position + meanRelativeSpawnPosition + new Vector3(Random.Range(- spawnRadius, spawnRadius), 0, cloudDepth),
                Quaternion.identity);
            cloud.transform.localScale *= Random.Range(minCloudSize, maxCloudSize); 
            lastCamPosition = cameraTransform.position;
            nextSpawnDistance = Random.Range(1 / spawnFrequency, 2 / spawnFrequency);
        }
	}
}
