﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

///Developed by Indie Studio
///https://assetstore.unity.com/publishers/9268
///www.indiestd.com
///info@indiestd.com

namespace IndieStudio.DrawingAndColoring.Logic
{
	public class ShapesManager : MonoBehaviour
	{
			/// <summary>
			/// The shapes list.
			/// </summary>
			public List<Shape> shapes = new List<Shape> ();

			/// <summary>
			/// The last selected shape.
			/// </summary>
			[HideInInspector]
			public int lastSelectedShape;

			/// <summary>
			/// The instance of this class.
			/// </summary>
			public static ShapesManager instance;
			void Awake ()
			{
				if (instance == null) {
					instance = this;
					//DontDestroyOnLoad (gameObject);
				lastSelectedShape = PlayerPrefs.GetInt("SelectedCar");
				} else {
					Destroy (gameObject);
				}
			}

			[System.Serializable]
			public class Shape
			{
					public bool showContents = true;
					public GameObject gamePrefab;
			}
	}
}
