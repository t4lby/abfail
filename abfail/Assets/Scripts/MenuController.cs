﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public List<GameObject> MenuObjects;
    public List<GameObject> InstructionObjects;
    public List<GameObject> CreditsObjects;

    public void LoadMainGame()
    {
        SceneManager.LoadScene("main");
    }

    public void LoadInstructions()
    {
        MenuObjects.ForEach(o => o.SetActive(false));
        InstructionObjects.ForEach(o => o.SetActive(true));
    }

    public void LoadCredits()
    {
        //qq: needs removing, left for testing.
        PlayerPrefs.SetString("TorsoUnlock", "1111100");
        MenuObjects.ForEach(o => o.SetActive(false));
        CreditsObjects.ForEach(o => o.SetActive(true));
    }

    public void Back()
    {
        CreditsObjects.ForEach(o => o.SetActive(false));
        InstructionObjects.ForEach(o => o.SetActive(false));
        MenuObjects.ForEach(o => o.SetActive(true));
    }

    public void LoadOutfits()
    {
        SceneManager.LoadScene("outfits");
    }

    private float _resets = 0;
    public void IncrementResetCount()
    {
        _resets += 1;
        if (_resets > 2)
        {
            PlayerPrefs.SetString("TorsoUnlock", "11111100");
            PlayerPrefs.SetString("TrouserUnlock", "1110");
            PlayerPrefs.SetInt("BestScore", 0);
            PlayerPrefs.SetInt("TorsoIndex", 5);
            PlayerPrefs.SetInt("TrouserIndex", 0);
        }
    }
}
