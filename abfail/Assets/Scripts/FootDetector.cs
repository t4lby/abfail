using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootDetector : MonoBehaviour {

    public CharController Character;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("foot"))
        {
            Character.PlatformHit(this.GetComponent<Collider2D>());
        }
    }
}
