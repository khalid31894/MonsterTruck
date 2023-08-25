using UnityEngine;
using System.Collections;
using DG.Tweening;

public class StartPoint : MonoBehaviour {

    void Start()
    {

        //Instantiate(cars[PlayerPrefs.GetInt("SelectedCar")], transform.position, transform.rotation);
        Car_Handler.instance.gameObject.SetActive(true);
        Car_Handler.instance.Car.transform.localPosition = transform.position;
        Car_Handler.instance.controller.enabled = true;
        Car_Handler.instance.controller.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        //Car_Handler.instance.Car.transform.GetChild(0).GetComponent<CarControllerNew>().enabled = true;
    }

    public void Reset_Rotation()
    {
        Car_Handler.instance.Car.transform.GetChild(0).DOLocalRotate(new Vector3(0, 0, 0), 0);
        Debug.Log(Car_Handler.instance.Car.name);
    }
    private void Awake()
    {
        Car_Handler.instance.gameObject.SetActive(true);
    }
}
