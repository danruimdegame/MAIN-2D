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
    public float verticalSpeed = 5f;
	public float jumpSpeed = 180f;

	Rigidbody2D rb;
	SpriteRenderer sr;
	Animator anim;
	bool isJumping = false;
    bool isRunning = false;

	public Transform feet;
	public Transform rightShoot;
	public Transform leftShoot;
	public float feetWidth = 0.58f;
	public float feetHeight = 0.1f;

	//shooting mechanichs
	public float delayForShoot = 0.005f;
	float shootTime = 0f;

	public GameObject rightShootPrefab;
	public GameObject leftShootPrefab;

	//

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
		
		if (shootTime < delayForShoot){
			shootTime += Time.deltaTime;
		}

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

		if (Input.GetButtonDown("Fire1")){
			Shoot();
		}
	}

	void Shoot (){
		if(delayForShoot > shootTime){
			return;
		}

        //jumpshoot
        if(isJumping == true)
        {
            anim.SetInteger("State", 5);
        }
        //runshoot
        else if(isRunning == true)
        {
            anim.SetInteger("State", 4);
        }
        else
        {
            anim.SetInteger("State", 3);
        }

		shootTime = 0f;


		if(sr.flipX){
			SFXM.instance.PlayShotSound(leftShoot.gameObject);
			Instantiate(leftShootPrefab, leftShoot.position, Quaternion.identity);
		}
		else{
		SFXM.instance.PlayShotSound(rightShoot.gameObject);
		Instantiate(rightShootPrefab, rightShoot.position, Quaternion.identity);
		}

	}

	void MoveHorizontal(float speed){
        isRunning = true;
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
        isRunning = false;
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



 
//Trigger Collider Interaction
	void OnTriggerEnter2D(Collider2D other){
		switch (other.gameObject.tag){

			case "Enemies":
			if (!invulnerable) {
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
