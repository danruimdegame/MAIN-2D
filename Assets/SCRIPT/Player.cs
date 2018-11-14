﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Idle - 0
Jump - 1
Run - 2
Shoot - 3
RunShoot - 4
JumpShoot - 5
ClimbShoot - 6
Climb - 7
ClimbEnd - 8
Damage - 9
 */

public class Player : MonoBehaviour {

	public float horizontalSpeed = 10f;
	public float jumpSpeed = 180f;

	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;
	bool isJumping = false;

	public Transform feet;
	public float feetWidth = 0.58f;
	public float feetHeight = 0.1f;

	public bool isGrounded;
	public LayerMask[] whaIsGround;

	bool invulnerable = false;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	void OnDrawGizmos(){
		Gizmos.DrawWireCube(feet.position, new Vector3(feetWidth, feetHeight, 0f));
	}
	
	bool TestGrounded() {
		foreach (LayerMask lm in whaIsGround) {
			if (Physics2D.OverlapBox(new Vector2(feet.position.x, feet.position.y), new Vector2(feetWidth, feetHeight), 360f, lm)) {
				return true;
			}
		}
		return false;
	}

	// Update is called once per frame
	void Update () {
		if(transform.position.y < GM.instance.YMinLive) {
			GM.instance.KillBill();
		}

		isGrounded = TestGrounded();

		float horizontalInput = Input.GetAxisRaw("Horizontal");
		float horizontalPlayerSpeed = horizontalSpeed * horizontalInput;
		if (horizontalPlayerSpeed != 0){
			MoveHorizontal(horizontalPlayerSpeed);
		}
		else {
			StopMoving();
		}

		if (Input.GetButtonDown("Jump")){
			Jump();
		}
	}

	void MoveHorizontal(float speed){
		rb.velocity = new Vector2(speed, rb.velocity.y);

		if (speed < 0f){
			sr.flipX = true;
		}else if (speed > 0f){
			sr.flipX = false;
		}

		if(!isJumping){
		anim.SetInteger("State", 2);
		}
	}

	void StopMoving(){
		rb.velocity = new Vector2(0f, rb.velocity.y);

		if(!isJumping){
		anim.SetInteger("State", 0);
		}
	}

	void Jump(){
		if (isGrounded){
		isJumping = true;
		SFXM.instance.PlayJumpSound(gameObject);
		rb.velocity = new Vector2(0, jumpSpeed);
		//rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
		anim.SetInteger("State", 1);

		}
	}

	void OnCollisionEnter2D(Collision2D other){
		//ground reaction
		if (other.gameObject.layer == 10){
			isJumping = false;
		}

		//moving platform reaction
		else if (other.gameObject.layer == 15){
			isJumping = false;
			transform.parent = other.transform;
		}
	}

//NO TOUCHING MOVING PLATFORM
	void OnCollisionExit2D(Collision2D other){
		if(other.gameObject.layer == 15){
		transform.parent = null;
		}
	}



 
//Collect XP 
	void OnTriggerEnter2D(Collider2D other){
		switch (other.gameObject.tag){

			case "Enemies":
			if (!invulnerable) {
				Debug.Log("pegou");
				EnemyData script = other.gameObject.GetComponent<EnemyData>();
				GM.instance.HurtBill(script.Damage);
			}
			break;

			case "XP":
			SFXM.instance.PlayXPPickupSound(other.gameObject);
			VFXM.instance.ShowXPParticles(other.gameObject);
			GM.instance.IncrementXpCount();
			Destroy(other.gameObject);
			break;

			case "XP2":
			SFXM.instance.PlayXPPickupSound(other.gameObject);
			VFXM.instance.ShowXPParticles(other.gameObject);
			GM.instance.IncrementXpCount2();
			Destroy(other.gameObject);
			break;

			case "XP3":
			SFXM.instance.PlayXPPickupSound(other.gameObject);
			VFXM.instance.ShowXPParticles(other.gameObject);
			GM.instance.IncrementXpCount3();
			Destroy(other.gameObject);
			break;
		}
	}

	public void PushPlayer(){
		rb.velocity = Vector2.zero;
		rb.AddForce(new Vector2(400.0f, -400.0f));
		invulnerable = true;
		Invoke("SetVulnarable", 2f);

	}

	void SetVulnarable() {
		invulnerable = false;
	}
}
