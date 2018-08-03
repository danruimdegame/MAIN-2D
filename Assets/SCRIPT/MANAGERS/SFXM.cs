using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXM : MonoBehaviour {
	public static SFXM instance;

	public SFX SoundFX;

	public SFXPlayer SoundFXPlayer;

	void Awake(){
		if (instance == null){
			instance = this;
		}
	}

	public void PlayXPPickupSound(GameObject obj){
		AudioSource.PlayClipAtPoint(SoundFX.XPPickup, obj.transform.position);
	}

	public void PlayJumpSound(GameObject obj){
		AudioSource.PlayClipAtPoint(SoundFXPlayer.Jump, obj.transform.position);
	}
}
