﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [Header("Timer stuff")]
    public float totalTime = 10.0f;

    [Header("UI stuff")]
    public Text timerText;

    [Header("Script refs")]
    public LevelHandler level;
    public RoundEndHandler endHandle;

    [Header("Tags")]
    public string handlerTag = "Handler";

    [HideInInspector]
    public bool isReady = false;

    bool isActive = false;
    float timeLeft = 0.0f;
    float timeTimerStarted = 0.0f;

	// Use this for initialization
	void Start () {
        level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        //StartTimer();
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            ClockTick();
        }
	}

    public void StartTimer()
    {
        timeLeft = totalTime;
        timeTimerStarted = Time.time;
        isActive = true;
    }

    public void StopTimer()
    {
        //timeLeft = 0.0f;
        isActive = false;
        //anything else that needs to happen when timer stops

    }

    public void RoundStartPreps()
    {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
        totalTime = level.timeLimitCurrentRound;
    }

    void ClockTick()
    {
        timeLeft = totalTime - (Time.time - timeTimerStarted);
        if(timeLeft <= 0)
        {
            StopTimer();
            endHandle.EndRound();
        }
        PresentTime();
    }

    void PresentTime()
    {
        int secondsLeft = (int)(timeLeft % 60);
        float timeMinusSeconds = timeLeft - secondsLeft;
        int minutesLeft = (int)(timeMinusSeconds / 60);
        timerText.text = minutesLeft + ":" + secondsLeft;
    }
}
