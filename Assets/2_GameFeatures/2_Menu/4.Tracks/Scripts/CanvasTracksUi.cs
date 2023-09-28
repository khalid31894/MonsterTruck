
using UnityEngine;

public class CanvasTracksUi:MonoBehaviour
{
    public void Home_Btn()
    {
        SoundManager.instance.PlayEffect_Instance(1);


       // PlayerPrefsManager.SetCurrentCanvas(0);
        Destroy(Car_Handler.instance.transform.gameObject);

        CanvasController.Instance.ChangeCanvas(0);
    }
    public void TrackSelect_Btn(int trackNum)
    {
        SoundManager.instance.PlayEffect_Instance(1);

        PlayerPrefsManager.SetCurrentTrack(trackNum);
        SceneLoader.LoadScene(SceneLoader.Scenes.Scene4_Game);
    }


    private void OnEnable()
    {
        //IntitializeAdmobAds_CB._instance.ShowAdmobInterstialAd();
    }
   
}
 