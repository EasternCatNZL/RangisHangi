using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstanceHandler : MonoBehaviour {

    public struct RoundInfo
    {
        public int numQuestions;
        public int baseLine; // <- fraction per grade
    }

    [Header("Scene stuff")]
    public int homeScene = 0;

    [Header("Script refs")]
    public ProblemGenerator problemGenerator;
    public Timer timer;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndRound()
    {

    }

    public void EndGame()
    {
        SceneManager.LoadScene(homeScene);
    }
}
