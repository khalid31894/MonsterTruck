using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.DrawingAndColoring.Logic
{
    //[System.Serializable]
    //public class Characters
    //{
    //    public List<GameObject> Character;
    //}
    [DisallowMultipleComponent]
    public class ShapesCanvas : MonoBehaviour
    {
        public static ShapesCanvas instance;
        public List<Transform> Characters;
 
        public Transform shapesContainer;

 
        public static Text shapeOrder;
        public float CarScale;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
              
                InstantiateShapes();
            }
            else
            {
                //Set up the render camera of the Canvas
                Canvas canvas = instance.GetComponent<Canvas>();
                if (canvas.worldCamera == null)
                {
                    canvas.worldCamera = Camera.main;
                }

                //SetShapeOrderReference();

                Destroy(gameObject);
            }
        }

        
        public void InstantiateShapes()
        {
            if (Car_Handler.instance == null)
            {

                if (shapesContainer == null)
                {
                    Debug.LogError("Shapes Container is undefined");
                    return;
                }

               
                RectTransform rectTransform;
                Transform transform;

                string Car_name = PlayerPrefsManager.GetCurrentTruck().ToString(); 
                GameObject shape = null;
                
                if (int.Parse(Car_name) <= 8)
                {
                    shape = Instantiate(Resources.Load("Car/CakeLand_Trucks/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Car_name) > 8 && int.Parse(Car_name) <= 16)
                {
                    shape = Instantiate(Resources.Load("Car/Halloween_Trucks/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Car_name) > 16 && int.Parse(Car_name) <= 24)
                {
                    shape = Instantiate(Resources.Load("Car/Jungle_Trucks/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Car_name) > 24 && int.Parse(Car_name) <= 32)
                {
                    shape = Instantiate(Resources.Load("Car/CalifornianCoast_Truck/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Car_name) > 32 && int.Parse(Car_name) <= 40)
                {
                    shape = Instantiate(Resources.Load("Car/SnowLand_Trucks/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Car_name) > 40 && int.Parse(Car_name) <= 48)
                {
                    shape = Instantiate(Resources.Load("Car/RobotWorld_Trucks/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
               


                CarScale = shape.GetComponent<Car_Handler>().CarScale;
                shape.GetComponent<Car_Handler>().PreviousP = GetComponent<ShapesCanvas>();
             
                shape.transform.SetParent(shapesContainer);//set the parent of the shape
                rectTransform = shape.GetComponent<RectTransform>();//get RectTransform component
                transform = shape.GetComponent<Transform>();
                rectTransform.anchoredPosition3D = Vector3.zero;//reset anchor position
                

                SetDriver(shape);


                shape.SetActive(true);//disable the shape
                
            }
        }

        private void SetDriver(GameObject shape  )
        {
            shape.transform.localScale = new Vector3(CarScale, CarScale, CarScale);//reset the scale to (1,1,1)

            var p = shape.transform.GetChild(0).GetChild(0);
            if (p.name == "Driver")
            {
                //int selectedMode = PlayerPrefs.GetInt("SelectedGarage");
                //int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
                int selectedCharacter = PlayerPrefsManager.GetCurrentChar();
               
                // var c = Instantiate(Characters[selectedCharacter].Character[selectedMode], p);
                var c = Instantiate(Characters[selectedCharacter-1], p);
                c.gameObject.AddComponent<AnimatorHandler>();
            }

        }

        public void SetP(GameObject shape)
        {
            foreach (Rigidbody2D item in Car_Handler.instance.rigidComponent)
            {
                item.bodyType = RigidbodyType2D.Dynamic;
            }
            if (Car_Handler.instance.rigidComponent.Length > 3&& Car_Handler.instance.rigidComponent[0].transform.parent.GetComponent<SpriteRenderer>()!=null)
            {
                Car_Handler.instance.rigidComponent[0].transform.parent.GetComponent<SpriteRenderer>().sortingLayerName = "Default";
                Car_Handler.instance.rigidComponent[0].transform.parent.GetChild(0).GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
            for (int i = 0; i < Car_Handler.instance.rigidComponent.Length - 1; i++)
            {
                Car_Handler.instance.rigidComponent[i].GetComponent<SpriteRenderer>().sortingLayerName = "Default";
            }
            shape.transform.SetParent(shapesContainer);//set the parent of the shape
            RectTransform rectTransform = shape.GetComponent<RectTransform>();//get RectTransform component
            Transform transform = shape.GetComponent<Transform>();
            rectTransform.anchoredPosition3D = Vector3.zero;//reset anchor position
            rectTransform.anchoredPosition = new Vector2(0, -75f);
            shape.transform.GetChild(0).transform.localPosition= Vector3.zero;
            shape.transform.GetChild(0).transform.localRotation= Quaternion.identity;
            shape.GetComponent<Car_Handler>().rawData.renderCam.clearFlags = CameraClearFlags.Skybox;
            shapes = shape;
 
            AnimatorHandler.Instance.idle = true;

            StartCoroutine(renewRigid());

        }
        GameObject shapes;
        IEnumerator  renewRigid()
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < Car_Handler.instance.rigidComponent.Length - 1; i++)
            {
                Car_Handler.instance.rigidComponent[i].bodyType= RigidbodyType2D.Dynamic;
            }
            yield return new WaitForSeconds(0.2f);
            Car_Handler.instance.rigidComponent[2].bodyType = RigidbodyType2D.Dynamic;
            Car_Handler.instance.addParticleRigid();
            Car_Handler.instance.Car.SetActive(true);
        }
    }
    
}
