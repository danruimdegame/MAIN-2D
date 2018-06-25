using System.Collections;
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

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		sr = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
		isJumping = true;
		rb.velocity = new Vector2(0, jumpSpeed);
		anim.SetInteger("State", 1);
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.layer == 10){
			isJumping = false;
		}
	}
}
