using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundStartHandler : MonoBehaviour {

    public GameObject gameUI;

    [Header("Script refs")]
    public ProblemGenerator problemGenerator;
    public Timer timer;
    public LevelHandler level;
    public RoundEndHandler endHandle;

    [Header("Tags")]
    public string levelHTag = "Handler";

    // Use this for initialization
    void Start () {
        level = GameObject.FindGameObjectWithTag(levelHTag).GetComponent<LevelHandler>();
        endHandle.roundEndStuff.SetActive(false);
        timer.RoundStartPreps();
        level.RoundStartPreps();
        timer.StartTimer();
        gameUI.SetActive(true);
        problemGenerator.GenerateProblem();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
