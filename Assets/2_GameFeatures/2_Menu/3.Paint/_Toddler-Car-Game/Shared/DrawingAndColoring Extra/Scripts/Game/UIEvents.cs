using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;


///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.DrawingAndColoring.Logic
{
	/// <summary>
	/// User interface events for (buttons,sliders,...etc).
	/// </summary>
	[DisallowMultipleComponent]
	public class UIEvents : MonoBehaviour
	{
		public void ResetZoom(){
			CameraZoom.ResetZoom ();
		}

		public void PointerButtonEvent(Pointer pointer){
			if (pointer == null) {
				return;
			}
			if (pointer.group != null) {
				ScrollSlider scrollSlider = GameObject.FindObjectOfType (typeof(ScrollSlider)) as ScrollSlider;
				if (scrollSlider != null) {
					scrollSlider.DisableCurrentPointer ();
					FindObjectOfType<ScrollSlider> ().currentGroupIndex = pointer.group.Index;
					scrollSlider.GoToCurrentGroup ();
				}
			}
		}

		public void PrintClickEvent(){
			GameObject.FindObjectOfType<WebPrint> ().PrintScreen ();
		}

		public void UndoClickEvent ()
		{
			History history = GameObject.FindObjectOfType<History> ();
			if (history != null) {
				history.UnDo ();
			}
		}

		public void RedoClickEvent ()
		{
			History history = GameObject.FindObjectOfType<History> ();
			if (history != null) {
				GameManager.interactable = false;
				history.Redo ();
			}
		}

		public void AlbumShapeEvent (TableShape tableShape)
		{
			if (tableShape == null) {
				return;
			}

			TableShape.selectedShape = tableShape;
			//LoadGameScene ();
		}

		public void ThicknessSizeEvent (ThicknessSize thicknessSize)
		{
			if (thicknessSize == null) {
				return;
			}

			GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();

			if (gameManager.currentTool == null) {
				return;
			}

			if (!(gameManager.currentTool.feature == Tool.ToolFeature.Line)) {
				return;
			}

			gameManager.currentThickness = thicknessSize;
			//gameManager.ChangeThicknessSizeColor ();
		}

        //public void ShowTrashConfirmDialog ()
        //{
        //	DisableGameManager ();
        //	GameObject.Find ("TrashConfirmDialog").GetComponent<ConfirmDialog> ().Show ();
        //}

        //public void TrashConfirmDialogEvent (GameObject value)
        //{
        //	if (value == null) {
        //		return;
        //	}

        //	if (value.name.Equals ("YesButton")) {
        //		Debug.Log ("Trash Confirm Dialog : Yes button clicked");
        //		GameObject.FindObjectOfType<GameManager> ().CleanCurrentShapeScreen ();

        //	} else if (value.name.Equals ("NoButton")) {
        //		Debug.Log ("Trash Confirm Dialog : No button clicked");
        //	}
        //	value.GetComponentInParent<ConfirmDialog> ().Hide ();
        //	EnableGameManager ();
        //	//AdsManager.instance.ShowAdvertisment (AdPackage.AdEvent.Event.ON_LOAD_GAME_SCENE);
        //}

       // [System.Obsolete]
        public void ToolClickEvent (Tool tool)
		{
			if (tool == null) {
				return;
			}
           // SoundManager.instance.PlayEffect_Instance(1);
            GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
			
			if (tool.useAsToolContent) {//like an eraser
				gameManager.currentToolContent = tool.GetComponent<ToolContent> ();
			}
			
			if (tool.useAsCursor) {
				//Set the tool as cursor
				gameManager.currentCursorSprite = tool.GetComponent<Image> ().sprite;
			}
			
			gameManager.currentTool.DisableSelection ();

			tool.EnableSelection ();
			gameManager.HideToolContents (gameManager.currentTool);
			gameManager.currentTool = tool;
			gameManager.LoadCurrentToolContents ();

			if (tool.contents.Count != 0) {
				//Select current content of the tool
				if(Tool.selectedContentIndex >=0 && Tool.selectedContentIndex < tool.contents.Count)
					gameManager.SelectToolContent (tool.contents [Tool.selectedContentIndex].GetComponent<ToolContent>());
                else
                    gameManager.SelectToolContent(tool.contents[tool.contents.Count - 1].GetComponent<ToolContent>());
            }

			if (tool.feature == Tool.ToolFeature.Hand) {
				CameraDrag.isRunning = true;
			} else {
				CameraDrag.isRunning = false;
			}
		}

        //[System.Obsolete]
        public void ToolContentClickEvent (ToolContent content)
		{
			//SoundManager.instance.PlayEffect_Instance(1);
			if (content == null) {
				return;
			}
			GameManager gameManager = GameManager.PaintMInstance;
            ParticleSystem.MainModule mainModule = gameManager.p.main;
            mainModule.startColor = new ParticleSystem.MinMaxGradient(content.gradientColor);
            if (gameManager.currentTool.feature==Tool.ToolFeature.Spunch)
			{
                ToolClickEvent(GameManager.PaintMInstance.currentThickness.GetComponent<Tool>());
				GameManager.PaintMInstance.selectToolRemote(GameManager.PaintMInstance.currentThickness.GetComponent<Tool>().gameObject);	
			}
			gameManager.SelectToolContent (content);
        }

		//public void NextButtonClickEvent ()
		//{
		//	GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		//	gameManager.NextShape ();
		//}

		//public void PreviousButtonClickEvent ()
		//{
		//	GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
		//	gameManager.PreviousShape ();
		//}

		public void OnPointerEnterDrawArea(){
			GameManager.pointerInDrawArea = true;
		}

		public void OnPointerExitDrawArea(){
			GameManager.pointerInDrawArea = false;
		}

		public void DisableGameManager ()
		{
			if (!GameManager.clickDownOnDrawArea) {
				GameManager.interactable = false;
			}
		}
		
		public void EnableGameManager ()
		{
			GameManager.interactable = true;
		}

		public void OnDrawAreaClickDown()
		{
			if (GameManager.PaintMInstance.currentTool.name == "Pencil")
			{
				//SoundManager.instance.PlayEffect_Loop(2);
			}
			else if (GameManager.PaintMInstance.currentTool.name == "Brush")
			{
				//SoundManager.instance.PlayEffect_Loop(3);
			}
			else if (GameManager.PaintMInstance.currentTool.name == "TexturePaint")
			{
				//SoundManager.instance.PlayEffect_Loop(4);
			}
			else if (GameManager.PaintMInstance.currentTool.name == "Spunch")
			{
				//SoundManager.instance.PlayEffect_Loop(58);
				//SoundManager.instance.PlayEffect_Loop(59);
			}
			
			GameManager.clickDownOnDrawArea = true;
		}

		public void OnDrawAreaClickUp ()
		{
			//print("adasda");
            if (GameManager.PaintMInstance.currentTool.name == "Pencil")
            {
               // SoundManager.instance.StopEffect(2);
            }
            else if (GameManager.PaintMInstance.currentTool.name == "Brush")
            {
                //SoundManager.instance.StopEffect(3);
            }
			else if (GameManager.PaintMInstance.currentTool.name == "TexturePaint")
            {
                //SoundManager.instance.StopEffect(4);
            }
			else if (GameManager.PaintMInstance.currentTool.name == "Spunch")
            {
                //SoundManager.instance.StopEffect(58);
               // SoundManager.instance.StopEffect(59);
            }
            GameManager.clickDownOnDrawArea = false;
		}

        //public void ChangeCursorToArrow ()
        //{
        //	GameManager gameManager = GameObject.FindObjectOfType<GameManager> ();
        //	if (gameManager != null)
        //		gameManager.ChangeCursorToArrow ();
        //}

        //public void ChangeCursorToCurrentSprite()
        //{
        //	GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        //	if (gameManager != null)
        //	{

        //		gameManager.ChangeCursorToCurrentSprite();
        //	}
        //}

        //[System.Obsolete]
        public void LoadAlbumScene ()
		{
			//CarController.ReadyForAd = true;
            //Firebase.Analytics.FirebaseAnalytics.LogEvent(SceneManager.GetActiveScene().name + "_Completed");
           // Firebase.Analytics.FirebaseAnalytics.LogEvent("Car_Garage" + "_IsLoading");
            if (Car_Handler.instance)
			{
				Destroy(Car_Handler.instance.gameObject);
			}
			StartCoroutine (LoadSceneAsync("Car_Garage"));
		}

        [System.Obsolete]
        public void LoadGameScene ()
		{
			StartCoroutine (LoadSceneAsync("Game"));
		}

		public void LeaveApp(){
			Application.Quit ();
		}

        [System.Obsolete]
        IEnumerator LoadSceneAsync (string sceneName)
		{
			if (!string.IsNullOrEmpty (sceneName)) {
				#if UNITY_PRO_LICENSE
				//Show loading panel here
				AsyncOperation async = Application.LoadLevelAsync (sceneName);
				while (!async.isDone) {
					yield return 0;
				}
				#else
				Application.LoadLevel (sceneName);
				yield return 0;
				#endif
			}
		}
	}
}
