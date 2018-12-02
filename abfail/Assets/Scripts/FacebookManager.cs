using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Facebook.Unity;

public class FacebookManager : MonoBehaviour
{
    //public Text UserIdText;
    public CharController charController;
    public Text ShareText;

    private void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void LogIn()
    {
        FB.LogInWithReadPermissions(callback: OnLogIn);
    }

    private void OnLogIn(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            AccessToken token = AccessToken.CurrentAccessToken;
            Share();
        }
        else
        {
            ShareText.text = result.Error;
        }
    }

    public void Share()
    {
        FB.ShareLink(contentTitle: "Badger Boyz R Gr8",
            contentURL: new System.Uri("http://badgerboysband.com"),
            contentDescription: "good times",
            callback: OnShare);
    }

    private void OnShare(IShareResult result)
    {
        if (result.Cancelled || !string.IsNullOrEmpty(result.Error))
        {
            ShareText.text = "Error Sharing :(";
        }
        else
        {
            charController.SuccesfulShareUnlock();
            ShareText.text = "Success";
        }
    }
}
