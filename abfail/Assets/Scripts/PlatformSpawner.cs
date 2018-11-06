using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlatformSpawner : MonoBehaviour {

    public Transform initialPlatform;
    public float platformOffset;
    public float minPlatformDistance;
    public GameObject platformPrefab;

    private int platformCount;
    private Vector2 lastPlatformLocation;

    private void Awake()
    {
        platformCount = 1;
        lastPlatformLocation = initialPlatform.position;
    }

    public void SpawnNextPlatform(CharController character)
    {
        var platform = Instantiate(
                            platformPrefab,
            lastPlatformLocation + Vector2.down * Random.Range(minPlatformDistance, minPlatformDistance + platformCount*platformOffset),
                            Quaternion.identity);
        platform.GetComponent<FootDetector>().Character = character;
        lastPlatformLocation = platform.transform.position;
        platformCount += 1;
    }
}
