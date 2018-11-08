using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;

public class PlatformSpawner : MonoBehaviour {

    public Transform initialPlatform;
    public float platformOffset;
    public float minPlatformDistance;
    public GameObject platformPrefab;
    public List<Sprite> platformSprites;

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
        platform.GetComponent<SpriteRenderer>().sprite = platformSprites[Random.Range(0, platformSprites.Count - 1)];
        lastPlatformLocation = platform.transform.position;
        platformCount += 1;
    }
}
