using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCharacterSpawner : MonoBehaviour {

    public float minScale = 0.2f;
    public float maxScale = 0.5f;

    public float spawnRadius = 2f;

    public float ragDoll;

    public float minSpawnFrequency = 1f;
    public float maxSpawnFrequency = 5f;

    public GameObject MockPlayerPrefab;

    private float nextSpawnTime;

    private void Start()
    {
        nextSpawnTime = Time.time + Random.Range(minSpawnFrequency, maxSpawnFrequency);
    }

    void Update ()
    {
		if (Time.time > nextSpawnTime)
        {
            nextSpawnTime = Time.time + Random.Range(minSpawnFrequency, maxSpawnFrequency);
            var player = Instantiate(MockPlayerPrefab, 
                                    this.transform.position + Vector3.right * Random.Range(-spawnRadius, spawnRadius),
                                    Quaternion.identity);
            player.transform.localScale *= Random.Range(minScale, maxScale);
            foreach (var rb in player.GetComponentsInChildren<Rigidbody2D>())
            {
                
                rb.AddForce( new Vector2(Random.Range(-ragDoll, ragDoll), Random.Range(-ragDoll, ragDoll)));
                if (rb.gameObject.CompareTag("head"))
                {
                    rb.AddForce(Vector2.left * 10000);
                }
            }
        }
	}
}
