using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {

    public List<Rigidbody2D> CharacterRbs;
	
    private bool charStoppedFlag;

    private void Start()
    {
        charStoppedFlag = false;
    }

    private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space) && !charStoppedFlag)
        {
            foreach (var rb in CharacterRbs)
            {
                rb.velocity = Vector2.zero;
            }
            charStoppedFlag = true;
        }
	}

    public void PlatformHit()
    {
        print("woiii");
    }
}
