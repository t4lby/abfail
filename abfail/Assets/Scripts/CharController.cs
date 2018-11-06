using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Status
{
    Falling,
    Dead,
    OnPlatform
}

public class CharController : MonoBehaviour {

    public List<Rigidbody2D> CharacterRbs;
    public float deathSpeed;
    public float jumpForce;
    public PlatformSpawner platformSpawner;
    public GameObject restartButton;
    public Transform mainCamera;
    public float platformSpawnAhead = 3;
	
    private bool charAttemptedStop;
    private Status charStatus;
    private Collider2D currentPlatformCollider;
    private List<TargetJoint2D> platformTargetJoints;

    private void Start()
    {
        charAttemptedStop = false;
        charStatus = Status.Falling;
        platformTargetJoints = new List<TargetJoint2D>();
        restartButton.SetActive(false);
        for (int i = 0; i < platformSpawnAhead; i++)
        {
            platformSpawner.SpawnNextPlatform(this);
        }
    }

    private void Update ()
    {
		if (Input.GetKeyDown(KeyCode.Space) || 
            (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (charStatus == Status.OnPlatform)
            {
                currentPlatformCollider.enabled = false;
                charAttemptedStop = false;
                charStatus = Status.Falling;
                platformTargetJoints.ForEach(j => Destroy(j));
                platformTargetJoints.Clear();
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
            platformTargetJoints.Add(this.gameObject.AddComponent<TargetJoint2D>());
            var feet = FindObjectsOfType<Transform>()
                               .Where(go => go.gameObject.CompareTag("foot"));
            foreach (var foot in feet)
            {
                platformTargetJoints.Add(foot.gameObject.AddComponent<TargetJoint2D>());
            }
            platformSpawner.SpawnNextPlatform(this);
        }
        else if (charStatus == Status.Falling)
        {
            //kill
            this.GetComponent<AudioSource>().Play();
            CharacterRbs
                .ForEach
                (
                    (obj) => obj.GetComponent<Rigidbody2D>().constraints = 
                                RigidbodyConstraints2D.None
                );
            charStatus = Status.Dead;
            StartCoroutine(WaitThenPostDeath(2.0f));
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene("main");
    }

    private void SetVelocitiesToZero(List<Rigidbody2D> rbs)
    {
        foreach (var rb in rbs)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private IEnumerator WaitThenPostDeath(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        mainCamera.GetComponent<SuperBlur.SuperBlur>().enabled = true;
        restartButton.SetActive(true);
    }
}
