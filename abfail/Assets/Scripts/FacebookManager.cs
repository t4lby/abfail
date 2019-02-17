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
        FB.ShareLink(contentTitle: "ABFAIL reflex rage game",
                     contentURL: new System.Uri("https://play.google.com/store/apps/details?id=com.PrawnStarStudios.Abfail"),
                     contentDescription: "I failed at abfail",
                     photoURL: new System.Uri("https://lh3.googleusercontent.com/dT1jxSDN37ZiP4RDsUt2LJn_3NA7xXhChSHVgHosSrHw86MlSsHCZyM6D0YfCdI7ODs=s360-rw"),
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
