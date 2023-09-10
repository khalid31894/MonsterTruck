using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Controller : MonoBehaviour
{
    public GameObject[] trackPrefabs;
    //public int currentLv;
    private void Awake()
    {
       // currentLv = PlayerPrefsManager.GetCurrentTrack();
       Instantiate(trackPrefabs[PlayerPrefsManager.GetCurrentTrack() -1 ],Vector3.zero, Quaternion.Euler(Vector3.zero));
    }
    private void Start()
    {
        
    }
}
