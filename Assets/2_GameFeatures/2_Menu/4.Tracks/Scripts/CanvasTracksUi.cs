
using UnityEngine;

public class CanvasTracksUi:MonoBehaviour
{
    public void ChangeToGameScene()
    {
        SoundManager.instance.PlayEffect_Instance(1);

        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
    public void TrackSelect_Btn(int trackNum)
    {
        SoundManager.instance.PlayEffect_Instance(1);

        PlayerPrefsManager.SetCurrentTrack(trackNum);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }
}
