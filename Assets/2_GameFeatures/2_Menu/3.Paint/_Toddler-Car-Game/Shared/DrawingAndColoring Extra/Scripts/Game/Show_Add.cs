using UnityEngine;

public class Show_Add : MonoBehaviour
{
    public bool show = false;
    public bool dis_Show = false;

    [System.Obsolete]
    private void OnEnable()
    {
        if (show)
        {
           // AssignAdIds_CB.instance.ShowBanner();
        }
        else
        {
          //  AssignAdIds_CB.instance.HideBanner();
        }
    }

    [System.Obsolete]
    private void OnDisable()
    {
        if (dis_Show)
        {
          //  AssignAdIds_CB.instance.ShowBanner();
        }
        else
        {
           // AssignAdIds_CB.instance.HideBanner() ;
        }
    }
}
