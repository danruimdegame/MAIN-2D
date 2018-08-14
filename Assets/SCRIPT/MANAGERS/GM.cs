using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	void DisplayHUDData(){
		//Xp bar
		if (data.XpCount <= 50){
		ui.hud.XPBar.value = data.XpCount;
		}
		else if (data.XpCount > 50){
		ui.hud.XPBar.value = data.XpCount - 50;
		}

		//Life Count
		ui.hud.LifeCount.text = data.lifeCount.ToString("F0");
	}

	public void IncrementXpCount(){
		data.XpCount++;
	}
	public void DecrementLives(){
		data.lifeCount --;
	}

	void GameOver(){
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
