using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerObject : MonoBehaviour {

    [System.Serializable]
    public struct AnswerDetails{
        public int key;
        public string name;
        public Sprite sprite;
    }

    [Header("Tags")]
    public string myTag = "Answer";

    public SpriteRenderer spriteRend;
    public AnswerDetails details;

    //set private once tested
    public bool canBeMoved = false; 

    // Use this for initialization
    void Start () {
        gameObject.tag = myTag;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public int GetDetailsKey()
    {
        return details.key;
    }

    public string GetDetailsName()
    {
        return details.name;
    }

    public void SetSprite()
    {
        spriteRend.sprite = details.sprite;
    }

    public void SetMovable(bool movable)
    {
        canBeMoved = movable;
    }
}
