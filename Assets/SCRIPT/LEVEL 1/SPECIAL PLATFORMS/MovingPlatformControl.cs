using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformControl : MonoBehaviour {

	public GameObject platform;
	public Transform currentPoint;
	public Transform[] positions;
	public int pointSelection = 0;
	public float moveSpeed = 2f;
	public float waitTime = 0.5f;

	void Start () {
		currentPoint = positions[pointSelection];
		StartCoroutine(Move());
	}
	
	IEnumerator Move(){
		while(true){
		platform.transform.position = Vector3.MoveTowards(platform.transform.position, currentPoint.position, Time.deltaTime*moveSpeed);

		if (platform.transform.position == currentPoint.position){
			pointSelection++;
			yield return new WaitForSeconds(waitTime);
		}

		if(pointSelection == positions.Length){
			pointSelection = 0;
			yield return new WaitForSeconds(waitTime);
		}

		currentPoint = positions[pointSelection];
		yield return null;

		}
	}

	void Update () {
		
		
	}
}
