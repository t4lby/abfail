using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClothesController : MonoBehaviour {

    public SpriteRenderer torsoRenderer;
    public SpriteRenderer leftArmRenderer;
    public SpriteRenderer rightArmRenderer;
    public SpriteRenderer crotchRenderer;
    public SpriteRenderer leftLegRenderer;
    public SpriteRenderer rightLegRenderer;

    public SpriteRenderer wang;
    public SpriteRenderer wong;

    // The following sprites sets must be ordered together.
    // 1 + the maximum index is naked in game.
    public List<Sprite> torsoSprites;
    public List<Sprite> leftArmSprites;
    public List<Sprite> rightArmSprites;
    public List<Sprite> crotchSprites;
    public List<Sprite> leftLegSprites;
    public List<Sprite> rightLegSprites;

    private int torsoCount;
    private int trouserCount;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("TorsoIndex"))
        {
            PlayerPrefs.SetInt("TorsoIndex", 0);
        }
        if (!PlayerPrefs.HasKey("TrouserIndex"))
        {
            PlayerPrefs.SetInt("TrouserIndex", 0);
        }
        if (!(torsoSprites.Count == leftArmSprites.Count &&
              torsoSprites.Count == rightArmSprites.Count))
        {
            throw new UnityException("Counts for torso sprites are not equal!");
        }
        else
        {
            torsoCount = torsoSprites.Count;
        }
        if (!(crotchSprites.Count == leftLegSprites.Count &&
              crotchSprites.Count == rightLegSprites.Count))
        {
            throw new UnityException("Counts for trouser sprites are not equal!");
        }
        else
        {
            trouserCount = crotchSprites.Count;
        }
        DressTorso(PlayerPrefs.GetInt("TorsoIndex"));
        DressLegs(PlayerPrefs.GetInt("TrouserIndex"));
    }

    public void IncrementTorsoIndex(int increment)
    {
        var torsoIndex = PlayerPrefs.GetInt("TorsoIndex");
        torsoIndex = (torsoIndex + increment + torsoCount + 1) % (torsoCount + 1);
        DressTorso(torsoIndex);
        PlayerPrefs.SetInt("TorsoIndex", torsoIndex);
    }

    public void IncrementTrouserIndex(int increment)
    {
        var trouserIndex = PlayerPrefs.GetInt("TrouserIndex");
        trouserIndex = (trouserIndex + increment + trouserCount + 1) % (trouserCount + 1);
        DressLegs(trouserIndex);
        PlayerPrefs.SetInt("TrouserIndex", trouserIndex);
    }

    private void DressTorso(int index)
    {
        if (index == torsoCount)
        {
            torsoRenderer.enabled = false;
            leftArmRenderer.enabled = false;
            rightArmRenderer.enabled = false;
        }
        else
        {
            torsoRenderer.enabled = true;
            leftArmRenderer.enabled = true;
            rightArmRenderer.enabled = true;
            torsoRenderer.sprite = torsoSprites[index];
            leftArmRenderer.sprite = leftArmSprites[index];
            rightArmRenderer.sprite = rightArmSprites[index];
        }
    }

    private void DressLegs(int index)
    {
        if (index == trouserCount)
        {
            crotchRenderer.enabled = false;
            leftLegRenderer.enabled = false;
            rightLegRenderer.enabled = false;
            wang.enabled = true;
            wong.enabled = true;
        }
        else
        {
            crotchRenderer.enabled = true;
            leftLegRenderer.enabled = true;
            rightLegRenderer.enabled = true;
            crotchRenderer.sprite = crotchSprites[index];
            leftLegRenderer.sprite = leftLegSprites[index];
            rightLegRenderer.sprite = rightLegSprites[index];
            wang.enabled = false;
            wong.enabled = false;
        }
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("menu");
    }
}
