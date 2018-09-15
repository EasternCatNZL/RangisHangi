using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangiResultsButtons : MonoBehaviour {

    public int homeSceneNum = 0;
    public int hangiSceneNum = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RedoGame()
    {
        SceneManager.LoadScene(hangiSceneNum);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(homeSceneNum);
    }
}
