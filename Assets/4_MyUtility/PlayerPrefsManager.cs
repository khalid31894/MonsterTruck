using System;
using System.Xml.Serialization;
//using UnityEditor.Build.Reporting;
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
    
    ///////////////////////////////////////////////////////////////////
    
    public static void SetCurrentCanvas(int canvasNumber)
    {
        if (canvasNumber < 0 && canvasNumber > 5) { Debug.LogError("Canvas number out of range"); return; }

        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCanvas, canvasNumber);

    }
    public static int  GetCurrentCanvas()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCanvas);
    }

    ////////////////////////////////////////////////////////////////
    

    public static  void SetCurrentGarage(int garageNumber)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentGarage, garageNumber);
    }

    public static int GetCurrentGarage()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentGarage);
    }

    //////////////////////////////////////////////////////////////////
    ///


    public static void SetCurrentChar(int charNum)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentCharacter, charNum);
    }
    public static int GetCurrentChar(){

        return PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentCharacter);
    }

    ///////////////////////////////////////////////////////////////////

    public static void SetCurrentTrack(int TrackNum)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentTrack, TrackNum);
    }
    public static int GetCurrentTrack()
    {

        return PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentTrack);
    }

    //////////////////////////////////////////////////////////////////////
    
    public static void SetCurrentTruck(int TruckNum)
    {
        PlayerPrefs.SetInt(PlayerPrefsKeys.CurrentTruck, TruckNum);
    }
    public static int GetCurrentTruck()
    {
        return PlayerPrefs.GetInt(PlayerPrefsKeys.CurrentTruck);   
    }

   /////////////////////////////////////////////////////////////////////////

}