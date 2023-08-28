using UnityEngine;
using System.Collections;

public class SmoothFollow2D : MonoBehaviour
{

    private Vector3 velocity = Vector3.zero;
    Transform target;
    [Space(3)]
    public float startDelay = 0.03f;
    public string targetTag = "Player";

    public Vector2 position = new Vector2(0.3f, 0.5f);
    public static bool isGameOver;
    //[HideInInspector]
    public CarController carController;
    public Camera Camera;
    public float MaxOrthoSize, MinOrthoSize;
    //public static bool isStart = false;
    public static SmoothFollow2D Instance;
    IEnumerator Start()
    {

        Instance = this;
        if (Car_Handler.instance != null)
        {
            carController = Car_Handler.instance.controller;
        }
        yield return new WaitForEndOfFrame();
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
        //Camera.fieldOfView = 60;

    }
    void Update()
    {
        if (target)
        {
            Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
            Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(position.x, position.y, point.z)); //(new Vector3(0.5, 0.5, point.z));
            Vector3 destination = transform.position + delta;
            transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, 0);
            isGameOver = true;

            //if (carController.speed <= 10)
            //{
            //    if (Camera.fieldOfView >= MinOrthoSize)
            //    {
            //        Camera.fieldOfView -= 0.01f;
            //    }
            //}
            //else
            //{
            //    if (Camera.fieldOfView <= MaxOrthoSize)
            //    {
            //        Camera.fieldOfView += 0.01f;
            //    }
            //}
        }
        else
        {
            isGameOver = false;

        }

    }
}