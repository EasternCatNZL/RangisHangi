using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class DialougeBox : MonoBehaviour {

    [Header("Movement vars")]
    public float moveTime = 1.0f;

    [Header("Positions")]
    //public Transform textboxOrigin;
    //public Transform textboxActivePos;
    public RectTransform textboxOrigin;
    public RectTransform textboxActivePos;

    [Header("Ui elements")]
    public GameObject textbox;

    [Header("Script refs")]
    public GameScreenHandler handler;
    public DialougeTypewriter typewriter;

    [HideInInspector]
    public bool isHidden = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //debug
        if (Input.GetKeyDown(KeyCode.K) && isHidden)
        {
            print("Showing");
            ShowDialouge();
        }
        else if (Input.GetKeyDown(KeyCode.L) && !isHidden)
        {
            print("Hiding");
            HideDialouge();
        }
	}

    public void ShowDialouge()
    {
        textbox.SetActive(true);

        //create tween to scale and move at same time
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(textboxActivePos.position, moveTime));
        //scale while moving
        seq.Insert(0, transform.DOScale(new Vector3(1, 1, 1), seq.Duration()));

        handler.state = GameScreenHandler.currentState.DialougeOpen;
        isHidden = false;
    }

    public void HideDialouge()
    {
        textbox.SetActive(false);

        //create tween to scale and move at same time
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMove(textboxOrigin.position, moveTime));
        //scale while moving
        seq.Insert(0, transform.DOScale(new Vector3(0, 0, 0), seq.Duration()));

        handler.state = GameScreenHandler.currentState.DialougeClosed;
        isHidden = true;
    }

    public void HandleInput(/*Collider collider*/)
    {
        if (!typewriter.finishedPrint)
        {
            typewriter.PrintAll();
        }
    }
}
