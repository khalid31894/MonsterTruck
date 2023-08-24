using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace IndieStudio.DrawingAndColoring.Logic
{
    [DisallowMultipleComponent]
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// The lines counter.
        /// </summary>
        private int linesCount;
        public ToolContent[] colors;
        [System.Obsolete]
        public void SetTexture()
        {
            var v = EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i].sprite = v.GetComponent<Tool>().contents[0].GetComponent<ToolContent>().sprite;
            }
        }

        /// <summary>
        /// The line prefab reference.
        /// </summary>
        public GameObject linePrefab;
        public List<GameObject> SelectedC, unselectedC, Tools;
        /// <summary>
        /// The stamp prefab reference.
        /// </summary>
        public GameObject stampPrefab;

        /// <summary>
        /// The current line.
        /// </summary>
        private Line currentLine;

        /// <summary>
        /// The drawing area.
        /// </summary>
        public Transform drawingArea;

        /// <summary>
        /// Whether the GameManager is interactable (listening to user input).
        /// </summary>
        public static bool interactable;

        /// <summary>
        /// Whether the pointer in draw area.
        /// </summary>
        public static bool pointerInDrawArea;
        public ParticleSystem p, SoapP;
        /// <summary>
        /// Whether the user clicked on the Area located in DrawCanvas.
        /// </summary>
        public static bool clickDownOnDrawArea;

        /// <summary>
        /// The default size of the cursor.
        /// </summary>
        private Vector3 cursorDefaultSize;

        /// <summary>
        /// The click size of the cursor .
        /// </summary>
        private Vector3 cursorClickSize;

        /// <summary>
        /// The current cursor sprite.
        /// </summary>
        [HideInInspector]
        public Sprite currentCursorSprite;

        /// <summary>
        /// The current tool.
        /// </summary>
        public Tool currentTool;

        /// <summary>
        /// The tools in the scene.
        /// </summary>
        [HideInInspector]
        public Tool[] tools;

        /// <summary>
        /// The content of the current tool (the selected content).
        /// </summary>
        [HideInInspector]
        public ToolContent currentToolContent;

        /// <summary>
        /// The current thickness.
        /// </summary>
        public ThicknessSize currentThickness;

        /// <summary>
        /// The user interface events(contains the events of the UI elements like buttons,...etc).
        /// </summary>
        public static UIEvents uiEvents;

        /// <summary>
        /// The camera that renders the drawings.
        /// </summary>
        public Camera drawCamera;
        public ShapesCanvas s;
        public GameObject LineP, StickerP, DecorationP;
        public static GameManager PaintMInstance;
        public float camScale;
        public Step4Handler[] handlers;

     
        void Awake()
        {
            Input.multiTouchEnabled = false;
            PaintMInstance = this;
            uiEvents = GameObject.FindObjectOfType<UIEvents>();
           // InstantiateDrawingContents();
        }
        [System.Obsolete]
        private void OnEnable()
        {
            if (Car_Handler.instance != null)
            {
                //Car_Handler.instance.coinsTrigger.SetActive(false);
                Car_Handler.instance.PreviousP = s;
                Car_Handler.instance.chkFlag();
                //ShapesManager.instance.shapes[ShapesManager.instance.lastSelectedShape].gamePrefab = Car_Handler.instance.gameObject;k

            }
        }
        [System.Obsolete]
        public void select()
        {
            var g = EventSystem.current.currentSelectedGameObject.transform.parent;
            for (int i = 0; i < SelectedC.Count; i++)
            {
                SelectedC[i].SetActive(false);
                unselectedC[i].SetActive(true);
            }
            g.transform.GetChild(0).gameObject.SetActive(false);
            g.transform.GetChild(1).gameObject.SetActive(true);
        }
        [System.Obsolete]
        public void selectTool()
        {
            var g = EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < Tools.Count; i++)
            {
                Tools[i].SetActive(false);
            }
            g.transform.GetChild(1).gameObject.SetActive(true);
        }
        [System.Obsolete]
        public void selectToolRemote(GameObject tool)
        {
            var g = tool;
            for (int i = 0; i < Tools.Count; i++)
            {
                Tools[i].SetActive(false);
            }
            g.transform.GetChild(1).gameObject.SetActive(true);
        }
        // Use this for initialization
        void Start()
        {

            ///Setting up the references
            tools = GameObject.FindObjectsOfType<Tool>() as Tool[];
            cursorClickSize = cursorDefaultSize / 1.2f;
            interactable = false;
            clickDownOnDrawArea = false;
            linesCount = 0;

            if (currentTool != null)
            {
                ///If the current tool is defined,set the appropriate sprites for the tools
                ///
                //print("dsadsada");
                //currentToolContent.DisableSelection();
                currentTool.EnableSelection();
                Tool.selectedContentIndex = 0;
                foreach (Tool tool in tools)
                {
                    if (tool.contents.Count != 0 && Tool.selectedContentIndex >= 0 && Tool.selectedContentIndex < tool.contents.Count && !tool.useAsCursor)
                    {
                        tool.GetComponent<Image>().color = tool.contents[Tool.selectedContentIndex].GetComponent<Image>().color;
                    }
                }
            }

            ///Load the contents of the current tool
            LoadCurrentToolContents();

            ///Load the current shape
            //LoadCurrentShape();
            SetPaintModule();
        }

        public LayerMask layer;
        void Update()
        {
            if (interactable)
            {

                if (currentTool == null)
                {
                    return;
                }

                // HandleInput();
                if (Input.GetMouseButton(0) && currentTool.feature != Tool.ToolFeature.Spunch)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    p.transform.position = mousePosition;
                }
                else if (Input.GetMouseButton(0) && currentTool.feature == Tool.ToolFeature.Spunch)
                {
                    Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    SoapP.transform.position = mousePosition;
                }
                if (currentTool.feature == Tool.ToolFeature.Line)
                {
                    RaycastHit2D hit2d = Physics2D.Raycast(GetCurrentPlatformClickPosition(drawCamera), Vector2.zero, layer);
                    //if (hit2d.collider != null && hit2d.collider.gameObject.tag == "CharacterPart")
                    //{
                    outOfRay = true;
                    UseLineFeature();
                    //}
                    //else
                    //{
                    //    currentLine = null;
                    //    outOfRay = false;
                    //}
                }
#if UNITY_EDITOR
                else if (currentTool.feature == Tool.ToolFeature.Spunch && Input.GetMouseButtonDown(0))
                {
                    if (!flag)
                    {
                        flag = true;
                        Invoke(nameof(Clean), 3);
                    }
                }
               
#else
           else if (currentTool.feature == Tool.ToolFeature.Spunch && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                print("Inside");
                if (!flag)
                {
                    flag = true;
                    Invoke(nameof(Clean), 3);
                }
            }
            //print(currentTool.feature);
           //else if (currentTool.feature == Tool.ToolFeature.Spunch && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
           // {
           //     print("out");
           //     flag = false;
           // }
#endif
            }
        }
        public Gradient Gradient;
        public Tool spunch;
        void Clean()
        {
            if (flag)
            {
                print("worked");
                Invoke(nameof(restart), 1);
                WashCar.instance.Soap_Obj();
                CleanCar(spunch);
            }
        }
        void restart()
        {
            flag = false;
        }
        public bool flag = false;
        
        void OnDestroy()
        {

            //Hide the shape's order
            if (ShapesCanvas.shapeOrder != null)
                ShapesCanvas.shapeOrder.gameObject.SetActive(false);

            
            //Hide the drawing contents
            foreach (DrawingContents dc in Area.shapesDrawingContents)
            {
                if (dc != null)
                    dc.gameObject.SetActive(false);
            }
        }
        
        private Vector3 GetCurrentPlatformClickPosition(Camera camera)
        {
            Vector3 clickPosition = Vector3.zero;

            if (Application.isMobilePlatform)
            {//current platform is mobile
                if (Input.touchCount != 0)
                {
                    Touch touch = Input.GetTouch(0);
                    clickPosition = touch.position;
                }
            }
            else
            {//others
                clickPosition = Input.mousePosition;
            }

            clickPosition = camera.ScreenToWorldPoint(clickPosition);//get click position in the world space
            clickPosition.z = 0;
            return clickPosition;
        }

        /// <summary>
        /// Uses the line feature.
        /// </summary>
        /// 
        [HideInInspector]
        public bool outOfRay = false;

        [System.Obsolete]
        private void UseLineFeature()
        {

            if (Application.isMobilePlatform)
            {//Mobile Platform
                if (Input.touchCount == 1 && outOfRay)
                {//Single Touch
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Began)
                    {
                        LineFeatureOnClickBegan();
                    }
                    if (touch.phase == TouchPhase.Moved && currentLine == null)
                    {
                        LineFeatureOnClickBegan();
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        LineFeatureOnClickReleased();
                    }
                }
                else
                {
                    currentLine = null;
                }
            }
            else
            {//Others
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 pos = Input.mousePosition;

                    LineFeatureOnClickBegan();
                }
                if (currentLine == null && outOfRay)
                {
                    LineFeatureOnClickBegan();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    LineFeatureOnClickReleased();
                }
            }

            if (currentLine != null)
            {
                //Add touch/click point into current line
                currentLine.AddPoint(GetCurrentPlatformClickPosition(drawCamera));
            }
        }

        /// <summary>
        /// Line feature on click began.
        /// </summary>
        [System.Obsolete]
        private void LineFeatureOnClickBegan()
        {
            //Create new line gameobject

            GameObject line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity) as GameObject;

            //Set the parent of line
            line.transform.SetParent(IndieStudio.DrawingAndColoring.Logic.GameManager.PaintMInstance.LineP.transform);

            //Set the name of the line
            line.name = "Line";

            //Get the Line component
            currentLine = line.GetComponent<Line>();

            //Increase linesCount by 1
            linesCount++;

            //Add the element to history
            History.Element element = new History.Element();
            element.transform = line.transform;
            element.type = History.Element.EType.Object;

            if (currentTool.repeatedTexture)
            {
                //Set the material of the line
                currentLine.SetMaterial(new Material(Shader.Find("Sprites/Default")));
                if (currentToolContent.sprite != null)
                {
                    currentLine.material.mainTexture = currentToolContent.sprite.texture;
                }

                currentLine.lineRenderer.numCapVertices = 0;
            }
            else
            {
                currentLine.SetMaterial(currentTool.drawMaterial);
            }

            //Set whether to create paint lines
            currentLine.createPaintLines = currentTool.createPaintLines;

            //Set the color of the line, if apply color flag in on
            if (currentToolContent != null && currentToolContent.applyColor)
                currentLine.SetColor(currentToolContent.gradientColor);

            if (currentThickness != null)
            {
                currentLine.SetWidth(currentThickness.value * currentTool.lineThicknessFactor, currentThickness.value * currentTool.lineThicknessFactor);
            }

            //Set the line texture mode
            currentLine.lineRenderer.textureMode = currentTool.lineTextureMode;
            currentLine.lineRenderer.sortingOrder = sortingline++;
        }
        int sortingline = 0;

        /// <summary>
        /// Line feature on click released.
        /// </summary>
        [System.Obsolete]
        private void LineFeatureOnClickReleased()
        {

            if (currentLine != null)
            {

                if (currentLine.GetPointsCount() == 0)
                {//Zero Points
                 //Destroy the line
                    Destroy(currentLine.gameObject);
                }
                else if (currentLine.GetPointsCount() == 1 || currentLine.GetPointsCount() == 2)
                {//One or Two Points
                    if (!currentTool.roundedEdges)
                    {
                        //Destroy the line
                        Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].currentSortingOrder--;
                        Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].GetComponent<History>().RemoveLastElement();
                        Destroy(currentLine.gameObject);
                    }
                    else
                    {
                        //Make the line as dot
                        currentLine.lineRenderer.SetVertexCount(2);
                        currentLine.lineRenderer.SetPosition(0, currentLine.points[0]);
                        currentLine.lineRenderer.SetPosition(1, currentLine.points[0] - new Vector3(0.015f, 0.015f, 0));
                    }
                }

                //Destroy Line component
                Destroy(currentLine);
            }

            //Release current line
            currentLine = null;
        }

        [System.Obsolete]
        public void SetCursorDefaultSize()
        {
            // cursor.transform.localScale = cursorDefaultSize;
        }


        [System.Obsolete]
        private void InstantiateDrawingContents()
        {

            //if (Area.shapesDrawingContents.Count == 0 /*&& ShapesManager.instance.shapes.Count != 0*/)
            //{
            //    foreach (ShapesManager.Shape s in ShapesManager.instance.shapes)
            //    {
            //        if (s == null)
            //        {
            //            continue;
            //        }
            //        GameObject drawingContents = new GameObject(s.gamePrefab.name + " Contents");
            //        drawingContents.layer = LayerMask.NameToLayer("MiddleCamera");
            //        DrawingContents drawingContentsComponent = drawingContents.AddComponent(typeof(DrawingContents)) as DrawingContents;
            //        drawingContents.AddComponent(typeof(History));
            //        drawingContents.transform.SetParent(drawingArea);
            //        drawingContents.transform.position = Vector3.zero;
            //        drawingContents.AddComponent<RectTransform>().anchoredPosition3D = Vector3.zero;
            //        drawingContents.transform.localScale = Vector3.one;
            //        drawingContents.SetActive(false);

            //        Transform shapeParts = s.gamePrefab.transform.Find("Parts");
            //        if (shapeParts != null)
            //        {
            //            foreach (Transform part in shapeParts)
            //            {
            //                if (part.GetComponent<ShapePart>() != null && part.GetComponent<SpriteRenderer>() != null)
            //                {
            //                    drawingContentsComponent.shapePartsColors.Add(part.name, part.GetComponent<SpriteRenderer>().color);
            //                    drawingContentsComponent.shapePartsSortingOrder.Add(part.name, part.GetComponent<SpriteRenderer>().sortingOrder);
            //                }
            //            }
            //        }

            //        Area.shapesDrawingContents.Add(drawingContentsComponent);
            //    }
            //}
        }

        public void LoadCurrentToolContents()
        {

            if (currentTool == null)
            {
                Debug.Log("Current tool is undefined");
                return;
            }

            ///Show the contents
            for (int i = 0; i < currentTool.contents.Count; i++)
            {
                if (currentTool.contents[i] == null)
                {
                    continue;
                }

                if (currentTool.contents[i].GetComponent<ToolContent>() == null)
                {
                    continue;
                }

                currentTool.contents[i].gameObject.SetActive(true);

                ToolContent toolContent = currentTool.contents[i].GetComponent<ToolContent>();

                if (currentTool.enableContentsShadow)
                {
                    if (currentTool.contents[i].GetComponent<Shadow>() != null)
                        currentTool.contents[i].GetComponent<Shadow>().enabled = true;
                }
                else
                {
                    if (currentTool.contents[i].GetComponent<Shadow>() != null)
                        currentTool.contents[i].GetComponent<Shadow>().enabled = false;
                }

                if (Tool.selectedContentIndex == i)
                {
                    toolContent.EnableSelection();
                    if (!currentTool.useAsCursor)
                        currentCursorSprite = currentTool.contents[i].GetComponent<Image>().sprite;
                    currentToolContent = toolContent;
                }

            }
        }

        /// <summary>
        /// Select the content of the current tool.
        /// </summary>
        /// <param name="content">Content.</param>
        [System.Obsolete]
        public void SelectToolContent(ToolContent content)
        {

            if (content == null)
            {
                return;
            }

            currentToolContent.DisableSelection();
            currentToolContent = content;

            for (int i = 0; i < currentTool.contents.Count; i++)
            {
                if (currentTool.contents[i] == null)
                {
                    continue;
                }

                if (currentTool.contents[i].name == content.transform.name)
                {
                    Tool.selectedContentIndex = i;
                    //print("ChangeColor");
                    foreach (Tool tool in tools)
                    {
                        if (tool.contents.Count != 0 && (i >= 0 && i < tool.contents.Count))
                        {
                            if (tool.contents[i] != null)
                            {
                                tool.GetComponent<Image>().color = tool.contents[Tool.selectedContentIndex].GetComponent<Image>().color;
                            }
                        }
                    }
                    break;
                }
            }
            SetShapeOrderColor();
            content.EnableSelection();
        }


        /// <summary>
        /// Set the shape order.
        /// </summary>
        [System.Obsolete]
        public void SetShapeOrder()
        {

            if (ShapesManager.instance.shapes == null || ShapesCanvas.shapeOrder == null)
            {
                return;
            }

            ShapesCanvas.shapeOrder.text = (ShapesManager.instance.lastSelectedShape + 1) + "/" + ShapesManager.instance.shapes.Count;
        }

        /// <summary>
        /// Set the color of the shape order.
        /// </summary>
        [System.Obsolete]
        public void SetShapeOrderColor()
        {

            if (ShapesCanvas.shapeOrder == null)
            {
                return;
            }

            if (currentToolContent != null)
            {
                ShapesCanvas.shapeOrder.color = currentToolContent.gradientColor.colorKeys[0].color;
            }
        }

        /// <summary>
        /// Load the current shape.
        /// </summary>
        [System.Obsolete]
        public void LoadCurrentShape()
        {

            //if (ShapesManager.instance.shapes == null)
            //{
            //    return;
            //}

            //if (!(ShapesManager.instance.lastSelectedShape >= 0 && ShapesManager.instance.lastSelectedShape < ShapesManager.instance.shapes.Count))
            //{
            //    return;
            //}

            //SetShapeOrder();
            //SetShapeOrderColor();
            //ShapesManager.instance.shapes[ShapesManager.instance.lastSelectedShape].gamePrefab.SetActive(true);
            //ShapesManager.instance.shapes[ShapesManager.instance.lastSelectedShape].gamePrefab.transform.localPosition = new Vector3(0, -75.0f, 0);
            //Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].gameObject.SetActive(true);
            //Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].GetComponent<History>().CheckUnDoRedoButtonsStatus(); // waqas
        }

        [System.Obsolete]
        public void HideToolContents(Tool tool)
        {
            if (tool == null)
            {
                return;
            }

            foreach (Transform content in tool.contents)
            {
                if (content != null)
                    content.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Show the tool contents.
        /// </summary>
        /// <param name="tool">Tool.</param>
        [System.Obsolete]
        public void ShowToolContents(Tool tool)
        {
            if (tool == null)
            {
                return;
            }

            foreach (Transform content in tool.contents)
            {
                if (content != null)
                    content.gameObject.SetActive(true);
            }
        }

        /// <summary>
        /// Clean the screen.
        /// </summary>
        [System.Obsolete]
        public void CleanCurrentShapeScreen()
        {

            if (Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape] == null)
            {
                return;
            }

            //Clean the history for the current shape
            Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].GetComponent<History>().CleanPool();

            //Remove all the childern in drawContents
            foreach (Transform child in Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].transform)
            {
                Destroy(child.gameObject);
            }

            Transform shapeParts = ShapesManager.instance.shapes[ShapesManager.instance.lastSelectedShape].gamePrefab.transform.Find("Parts");
            if (shapeParts != null)
            {
                foreach (Transform part in shapeParts)
                {
                    part.GetComponent<SpriteRenderer>().color = Color.white;
                    Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].shapePartsColors[part.name] = Color.white;
                    part.GetComponent<ShapePart>().ApplyInitialSortingOrder();
                    part.GetComponent<ShapePart>().ApplyInitialColor();
                    Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].shapePartsSortingOrder[part.name] = part.GetComponent<ShapePart>().initialSortingOrder;
                }
            }

            linesCount = 0;
            Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].currentSortingOrder = 0;
            Area.shapesDrawingContents[ShapesManager.instance.lastSelectedShape].lastPartSortingOrder = 0;
        }


        /// <summary>
        /// Clean the shapes.
        /// </summary>
        [System.Obsolete]
        public void CleanShapes()
        {
            Debug.Log("clean");
            for (int i = 0; i < ShapesManager.instance.shapes.Count; i++)
            {
                //Clean the history for the current shape
                Area.shapesDrawingContents[i].GetComponent<History>().CleanPool();

                //Remove all the childern in drawContents
                foreach (Transform child in Area.shapesDrawingContents[i].transform)
                {
                    Debug.Log(Area.shapesDrawingContents[i].gameObject.name);
                    Debug.Log(child.gameObject.name);
                    Destroy(child.gameObject);
                }

                Transform shapeParts = ShapesManager.instance.shapes[i].gamePrefab.transform.Find("Parts");
                if (shapeParts != null)
                {
                    foreach (Transform part in shapeParts)
                    {
                        part.GetComponent<SpriteRenderer>().color = Color.white;
                        Area.shapesDrawingContents[i].shapePartsColors[part.name] = Color.white;
                        part.GetComponent<ShapePart>().ApplyInitialSortingOrder();
                        part.GetComponent<ShapePart>().ApplyInitialColor();
                        Area.shapesDrawingContents[i].shapePartsSortingOrder[part.name] = part.GetComponent<ShapePart>().initialSortingOrder;
                    }
                }

                linesCount = 0;
                Area.shapesDrawingContents[i].currentSortingOrder = 0;
                Area.shapesDrawingContents[i].lastPartSortingOrder = 0;
            }
        }


        public void CleanCar(Tool Tool)
        {
            Debug.Log("Clean the Car");
            currentTool.DisableSelection();
            Tool.EnableSelection();
            StartCoroutine(del_Car_CleanObject());
            linesCount = 0;
        }

        IEnumerator del_Car_CleanObject()
        {
            yield return new WaitForSeconds(1);
            foreach (Transform child in LineP.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in StickerP.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in DecorationP.transform)
            {
                Destroy(child.gameObject);
            }
        }



        //create a renderer texture at run time

        //[HideInInspector]
        public Transform body;
        //[HideInInspector]
        public Sprite carBodySprite, strokeSprite;
        public RectTransform paintCanvasRect;
        public Image carBody;
        //[HideInInspector]
        public float paintAreaWidth, paintAreaHeight;
        public Camera mCamera;
        public RawImage viewPort;
        public Image stroke;





        public void FindCarBody()
        {
            if (body == null)
            {
                body = Car_Handler.instance.rawData.transform;
                var RawRef = Car_Handler.instance.rawData;
                carBody = RawRef.body;
                viewPort = RawRef.raw;
                mCamera = RawRef.renderCam;
                LineP = RawRef.LineP;
                StickerP = RawRef.StickerP;
                DecorationP = RawRef.DecorationP;
                camScale = RawRef.camScale;
            }
            if (body != null)
            {
                if (carBodySprite == null)
                {
                    if (body.TryGetComponent(out Image ImageSprite))
                    {
                        carBodySprite = ImageSprite.sprite;
                    }
                }
            }
        }
        public void SetPaintArea()
        {
            if (carBodySprite)
            {
                if (carBody)
                {
                    carBody.sprite = carBodySprite;

                    carBody.SetNativeSize();

                    if (carBody.TryGetComponent(out RectTransform rectTransform))
                    {
                        paintAreaWidth = rectTransform.sizeDelta.x;
                        paintAreaHeight = rectTransform.sizeDelta.y;
                    }
                }
            }
        }
        public void SetCameraResolution()
        {
            if (mCamera)
            {
                if (mCamera.targetTexture != null) mCamera.targetTexture.Release();

                if (paintAreaWidth > 0 && paintAreaHeight > 0)
                {
                    mCamera.targetTexture = new RenderTexture((int)paintAreaWidth, (int)paintAreaHeight, 24);


                    //float ppu = carBodySprite.pixelsPerUnit;
                    //float height = carBodySprite.rect.height / ppu;
                    //float size = height;
                    mCamera.transform.localPosition = new Vector3(0, 0, -1);
                    mCamera.transform.localScale = new Vector3(1, 1, 1);
                    mCamera.orthographicSize = camScale;
                }

                if (mCamera.targetTexture != null)
                {
                    viewPort.texture = mCamera.targetTexture;
                    viewPort.SetNativeSize();
                }
            }
        }

        public void SetPaintModule()
        {
            FindCarBody();
            SetPaintArea();
            SetCameraResolution();
        }
    }

}