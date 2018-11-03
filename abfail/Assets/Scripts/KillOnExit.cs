using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOnExit : MonoBehaviour {

    public Transform cameraTransform;

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private void Update()
    {
        this.transform.position = new Vector3(
            cameraTransform.position.x,
            cameraTransform.position.y,
            0);
    }
}
