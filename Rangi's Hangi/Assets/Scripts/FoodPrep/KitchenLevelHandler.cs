using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenLevelHandler : GameLevel {

    [Header("Dialouge")]
    public string startDialouge = "Peel the skin off the potato";
    public string endDialouge = "Well Done!";

    ////control vars
    //int dialougeIndex = 0;

	// Use this for initialization
	void Start () {
        StartLevel();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public override void StartLevel()
    {
        textbox.ShowDialouge();
        typewriter.ReadyNextText(startDialouge);
        
    }

    public override void EndLevel()
    {
        textbox.ShowDialouge();
        typewriter.ReadyNextText(endDialouge);
        
    }
}
