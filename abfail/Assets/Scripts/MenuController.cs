using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public void LoadMainGame()
    {
        SceneManager.LoadScene("main");
    }

    public void LoadInstructions()
    {
        
    }

    public void LoadOutfits()
    {
        SceneManager.LoadScene("outfits");
    }
}
