using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangiDetails : MonoBehaviour {

    public int numQuestions = 4;

    [Header("Tags")]
    public string levelHTag = "Handler";

    public LevelHandler level;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PassInfoToLevel()
    {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(levelHTag).GetComponent<LevelHandler>();
        }
        level.ReceiveHangiDetails(numQuestions);
    }
}
