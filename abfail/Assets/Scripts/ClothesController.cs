using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothesController : MonoBehaviour {

    public SpriteRenderer torsoRenderer;
    public SpriteRenderer leftArmRenderer;
    public SpriteRenderer rightArmRenderer;
    public SpriteRenderer lowerLeftArmRenderer;
    public SpriteRenderer lowerRightArmRenderer;

    public SpriteRenderer crotchRenderer;
    public SpriteRenderer leftLegRenderer;
    public SpriteRenderer rightLegRenderer;
    public SpriteRenderer lowerLeftLegRenderer;
    public SpriteRenderer lowerRightLegRenderer;

    public Renderer trouserCriteriaRender;
    public Renderer torsoCriteriaRender;

    public SpriteRenderer wang;
    public SpriteRenderer wong;

    // The following sprites sets must be ordered together.
    // 1 + the maximum index is naked in game.
    public List<Sprite> torsoSprites;
    public List<Sprite> leftArmSprites;
    public List<Sprite> rightArmSprites;
    public List<Sprite> lowerLeftArmSprites;
    public List<Sprite> lowerRightArmSprites;

    public List<String> torsoUnlockCriteria;

    public List<Sprite> crotchSprites;
    public List<Sprite> leftLegSprites;
    public List<Sprite> rightLegSprites;
    public List<Sprite> lowerLeftLegSprites;
    public List<Sprite> lowerRightLegSprites;

    public List<String> trouserUnlockCriteria;


    private int torsoCount;
    private int trouserCount;
    private int torsoIndex;
    private int trouserIndex;

    private void Start()
    {
        //Check consistency of sprite counts.
        if (!(torsoSprites.Count == leftArmSprites.Count &&
              torsoSprites.Count == rightArmSprites.Count &&
              torsoSprites.Count == lowerRightArmSprites.Count &&
              torsoSprites.Count == lowerLeftArmSprites.Count))
        {
            throw new UnityException("Counts for torso sprites are not equal!");
        }
        else
        {
            torsoCount = torsoSprites.Count;
        }
        if (!(crotchSprites.Count == leftLegSprites.Count &&
              crotchSprites.Count == rightLegSprites.Count &&
              crotchSprites.Count == lowerRightLegSprites.Count &&
              crotchSprites.Count == lowerLeftLegSprites.Count))
        {
            throw new UnityException("Counts for trouser sprites are not equal!");
        }
        else
        {
            trouserCount = crotchSprites.Count;
        }

        // set up player prefs
        if (!PlayerPrefs.HasKey("TorsoIndex"))
        {
            PlayerPrefs.SetInt("TorsoIndex", 0);
        }
        if (!PlayerPrefs.HasKey("TrouserIndex"))
        {
            PlayerPrefs.SetInt("TrouserIndex", 0);
        }
        if (!PlayerPrefs.HasKey("TorsoUnlock"))
        {
            PlayerPrefs.SetString("TorsoUnlock", "10");
        }
        if (!PlayerPrefs.HasKey("TrouserUnlock"))
        {
            PlayerPrefs.SetString("TrouserUnlock", "10");
        }
        DressTorso(PlayerPrefs.GetInt("TorsoIndex"));
        DressLegs(PlayerPrefs.GetInt("TrouserIndex"));
    }

    public void IncrementTorsoIndex(int increment)
    {
        torsoIndex = (torsoIndex + increment + torsoCount + 1) % (torsoCount + 1);
        DressTorso(torsoIndex);
    }

    public void IncrementTrouserIndex(int increment)
    {
        trouserIndex = (trouserIndex + increment + trouserCount + 1) % (trouserCount + 1);
        DressLegs(trouserIndex);
    }

    private void DressTorso(int index)
    {
        if (index == torsoCount)
        {
            torsoRenderer.enabled = false;
            leftArmRenderer.enabled = false;
            rightArmRenderer.enabled = false;
            lowerLeftArmRenderer.enabled = false;
            lowerRightArmRenderer.enabled = false;
        }
        else
        {
            torsoRenderer.enabled = true;
            leftArmRenderer.enabled = true;
            rightArmRenderer.enabled = true;
            lowerLeftArmRenderer.enabled = true;
            lowerRightArmRenderer.enabled = true;

            torsoRenderer.sprite = torsoSprites[index];
            leftArmRenderer.sprite = leftArmSprites[index];
            rightArmRenderer.sprite = rightArmSprites[index];
            lowerLeftArmRenderer.sprite = lowerLeftArmSprites[index];
            lowerRightArmRenderer.sprite = lowerRightArmSprites[index];
        }
        if (PlayerPrefs.GetString("TorsoUnlock")[index] == '1'
            && torsoCriteriaRender != null)
        {
            torsoRenderer.color = Color.white;
            leftArmRenderer.color = Color.white;
            rightArmRenderer.color = Color.white;
            lowerLeftArmRenderer.color = Color.white;
            lowerRightArmRenderer.color = Color.white;

            torsoCriteriaRender.enabled = false;
        }
        else if (torsoCriteriaRender != null)
        {
            torsoRenderer.color = Color.black;
            leftArmRenderer.color = Color.black;
            rightArmRenderer.color = Color.black;
            lowerLeftArmRenderer.color = Color.black;
            lowerRightArmRenderer.color = Color.black;

            torsoCriteriaRender.enabled = true;
            torsoCriteriaRender.GetComponent<TextMesh>().text =
                              torsoUnlockCriteria[index];
        }
    }

    private void DressLegs(int index)
    {
        if (index == trouserCount)
        {
            crotchRenderer.enabled = false;
            leftLegRenderer.enabled = false;
            rightLegRenderer.enabled = false;
            lowerLeftLegRenderer.enabled = false;
            lowerRightLegRenderer.enabled = false;

            wang.enabled = true;
            wong.enabled = true;
        }
        else
        {
            crotchRenderer.enabled = true;
            leftLegRenderer.enabled = true;
            rightLegRenderer.enabled = true;
            lowerLeftLegRenderer.enabled = true;
            lowerRightLegRenderer.enabled = true;

            crotchRenderer.sprite = crotchSprites[index];
            leftLegRenderer.sprite = leftLegSprites[index];
            rightLegRenderer.sprite = rightLegSprites[index];
            lowerLeftLegRenderer.sprite = lowerLeftLegSprites[index];
            lowerRightLegRenderer.sprite = lowerRightLegSprites[index];

            wang.enabled = false;
            wong.enabled = false;
        }
        if (PlayerPrefs.GetString("TrouserUnlock")[index] == '1'
            && trouserCriteriaRender != null)
        {
            crotchRenderer.color = Color.white;
            leftLegRenderer.color = Color.white;
            rightLegRenderer.color = Color.white;
            lowerLeftLegRenderer.color = Color.white;
            lowerRightLegRenderer.color = Color.white;

            trouserCriteriaRender.enabled = false;

            wang.color = Color.white;
            wong.color = Color.white;
        }
        else if (trouserCriteriaRender != null)
        {
            crotchRenderer.color = Color.black;
            leftLegRenderer.color = Color.black;
            rightLegRenderer.color = Color.black;
            lowerLeftLegRenderer.color = Color.black;
            lowerRightLegRenderer.color = Color.black;

            trouserCriteriaRender.enabled = true;
            trouserCriteriaRender.GetComponent<TextMesh>().text =
                              trouserUnlockCriteria[index];
            wang.color = Color.black;
            wong.color = Color.black;
        }
    }

    public void GoToMenu()
    {
        if (PlayerPrefs.GetString("TorsoUnlock")[torsoIndex] == '1')
        {
            PlayerPrefs.SetInt("TorsoIndex", torsoIndex);
        }
        if (PlayerPrefs.GetString("TrouserUnlock")[trouserIndex] == '1')
        {
            PlayerPrefs.SetInt("TrouserIndex", trouserIndex);
        }
        SceneManager.LoadScene("menu");
    }
}
