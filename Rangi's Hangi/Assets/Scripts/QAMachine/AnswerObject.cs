using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnswerObject : TouchMovable {

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
    public bool isAnswer = false;

    public AnswerObject currentLink;

    // Use this for initialization
    void Start () {
        gameObject.tag = myTag;
	}
	
	// Update is called once per frame
	void Update () {
        TouchInputHandling(GetComponent<Collider2D>());
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
        isMovable = movable;
    }

    //checks to see if problem is above answer, otherwise move back to original pos
    public override void DoReleaseLogic()
    {
        if (!isAnswer)
        {
            if (currentLink)
            {
                if (currentLink.GetComponent<AnswerObject>().isAnswer)
                {
                    if (details.key == currentLink.GetComponent<AnswerObject>().details.key)
                    {
                        //is right
                    }
                    else
                    {
                        //is wrong
                    }
                    //next problem
                    if (GetComponentInParent<ProblemGenerator>())
                    {
                        GetComponentInParent<ProblemGenerator>().NewProblem();
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = collision.gameObject.GetComponent<AnswerObject>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = null;
        }
    }
}
