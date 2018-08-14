using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class UI {

	[Serializable]
	public class HUD {

		[Header("Bars")]
		public Image HPBar;

		public Slider XPBar;

		[Header("Text")]
		public Text LifeCount;
	}

	[Serializable]
	public class GameOver {

		[Header("Other")]

		public GameObject gameOverPanel;
	}

	public HUD hud;
	public GameOver gameOver;
}
