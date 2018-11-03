using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CameraFollowTarget : MonoBehaviour {
    
    public Transform Target;

    public float SmoothSpeed = 0.125f;

    public float MinOrthopedicSize = 1;

    public Vector3 Offset;
	
	private void LateUpdate ()
    {
        transform.position = Target.position + Offset;
	}

    private void Update()
    {
        if (this.GetComponent<Camera>().orthographicSize + Input.mouseScrollDelta.y > MinOrthopedicSize)
        {
            this.GetComponent<Camera>().orthographicSize += Input.mouseScrollDelta.y;
        }
    }
}
