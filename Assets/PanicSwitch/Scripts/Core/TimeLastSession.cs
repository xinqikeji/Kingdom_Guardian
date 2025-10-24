  

using System;
using UnityEngine;

public class TimeLastSession : MonoBehaviour
{
   public static TimeSpan ts;

    private void Awake()
    {
        CheckTime();
    }    
    
    void CheckTime()
    {
        if (PlayerPrefs.HasKey("LastSession"))
        {
            ts = DateTime.Now - DateTime.Parse(PlayerPrefs.GetString("LastSession"));            
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        }
        
    }

    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
        }
    }

    public void SaveTime()
    {
        PlayerPrefs.SetString("LastSession", DateTime.Now.ToString());
    }
}
