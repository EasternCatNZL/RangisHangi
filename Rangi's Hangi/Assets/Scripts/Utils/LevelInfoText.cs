using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelInfoText : MonoBehaviour {

    public Text infoText;
    public string levelHTag = "Handler";
    LevelHandler level;

	// Use this for initialization
	void Start () {
        level = GameObject.FindGameObjectWithTag(levelHTag).GetComponent<LevelHandler>();
        infoText.text = "Questions: " + level.numQuestionsCurrentRound + "\nTime: " + level.timeLimitCurrentRound;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
