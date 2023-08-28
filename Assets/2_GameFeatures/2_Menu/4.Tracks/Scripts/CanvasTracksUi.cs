using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTracksUi : MonoBehaviour
{
    public void ChangeToGameScene()
    {
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
    public void TrackSelect_Btn(int TrackNum)
    {
        PlayerPrefsManager.SetCurrentTrack(TrackNum);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
}
