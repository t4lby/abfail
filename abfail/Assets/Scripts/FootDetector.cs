using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootDetector : MonoBehaviour {

    public CharController Character;
    public float killOffset;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("foot"))
        {
            Character.PlatformHit(this.GetComponent<Collider2D>());
        }
    }

    private void Update()
    {
        if (transform.position.y - Character.transform.position.y > killOffset)
        {
            Destroy(this.gameObject);
        }
    }
}
