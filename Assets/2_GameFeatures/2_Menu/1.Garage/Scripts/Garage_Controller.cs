using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Garage_Controller : MonoBehaviour
{
    public int currentGarage;
    public GameObject[] garages;


    private void Start()
    {
        PlayerPrefsManager.SetCurrentGarage(0);
        foreach (GameObject garage in garages)                          //turn off all garges
        {
            garage.SetActive(false);                                   //turn on Current garage (last opened garage in current session)
        }
        garages[0].SetActive(true); 
    }
    private void OnEnable()
    {
        currentGarage = PlayerPrefsManager.GetCurrentGarage(); 
       foreach (GameObject garage in garages)                          //turn off all garges
        {
            garage.SetActive(false);                                   //turn on Current garage (last opened garage in current session)
        }
        garages[currentGarage].gameObject.SetActive(true);  
    }
    private void OnDisable()
    {
        UpdateGarage();
    }


    public void UpdateGarage()
    {
        PlayerPrefsManager.SetCurrentGarage(currentGarage);
    }

    public void NextGarage_Btn() {
        SoundManager.instance.PlayEffect_Instance(1);

        if (currentGarage == garages.Length-1) { Debug.Log("Reached Max Garage"); return; }
        garages[currentGarage].SetActive(false);
        currentGarage++;
        UpdateGarage();
        garages[currentGarage].gameObject.SetActive(true);
        
    }
    public void PrevGarage_Btn() {
        SoundManager.instance.PlayEffect_Instance(1);

        if (currentGarage == 0) { Debug.Log("Reached Mix Garage"); return; }
        garages[currentGarage].SetActive(false);
        currentGarage--;
        UpdateGarage();
        garages[currentGarage].gameObject.SetActive(true);

    }

    public void TruckSelect_Btn(int TruckNum)
    {
        SoundManager.instance.PlayEffect_Instance(1);

        PlayerPrefsManager.SetCurrentTruck(TruckNum);
        CanvasController.Instance.ChangeCanvas(2);
    }


    
 

}
