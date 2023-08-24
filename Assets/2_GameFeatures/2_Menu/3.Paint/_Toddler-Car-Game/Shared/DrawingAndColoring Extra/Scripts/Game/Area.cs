using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.DrawingAndColoring.Logic
{
	[DisallowMultipleComponent]
	public class Area : MonoBehaviour, IPointerUpHandler
	{

		/// <summary>
		/// The shapes drawing contents.
		/// </summary>
		public static List<DrawingContents> shapesDrawingContents = new List<DrawingContents>();
		public void OnPointerUp(PointerEventData eventData)
		{
			if (GameManager.PaintMInstance.currentTool.feature == Tool.ToolFeature.Spunch && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				print("out");
                GameManager.PaintMInstance.flag = false;
			}
		}
	}
}
