﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.DrawingAndColoring.Logic
{
	public class ToolContent : MonoBehaviour
	{
		/// <summary>
		/// The color of the content.
		/// </summary>
		public Gradient gradientColor;

		/// <summary>
		/// The sprite reference.
		/// </summary>
		public Sprite sprite;

		/// <summary>
		/// Whether to apply the color of ToolContent e.g on the line,stamp.
		/// </summary>
		public bool applyColor = true;

		/// <summary>
		/// Enable content selection.
		/// </summary>
		public void EnableSelection() // color pic karny pa 
		{
			//GetComponent<Animator>().SetBool("RunScale",true);
		}

		/// <summary>
		/// Disable content selection.
		/// </summary>
		public void DisableSelection()
		{
			//GetComponent<Animator>().SetBool("RunScale",false);
		}
	}
}
