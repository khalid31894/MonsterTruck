using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasMainMenuUi : MonoBehaviour
{
    [Header("Panel Reff")]
    [SerializeField] GameObject privacyPanel;


    private void OnEnable()
    {
        if (PaintSecUI.isPainted == false)
        {
        PlayerPrefsManager.SetCurrentCanvas(0);

        }
    }
  
    public void Privacy_Btn()
    {
        privacyPanel.SetActive(true);
    }

}
