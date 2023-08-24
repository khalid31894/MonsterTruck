using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class GameManager : MonoBehaviour
{
    public GameObject Race, Break,play;
    public ParticleSystem particle;
    public Button Jump;
    public SortingLayer layer;
    public static GameManager instance;
    IEnumerator Start()
    {
        Input.multiTouchEnabled = true;
        instance = this;
        if (Car_Handler.instance != null)
        {
            if (Car_Handler.instance.rigidComponent.Length > 3)
            {
                if (Car_Handler.instance.rigidComponent[0].transform.parent.GetComponent<SpriteRenderer>())
                {
                    Car_Handler.instance.rigidComponent[0].transform.parent.GetComponent<SpriteRenderer>().sortingLayerName = "Car";
                }
                Car_Handler.instance.rigidComponent[0].transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Car";
            }
            for (int i = 0; i < Car_Handler.instance.rigidComponent.Length - 1; i++)
            {
                Car_Handler.instance.rigidComponent[i].GetComponent<SpriteRenderer>().sortingLayerName = "Car";
            }
        }
        //Start Main Game   -----------------------------------------------
        yield return new WaitForEndOfFrame();   //Player is Spawned afer milisecond. we wait .3 and then find it

    }
    [System.Obsolete]
    public void BreakCar()
    {
        Car_Handler.instance.controller.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    [Obsolete]
    private void OnEnable()
    {
        if (Car_Handler.instance != null)
        {
            Car_Handler.instance.controller.IsJump = true;
            Jump.onClick.AddListener(() => Car_Handler.instance.controller.CheckJump());
        }
        }
}