using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GM : MonoBehaviour {

	public static GM instance = null;
	Player player;

	//minimus height to keep alive
	public float YMinLive = -18f;
	public Transform spawnPoint;
	public GameObject playerPrefab;
	public float timeToRespawn = 2f;

	public UI ui;

	GD data = new GD();

	void Awake(){
		if (instance == null){
			instance = this;
		}
	}

	// Use this for initialization
	void Start () {
		if (player == null){
			RespawnPlayer();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null){
			GameObject obj = GameObject.FindGameObjectWithTag("Player");
			if (obj != null){
				player = obj.GetComponent<Player>();
			}
		}

		DisplayHUDData();
	}
	//Scene Management
	public void RestartLevel(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
	public void ExitToMainMenu(){
		LoadScene(0);
	}
	public void CloseApp(){
		Application.Quit();
	}
	public void LoadScene(int sceneNumber){
		SceneManager.LoadScene(sceneNumber);
	}
	// 

	void DisplayHUDData(){
		//Xp bar
		if (data.XpCount <= 50){
		ui.hud.XPBar.value = data.XpCount;
		}
		else if (data.XpCount > 50){
		ui.hud.XPBar.value = data.XpCount - 50;
		}

		//HP bar
		if (data.HpCount > 100){
			ui.hud.HPBar.maxValue = data.HpCount;
			ui.hud.HPBar.value = data.HpCount;
		}
		else{
			ui.hud.HPBar.value = data.HpCount;
		}


		//Life Count
		ui.hud.LifeCount.text = data.lifeCount.ToString("F0");
	}

	//XP Interaction
	public void IncrementXpCount(){
		data.XpCount++;
	}
	public void IncrementXpCount2(){
		data.XpCount = data.XpCount + 5;
	}
	public void IncrementXpCount3(){
		data.XpCount = data.XpCount + 20;
	}

	//Lives and HP Interaction
	public void DecrementLives(){
		data.lifeCount --;
	}

	void GameOver(){
		ui.hud.HUDPanel.SetActive(false);
		ui.gameOver.gameOverPanel.SetActive(true);
	}

	public void RespawnPlayer (){
		Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
		player.transform.parent = null;
	}

	public void KillBill(){
		if (player != null){
			Destroy(player.gameObject);
			DecrementLives();
			if(data.lifeCount > 0){
			Invoke("RespawnPlayer", timeToRespawn);
			}
			else {
			GameOver();
			}
		}
	}
}
