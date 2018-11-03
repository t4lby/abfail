using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Status
{
    Falling,
    Dead,
    OnPlatform
}

public class CharacterController : MonoBehaviour {

    public List<Rigidbody2D> CharacterRbs;
    public float deathSpeed;
    public float jumpForce;
	
    private bool charAttemptedStop;
    private Status charStatus;
    private Collider2D currentPlatformCollider;
    private TargetJoint2D platformTargetJoint;

    private void Start()
    {
        charAttemptedStop = false;
        charStatus = Status.Falling;
    }

    private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space))
        {
            if (charStatus == Status.OnPlatform)
            {
                currentPlatformCollider.enabled = false;
                charAttemptedStop = false;
                charStatus = Status.Falling;
                Destroy(platformTargetJoint);
                foreach (var rb in CharacterRbs)
                {
                    rb.AddForce(new Vector2(0, jumpForce));
                }
            }
            else if (charStatus == Status.Falling && !charAttemptedStop)
            {
                SetVelocitiesToZero(CharacterRbs);
                charAttemptedStop = true;
            }
        }
	}

    public void PlatformHit(Collider2D platformCollider)
    {
        currentPlatformCollider = platformCollider;
        if (CharacterRbs.Select(rb => rb.velocity.magnitude).Sum() / CharacterRbs.Count() < deathSpeed 
            && charStatus == Status.Falling)
        {
            charStatus = Status.OnPlatform;
            platformTargetJoint = this.gameObject.AddComponent<TargetJoint2D>();
        }
        else if (charStatus == Status.Falling)
        {
            //kill
            charStatus = Status.Dead;
        }
    }

    private void SetVelocitiesToZero(List<Rigidbody2D> rbs)
    {
        foreach (var rb in rbs)
        {
            rb.velocity = Vector2.zero;
        }
    }
}
