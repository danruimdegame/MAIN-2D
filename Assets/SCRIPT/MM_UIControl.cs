using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MM_UIControl : MonoBehaviour {

    public void LoadScene(int indexNumber)
    {
        SceneManager.LoadScene(indexNumber);
    }
}
