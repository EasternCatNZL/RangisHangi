using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLevel : MonoBehaviour {

    [Header("Script refs")]
    public DialougeBox textbox;
    public DialougeTypewriter typewriter;
    public GameScreenHandler gameHandler;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public virtual void StartLevel()
    {

    }

    public virtual void EndLevel()
    {

    }
}
