using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public float YMin = -15.48f;
	public float XMin = -5.17f;
	private Vector2 velocity;
	public float smoothTimeY;
	public float smoothTimeX;

	Vector3 Pos;

	public GameObject player;
	void Update(){
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void FixedUpdate(){
		float posx = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
		float posy = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);

		Vector3 Pos = new Vector3(posx, posy, transform.position.z);

		if (Pos.x < XMin){
			Pos = new Vector3 (XMin, Pos.y, Pos.z);
		}
		if (Pos.y < YMin){
			Pos = new Vector3 (Pos.x, YMin, Pos.z);
		}

		 transform.position = Pos;

	}
	
}
