using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TestingScript : MonoBehaviour
{

    public TextMeshProUGUI Canvas_textMeshPro;
    public TextMeshProUGUI Garage_textMeshPro;
    public TextMeshProUGUI Car_textMeshPro;
    public TextMeshProUGUI Char_textMeshPro;
    public TextMeshProUGUI Track_textMeshPro;


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


    }
}
