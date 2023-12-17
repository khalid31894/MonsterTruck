using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Device;
using Screen = UnityEngine.Screen;

public class TestingScript : MonoBehaviour
{
    [Header("Player Prefs")]
    public TextMeshProUGUI Canvas_textMeshPro;
    public TextMeshProUGUI Garage_textMeshPro;
    public TextMeshProUGUI Car_textMeshPro;
    public TextMeshProUGUI Char_textMeshPro;
    public TextMeshProUGUI Track_textMeshPro;
    [Header("Values")]
    public TextMeshProUGUI Value1;
    public TextMeshProUGUI Value2;
    public TextMeshProUGUI Value3;
    public GameObject obj;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Canvas_textMeshPro.text="Canvas: "+PlayerPrefsManager.GetCurrentCanvas().ToString();
        Garage_textMeshPro.text= "Garage: " + PlayerPrefsManager.GetCurrentGarage().ToString();
        Car_textMeshPro.text = "Car: " + PlayerPrefsManager.GetCurrentTruck().ToString();
        Char_textMeshPro.text = "Hero: " + PlayerPrefsManager.GetCurrentChar().ToString();
        Track_textMeshPro.text = "Track: " + PlayerPrefsManager.GetCurrentTrack().ToString();

        Value1.text = "Height: " + Screen.height.ToString()+" width: "+ Screen.width.ToString();
        Value2.text = "currentResolution: " + Screen.currentResolution.ToString();
        Value3.text = "mainWindowDisplayInfo: " + Screen.mainWindowDisplayInfo.ToString();

        obj.transform.position = new Vector3(Screen.width, 0, 0);
    }
}
