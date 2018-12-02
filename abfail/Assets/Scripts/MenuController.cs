using System.Collections;
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
}
