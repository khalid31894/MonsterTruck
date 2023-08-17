using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenuUi : MonoBehaviour
{
    [Header("Panel Reff")]
    [SerializeField] GameObject privacyPanel;
    public void Play_Btn()
    {
        CanvasController.Instance.ChangeCanvas(1);
    }
    public void Privacy_Btn()
    {
        privacyPanel.SetActive(true);
    }

}
