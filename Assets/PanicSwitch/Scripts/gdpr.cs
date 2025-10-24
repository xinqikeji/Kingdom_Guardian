  

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gdpr : MonoBehaviour
{
    private int showWin;
    public GameObject gdprWindow;

    void Start()
    {
        showWin = PlayerPrefs.GetInt("showWin");

        // MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        // {
        //     if (sdkConfiguration.ConsentDialogState == MaxSdkBase.ConsentDialogState.Applies)
        //     {
        //         if (showWin <= 0)
        //             gdprWindow.SetActive(true);
        //     }
        //     else if (sdkConfiguration.ConsentDialogState == MaxSdkBase.ConsentDialogState.DoesNotApply)
        //     {
        //         // No need to show consent dialog, proceed with initialization
        //     }
        //     else
        //     {
        //         // Consent dialog state is unknown. Proceed with initialization, but check if the consent
        //         // dialog should be shown on the next application initialization
        //     }
        // };
    }

    public void Terms()
    {
        Application.OpenURL("https://privacy.azurgames.com/#h.v7mztoso1wgw");
    }

    public void Policy()
    {
        Application.OpenURL("https://privacy.azurgames.com/#h.hn0lb3lfd0ij");
    }

    public void Accept()
    {
        PlayerPrefs.SetInt("showWin", 1);
        gdprWindow.SetActive(false);
      //  MaxSdk.SetHasUserConsent(true);
    }
}
