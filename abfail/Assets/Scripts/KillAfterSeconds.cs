using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAfterSeconds : MonoBehaviour {

    public float Seconds;
	
    private float killTime;

    private void Start()
    {
        killTime = Time.time + Seconds;
    }

    void Update ()
    {
	    if (Time.time > killTime)
        {
            Destroy(this.gameObject);
        }	
	}
}
