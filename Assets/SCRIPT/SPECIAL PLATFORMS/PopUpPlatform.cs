using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpPlatform : MonoBehaviour {

    public GameObject popupPlatform;
    public bool disabled = false;
    public float WaitTime;
	
	
    // Update is called once per frame
	void Update () {
        StartCoroutine(Disable());
    }

    private IEnumerator Disable(){
        if (disabled == false)
        {
            popupPlatform.SetActive(false);
            yield return new WaitForSeconds(WaitTime);
            disabled = true;
        }
        else
        {
            popupPlatform.SetActive(true);
            yield return new WaitForSeconds(WaitTime);
            disabled = false;
        }
    }
}
