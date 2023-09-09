
using UnityEngine;

public class CanvasTracksUi:MonoBehaviour
{
    public void ChangeToGameScene()
    {
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
    public void TrackSelect_Btn(int trackNum)
    {
        PlayerPrefsManager.SetCurrentTrack(trackNum);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
}
