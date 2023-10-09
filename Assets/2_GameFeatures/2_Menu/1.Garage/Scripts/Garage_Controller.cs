using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class Garage_Controller : MonoBehaviour
{
    public int currentGarage;
    public GameObject[] garages;
    public GarageSlider[] sliderArray;
    public Garage_CarScroll[] carScrollArray;

    public GameObject next_btn,prev_btn;

    public Transform garageParent;
    public float TransitionTime=2f;
    private float gargeParent_pos=0f;


    private void Awake()
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
        UpdateBtn();
        currentGarage = PlayerPrefsManager.GetCurrentGarage(); 
       foreach (GameObject garage in garages)                          //turn off all garges
        {
            garage.SetActive(false);                                   //turn on Current garage (last opened garage in current session)
        }
        garages[currentGarage].gameObject.SetActive(true);

        Invoke(nameof(openFirstGarage), CanvasLoading.delay);
    }
    public void openFirstGarage() {
        sliderArray[0].transform.localPosition = new Vector3(-0.4f, -9, 0);
        sliderArray[0].OpenGarage();
        carScrollArray[currentGarage].ScrollCars();

    }
    private void OnDisable()
    {
        UpdateGarage();
    }


    public void UpdateGarage()
    {
        PlayerPrefsManager.SetCurrentGarage(currentGarage);
    }
    public void UpdateBtn()
    {
        if (currentGarage == garages.Length - 1) { next_btn.SetActive(false); }
        if (currentGarage == 0) { prev_btn.SetActive(false); }
        if(currentGarage > 0&& currentGarage < garages.Length - 1) { next_btn.SetActive(true); prev_btn.SetActive(true); }
    }
    public void NextGarage_Btn() {
        SoundManager.instance.PlayEffect_Instance(1);

        if (currentGarage == garages.Length-1) { Debug.Log("Reached Max Garage"); return; }
        sliderArray[currentGarage].CloseGarage();
        sliderArray[currentGarage+1].OpenGarage();
        //garages[currentGarage].SetActive(false);
        currentGarage++;
        UpdateGarage();
        UpdateBtn();
        garages[currentGarage].gameObject.SetActive(true);
        Next_CanvasTransition();



        carScrollArray[currentGarage].ScrollCars();
    }
    public void PrevGarage_Btn() {
        SoundManager.instance.PlayEffect_Instance(1);

        if (currentGarage == 0) { Debug.Log("Reached Mix Garage"); return; }
        sliderArray[currentGarage].CloseGarage();
        sliderArray[currentGarage - 1].OpenGarage();
        // garages[currentGarage].SetActive(false);
        currentGarage--;
        UpdateGarage();
        UpdateBtn();
        garages[currentGarage].gameObject.SetActive(true);
        Prev_CanvasTransition();

        carScrollArray[currentGarage].ScrollCars();
    }

    public void TruckSelect_Btn(int TruckNum)
    {
        SoundManager.instance.PlayEffect_Instance(1);

        PlayerPrefsManager.SetCurrentTruck(TruckNum);

        //CanvasController.Instance.ChangeCanvas(2);

        StartCoroutine(CharSelectTime());
    }



    public GameObject loadingPanel;
    IEnumerator CharSelectTime()
{
    SoundManager.instance.PlayEffect_Instance(1);
    loadingPanel.SetActive(true);
    yield return new WaitForSeconds(1);


    SceneLoader.LoadScene(SceneLoader.Scenes.Scene3_Paint);
    //CanvasController.Instance.ChangeCanvas(3);
}


public void Next_CanvasTransition()
    {
        gargeParent_pos -= 1920;
       garageParent.DOLocalMoveX(gargeParent_pos, TransitionTime);

    }
    public void Prev_CanvasTransition()
    {
        gargeParent_pos += 1920;
        garageParent.DOLocalMoveX(gargeParent_pos, TransitionTime);
    }




}
