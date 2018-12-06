using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXM : MonoBehaviour {
	public static VFXM instance;

	public GameObject SmallXPParticles;
	public GameObject EnemyDeathParticles;

	void Awake(){
		if (instance == null){
			instance = this;
		}
	}

	public void ShowXPParticles(GameObject obj){
		Instantiate(SmallXPParticles, obj.transform.position, Quaternion.identity);
	}

	public void ShowEnemyDeathParticles(GameObject obj){
		Instantiate(EnemyDeathParticles, obj.transform.position, Quaternion.identity);
	}
}
