using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RoundEndHandler : MonoBehaviour {

    [Header("Script refs")]
    public ProblemGenerator problemGenerator;
    public Timer timer;
    public LevelHandler level;
    public GameInstanceHandler game;

    [Header("UI stuff")]
    public GameObject roundEndStuff;
    public Text finalCorrectText;
    public GameObject levelSelectStuff;
    public GameObject gameUIStuff;
    //public Text totalTextText;

    [Header("Scene stuff")]
    public int homeScene = 0;

    [Header("Tags")]
    public string handlerTag = "Handler";

    // Use this for initialization
    void Start () {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
        if (!game)
        {
            game = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<GameInstanceHandler>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EndRound()
    {
        timer.StopTimer();
        //go to round end ui
        roundEndStuff.SetActive(true);
        finalCorrectText.text = "Final Score: \n" + level.currentCorrect + "/" + level.numQuestionsCurrentRound;
    }

    //button logic
    public void EndGame()
    {
        SceneManager.LoadScene(homeScene);
    }

    public void RedoRound()
    {
        game.RedoRound();
    }

    public void DifferentLevel()
    {
        levelSelectStuff.SetActive(true);
        roundEndStuff.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        levelSelectStuff.SetActive(false);
        roundEndStuff.SetActive(true);
    }
}
