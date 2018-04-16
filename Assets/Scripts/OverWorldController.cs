using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
public class OverWorldController : MonoBehaviour {

	// Use this for initialization
	public void StartButton()
    {
        SceneManager.LoadScene(1);
    }
}
