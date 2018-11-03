using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootDetector : MonoBehaviour {

    public CharacterController Character;

    private void OnTriggerEnter2D(Collider other)
    {
        if (other.CompareTag("foot"))
        {
            Character.PlatformHit();
        }
    }
}
