using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENMYflyer : MonoBehaviour {
	public float speed = -2f;
	public int damage = 25;

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

	void OnTriggerEnter2D(Collider2D other){
		//player
		if (other.gameObject.layer == 14){
			GM.instance.HurtBill(damage);
		}
		//platform
		else if (other.gameObject.layer == 10){
			Flip();
		}

		if (other.gameObject.tag == "Bullet1"){
			VFXM.instance.ShowEnemyDeathParticles(transform.gameObject);
			Destroy(other.gameObject);
			Destroy(this.gameObject);
		}

	}
}
