using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeTypewriter : MonoBehaviour {

    [Header("Typing vars")]
    public float timeNextChar = 0.2f;
    public string testString = "Hey man, type this out to see if it works fam.";

    [Header("UI components")]
    public Text textbox;

    [Header("Script refs")]
    public GameScreenHandler handler;
    public DialougeBox box;

    [HideInInspector]
    public bool finishedPrint = false;
    int currentCharIndex = 0;
    float timeLastChar = 0.0f;
    string textToPrint;

	// Use this for initialization
	void Start () {
        //ReadyNextText(testString);
	}
	
	// Update is called once per frame
	void Update () {
        if (!box.isHidden && !finishedPrint)
        {
            PrintText();
        }
	}

    public void ReadyNextText(string nextText)
    {
        //set text box back to blank
        textbox.text = "";
        //set index back to 0
        currentCharIndex = 0;
        finishedPrint = false;

        textToPrint = nextText;
    }

    void PrintText()
    {
        string tempString = textbox.text;
        if(Time.time > timeLastChar + timeNextChar && currentCharIndex < textToPrint.Length)
        {
            tempString += textToPrint[currentCharIndex];
            textbox.text = tempString;
            timeLastChar = Time.time;
            currentCharIndex++;
            if(currentCharIndex >= textToPrint.Length)
            {
                finishedPrint = true;
            }
        }
    }

    public void PrintAll()
    {
        textbox.text = textToPrint;
        finishedPrint = true;
    }
}
