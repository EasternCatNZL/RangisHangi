using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDetails : MonoBehaviour {

    public int numQuestions = 3;
    public int baseLine = 1; // <- fraction per grade
    public float timeLimit = 10.0f;

    [Header("Tags")]
    public string levelHTag = "Handler";

    public LevelHandler level;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PassInfoToLevelHandler()
    {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(levelHTag).GetComponent<LevelHandler>();
        }
        level.ReceiveLevelDetails(numQuestions, baseLine, timeLimit);
    }
}
