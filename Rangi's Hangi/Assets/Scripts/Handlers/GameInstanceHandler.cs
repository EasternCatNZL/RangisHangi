using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class GameInstanceHandler : MonoBehaviour {

    public struct RoundInfo
    {
        public int numQuestions;
        public int baseLine; // <- fraction per grade
    }

    public enum CurrentScene
    {
        HOMESCENE,
        GAMESCENE
    }

    [Header("Scene stuff")]
    public int homeScene = 0;

    [Header("Script refs")]
    public ProblemGenerator problemGenerator;
    public Timer timer;
    public LevelHandler level;
    public RoundEndHandler endHandle;

    //[Header("UI stuff")]
    //public GameObject roundEndStuff;
    //public Text finalCorrectText;
    //public GameObject levelSelectStuff;

    [Header("Tags")]
    public string levelHTag = "Handler";
    public string roundEndTag = "RoundEnder";

    public CurrentScene currentScene;

    //event stuff
    public delegate void GameEventDelegate();
    public static event GameEventDelegate GameStartEvent;
    public static event GameEventDelegate RoundStartEvent;
    //public static event GameEventDelegate RoundEndEvent;

    bool waitingForRoundStart = true;

	// Use this for initialization
	void Start () {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(levelHTag).GetComponent<LevelHandler>();
        }
        waitingForRoundStart = true;
        endHandle.roundEndStuff.SetActive(false);
        //roundEndStuff.SetActive(false);
        GameStartEvent();
	}
	
	// Update is called once per frame
	void Update () {
		if(timer.isReady && level.isReady && waitingForRoundStart)
        {
            StartRound();
        }
	}

    public void StartRound()
    {
        endHandle.roundEndStuff.SetActive(false);
        endHandle.levelSelectStuff.SetActive(false);
        waitingForRoundStart = false;
        timer.StartTimer();
        problemGenerator.NewProblem();

        //budget control <- fix later
        timer.isReady = false;
        level.isReady = false;
    }

    ////button logic
    //public void EndGame()
    //{
    //    SceneManager.LoadScene(homeScene);
    //}

    public void RedoRound()
    {
        waitingForRoundStart = true;
        GameStartEvent();
    }

    //public void DifferentLevel()
    //{
    //    levelSelectStuff.SetActive(true);
    //    roundEndStuff.SetActive(false);
    //}

    //public void CloseLevelSelect()
    //{
    //    levelSelectStuff.SetActive(false);
    //    roundEndStuff.SetActive(true);
    //}
}
