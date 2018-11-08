using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraFollowTarget : MonoBehaviour {
    
    public Transform Target;

    public float SmoothSpeed = 0.125f;

    public float MinOrthopedicSize = 1;

    public Vector3 Offset;

    public float LookAhead;


    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position,
                                          Target.position + Offset + 
                                          new Vector3 (0, Target.GetComponent<Rigidbody2D>().velocity.y * LookAhead, 0),
                                          SmoothSpeed);
    }

    private void Update()
    {
        if (this.GetComponent<Camera>().orthographicSize + Input.mouseScrollDelta.y > MinOrthopedicSize)
        {
            this.GetComponent<Camera>().orthographicSize += Input.mouseScrollDelta.y;
        }
    }
}
