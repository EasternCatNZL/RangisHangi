using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hangi : MonoBehaviour {

    [System.Serializable]
    public struct Contents
    {
        public AnswerObject item;
        public List<AnswerObject> possibleSolutions;
    }

    public Contents[] contents = new Contents[0];

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
