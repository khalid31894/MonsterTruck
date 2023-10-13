using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveAd_TurnOff : MonoBehaviour
{

    public GameObject[] turnOff;
    public void TurnOffGO()
    {
        foreach (GameObject obj in turnOff) { obj.SetActive(false); }
    }
    public void TurnOnGO()
    {
        foreach (GameObject obj in turnOff) { obj.SetActive(true); }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        TurnOffGO();
    }
    private void OnDisable()
    {
        TurnOnGO();
    }
}
