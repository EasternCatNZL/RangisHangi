using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelHandler : MonoBehaviour {

    [Header("Scene stuff")]
    public int gameScene = 1;

    [Header("Script refs")]
    public GameInstanceHandler gameInstance;

    [Header("Details")]
    public int numQuestionsCurrentRound = 0;
    public int baseLineCurrentRound = 0;
    public float timeLimitCurrentRound = 0.0f;

    //control vars for current round
    public int currentQuestion = 1;
    public int currentCorrect = 0;
    //float timeStarted = 0.0f;
    public bool isReady = false;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnEnable()
    {
        GameInstanceHandler.GameStartEvent += RoundStartPreps;
    }

    private void OnDisable()
    {
        GameInstanceHandler.GameStartEvent -= RoundStartPreps;
    }

    void RoundStartPreps()
    {
        currentQuestion = 1;
        currentCorrect = 0;
        isReady = true;
    }

    public void ReceiveLevelDetails(int numQ, int baseL, float timeL)
    {
        numQuestionsCurrentRound = numQ;
        baseLineCurrentRound = baseL;
        timeLimitCurrentRound = timeL;

        //if in home scene, load to game scene
        //if(gameInstance.currentScene == GameInstanceHandler.CurrentScene.HOMESCENE)
        //{
        SceneManager.LoadScene(gameScene);
        //}
    }
}
