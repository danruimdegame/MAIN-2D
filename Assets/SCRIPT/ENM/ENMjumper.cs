﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENMjumper : MonoBehaviour {

	public GameObject jumper;
	public Transform currentPoint;
	public Transform[] positions;
	public int pointSelection = 0;
	public float moveSpeed = 2f;
	public float waitTime = 0.5f;

	Animator anim;

	SpriteRenderer sr;

	void Start () {
		anim = GetComponent<Animator>();
		currentPoint = positions[pointSelection];
		StartCoroutine(Move());
		sr = GetComponent<SpriteRenderer>();
	}
	
	IEnumerator Move(){
		while(true){
		jumper.transform.position = Vector3.MoveTowards(jumper.transform.position, currentPoint.position, Time.deltaTime*moveSpeed);

		if (jumper.transform.position == currentPoint.position){
			pointSelection++;
			anim.SetInteger("State", 1);
			yield return new WaitForSeconds(waitTime);
			anim.SetInteger("State", 0);
			Flip();
		}

		if(pointSelection == positions.Length){
			pointSelection = 0;
			anim.SetInteger("State", 1);
			yield return new WaitForSeconds(waitTime);
			anim.SetInteger("State", 0);
	
		}

		currentPoint = positions[pointSelection];
		yield return null;

		}
	}

	void Flip(){
		if (sr.flipX == false){
			sr.flipX = true; 
		}
		else if (sr.flipX == true){
			sr.flipX = false;
		}
	}

	void Update () {
	}
}
