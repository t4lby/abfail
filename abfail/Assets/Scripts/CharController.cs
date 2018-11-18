﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum Status
{
    Falling,
    Dead,
    OnPlatform
}

public class CharController : MonoBehaviour {
    
    public List<Rigidbody2D> CharacterRbs;
    public List<Rigidbody2D> HandRbs;
    public float deathSpeed;
    public float jumpForce;
    public PlatformSpawner platformSpawner;
    public GameObject restartButton;
    public Transform mainCamera;
    public float ropeHandsXCoord;
    public float platformSpawnAhead = 3;
    public FaceController faceController;
    public CameraFollowTarget cameraController;
    public float CalmSpeed;
    public float PanicSpeed;
    public Text ScoreText;
    public Text speedText;
    public Image healthMaskImage;
    public Image failImage;
    public GameObject finalScore;
    public GameObject bestScore;
    public GameObject currentScore;
    public GameObject currentSpeed;
    public GameObject[] healthObjects;
	
    private bool charAttemptedStop;
    private float health;
    private Status charStatus;
    private Collider2D currentPlatformCollider;
    private List<TargetJoint2D> platformTargetJoints;
    private int score;

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
        FixHands();
        score = 0;
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
                FixHands();
            }
            else if (charStatus == Status.Falling && !charAttemptedStop)
            {
                SetVelocitiesToZero(CharacterRbs);
                ReleaseHands();
                charAttemptedStop = true;
            }
        }

        if (charStatus == Status.OnPlatform)
        {
            ReleaseHands();
            HandRbs.ForEach(rb => rb.MovePosition( new Vector3(ropeHandsXCoord,
                                                               rb.transform.position.y)));
        }
        var speed = GetComponent<Rigidbody2D>().velocity.magnitude;
        speedText.text = Mathf.Round( speed ) / 10  + " m / s";
        if (charStatus != Status.Dead && !charAttemptedStop)
        {
            if (speed < CalmSpeed)
            {
                faceController.SetCalm();
            }
            else if (speed < PanicSpeed)
            {
                faceController.SetLessCalm();
            }
            else
            {
                faceController.SetPanicked();
            }
        }
        if (charStatus == Status.Falling && health > 0)
        {
            health -= Time.deltaTime;
            SetHealth(health);
        }
	}

    public void PlatformHit(Collider2D platformCollider)
    {
        currentPlatformCollider = platformCollider;
        var speed = CharacterRbs.Select(rb => rb.velocity.magnitude).Sum() / CharacterRbs.Count();
        if (charStatus == Status.Falling)
        {
            health = speed / deathSpeed;
            SetHealth(health);
        }
        if (speed < deathSpeed 
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
            score += 1;
            ScoreText.text = score.ToString();
            platformSpawner.SpawnNextPlatform(this);
            charAttemptedStop = false;
        }
        else if (charStatus == Status.Falling)
        {
            //kill
            this.GetComponent<AudioSource>().Play();
            CharacterRbs
                .ForEach
                (
                    SetRbDead
                );
            cameraController.LookAhead = 0.1f;
            charStatus = Status.Dead;
            if (PlayerPrefs.HasKey("BestScore"))
            {
                if (PlayerPrefs.GetInt("BestScore") < score)
                {
                    PlayerPrefs.SetInt("BestScore", score);
                }
            }
            else 
            {
                PlayerPrefs.SetInt("BestScore", score);
            }
            finalScore.GetComponentInChildren<Text>().text = score.ToString();
            bestScore.GetComponentInChildren<Text>().text = PlayerPrefs.GetInt("BestScore").ToString();
            StartCoroutine(WaitThenPostDeath(3.0f));
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
        failImage.enabled = true;
        bestScore.SetActive(true);
        finalScore.SetActive(true);
        restartButton.SetActive(true);
        currentScore.SetActive(false);
        currentSpeed.SetActive(false);
        healthObjects.ToList().ForEach(obj => obj.SetActive(false));
    }

    private void FixHands()
    {
        HandRbs.ForEach(rb => rb.constraints = RigidbodyConstraints2D.FreezePositionX);
    }

    private void ReleaseHands()
    {
        HandRbs.ForEach(rb => rb.constraints = RigidbodyConstraints2D.None);
    }
    private void SetRbDead(Rigidbody2D rb)
    {
        rb.constraints = RigidbodyConstraints2D.None;
        rb.drag = 0;
        rb.gravityScale = 3;
        rb.GetComponents<HingeJoint2D>().ToList()
          .ForEach(
              j => j.limits = new JointAngleLimits2D { min = -180, max = 180 }
             );
    }
    private void SetHealth(float value)
    {
        healthMaskImage.rectTransform.sizeDelta = new Vector2(healthMaskImage.rectTransform.sizeDelta.x,
                                                                  value * 200);
        healthMaskImage.rectTransform.anchoredPosition = new Vector3(healthMaskImage.rectTransform.anchoredPosition.x,
                                                                     value * 100);
    }

    public void QuitToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
