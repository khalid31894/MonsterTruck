using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName ="MonsterTruck_SO", menuName ="ScriptableObjects/MonsterTruck_SO")]
public class MonsterTruck_SO : ScriptableObject
{
    public int truck_ID;
    public MyEnums.Gurage gurage;
    public Sprite sprite;
    public GameObject Prefab;
    public AudioClip engineSound;

}
