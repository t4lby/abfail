using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour {

    public Transform initialPlatform;
    public Vector2 initialOffset;
    public float minorRandomOffset;
    public GameObject platformPrefab;

    private int platformCount;
    private Vector2 lastPlatformLocation;

    private void Start()
    {
        platformCount = 1;
        lastPlatformLocation = initialPlatform.position;
    }

    public void SpawnNextPlatform(CharController character)
    {
        var platform = Instantiate(
                            platformPrefab,
                            lastPlatformLocation + initialOffset * platformCount * Random.Range(1-minorRandomOffset, 1+minorRandomOffset),
                            Quaternion.identity);
        platform.GetComponent<FootDetector>().Character = character;
        lastPlatformLocation = platform.transform.position;
        platformCount += 1;
    }
}
