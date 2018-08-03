using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXM : MonoBehaviour {
	public static VFXM instance;

	public GameObject SmallXPParticles;

	void Awake(){
		if (instance == null){
			instance = this;
		}
	}

	public void ShowXPParticles(GameObject obj){
		Instantiate(SmallXPParticles, obj.transform.position, Quaternion.identity);
	}
}
