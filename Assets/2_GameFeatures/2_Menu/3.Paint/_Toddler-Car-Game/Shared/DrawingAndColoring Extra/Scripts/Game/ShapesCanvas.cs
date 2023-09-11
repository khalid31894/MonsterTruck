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
    [System.Serializable]
    public class Characters
    {
        public List<GameObject> Character;
    }
    [DisallowMultipleComponent]
    public class ShapesCanvas : MonoBehaviour
    {
        public static ShapesCanvas instance;
        public List<Characters> Characters;
        /// <summary>
        /// The shapes container.
        /// </summary>
        public Transform shapesContainer;

        /// <summary>
        /// The shape order.
        /// </summary>
        public static Text shapeOrder;
        public float CarScale;

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                //DontDestroyOnLoad(gameObject);

                //SetShapeOrderReference();

                //Instantiate the shapes
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

        /// <summary>
        /// Set the shape order reference.
        /// </summary>
        //private static void SetShapeOrderReference()
        //{
        //    if (shapeOrder == null)
        //    {
        //        shapeOrder = GameObject.Find("ShapeOrder").GetComponent<Text>();
        //    }
        //}

        /// <summary>
        /// Instantiate the shapes.
        /// </summary>
        public void InstantiateShapes()
        {
            if (Car_Handler.instance == null)
            {

                if (shapesContainer == null)
                {
                    Debug.LogError("Shapes Container is undefined");
                    return;
                }

                //if (ShapesManager.instance.shapes.Count == 0)
                //{
                //    Debug.LogWarning("No Shapes Found in the list");
                //}

                //Destroy all shapes in the shapesContainer
                //foreach (Transform child in shapesContainer)
                //{
                //    Destroy(child.gameObject);
                //}

                RectTransform rectTransform;
                Transform transform;


                //for (int i = 0; i < ShapesManager.instance.shapes.Count; i++)
                //{
               // int i = ShapesManager.instance.lastSelectedShape;

                //if (ShapesManager.instance.shapes[i] == null)
                //{
                //    continue;
                //}
                string Selected_Car = "6"/*PlayerPrefs.GetString("Car_Resource")*/;
                string Car_name = "6"/*PlayerPrefs.GetString("Selected_Car")*/;
                GameObject shape = null;
                print(Selected_Car);
                if (int.Parse(Selected_Car) <= 10)
                {
                    shape = Instantiate(Resources.Load("Car/EveryDay/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Selected_Car) > 10 && int.Parse(Selected_Car) <= 20)
                {
                    shape = Instantiate(Resources.Load("Car/Diggers_Cars/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Selected_Car) > 20 && int.Parse(Selected_Car) <= 30)
                {
                    shape = Instantiate(Resources.Load("Car/Off_Road/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Selected_Car) > 30 && int.Parse(Selected_Car) <= 40)
                {
                    shape = Instantiate(Resources.Load("Car/Space/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Selected_Car) > 40 && int.Parse(Selected_Car) <= 50)
                {
                    shape = Instantiate(Resources.Load("Car/Super_Hero/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                else if (int.Parse(Selected_Car) > 50 && int.Parse(Selected_Car) <= 60)
                {
                    shape = Instantiate(Resources.Load("Car/Race/" + Car_name), Vector3.zero, Quaternion.identity) as GameObject;
                }
                //GameObject shape = Instantiate(ShapesManager.instance.shapes[i].gamePrefab, Vector3.zero, Quaternion.identity) as GameObject;
               

                CarScale = shape.GetComponent<Car_Handler>().CarScale;
                shape.GetComponent<Car_Handler>().PreviousP = GetComponent<ShapesCanvas>();
                //shape.name = ShapesManager.instance.shapes[i].gamePrefab.name;//set the name of the shape
                //if (shape.name == "FreeArea")
                //{//Hide Free Area image
                //    shape.GetComponent<Image>().enabled = false;
                //}
                shape.transform.SetParent(shapesContainer);//set the parent of the shape
                rectTransform = shape.GetComponent<RectTransform>();//get RectTransform component
                transform = shape.GetComponent<Transform>();
                rectTransform.anchoredPosition3D = Vector3.zero;//reset anchor position
                //rectTransform.offsetMax = rectTransform.offsetMin = Vector2.zero;//reset offset
                //shape.transform.localScale = Vector3.one;//reset the scale to (1,1,1)
                shape.transform.localScale = new Vector3(CarScale, CarScale, CarScale);//reset the scale to (1,1,1)
                var p = shape.transform.GetChild(0).GetChild(0);
                if (p.name == "Driver")
                {
                    int selectedMode = PlayerPrefs.GetInt("SelectedGarage");
                    int selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter");
                    var c = Instantiate(Characters[selectedCharacter].Character[selectedMode], p);
                    c.gameObject.AddComponent<AnimatorHandler>();
                }
                shape.SetActive(true);//disable the shape
                //ShapesManager.instance.shapes[i].gamePrefab = shape;
                //}
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
            //shape.transform.localScale = new Vector3(CarScale, CarScale, CarScale);//reset the scale to (1,1,1)
            shape.GetComponent<Car_Handler>().rawData.renderCam.clearFlags = CameraClearFlags.Skybox;
            shapes = shape;
           // shape.transform.GetChild(0).GetComponent<CarController>().forward = false;
         //   shape.transform.GetChild(0).GetComponent<CarController>().backward = false;
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
