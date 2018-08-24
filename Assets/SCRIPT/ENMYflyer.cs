﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENMYflyer : MonoBehaviour {
	public float speed = -2f;

	Rigidbody2D rb;
	SpriteRenderer sr;


	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < GM.instance.YMinLive){
			Destroy(gameObject);
		}
		Move();
		FlipCheck();
	}

	void Move(){
		rb.velocity = new Vector2(speed, rb.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer != 14){
			Flip();
		}
	}

	void FlipCheck(){
		if (speed > 0){
			sr.flipX = true; 
		}
		else if (speed < 0){
			sr.flipX = false;
		}
	}
	void Flip(){
		speed = -speed;
	}
}
