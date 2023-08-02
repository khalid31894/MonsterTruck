using System;
using UnityEditor.Build.Reporting;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class PlayerPrefsManager //: MonoBehaviour
{

    public static void SetHighScore(int score)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.HighScore, score);
        PlayerPrefs.Save();
    }
    // Example method to get the high score
    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.HighScore, 0);
    }


    ///////////////////////////////////////////////////////////////////
    
    public static void SetPrivacySplash(bool confirmed)
    {
        if (confirmed)
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.PrivacySplashAccept, 1);
        }
        else
        {
            PlayerPrefs.SetInt(PlayerPrefsKeys.PrivacySplashAccept, 0);
        }
    }
    public static bool GetPrivacySplash()
    {
       if(PlayerPrefs.GetInt(PlayerPrefsKeys.PrivacySplashAccept) == 1)
        {
            return true;
        }
       else { return false; }
  
    }
    




}