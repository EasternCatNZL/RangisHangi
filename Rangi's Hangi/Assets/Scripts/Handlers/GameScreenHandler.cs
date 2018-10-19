using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenHandler : MonoBehaviour {

    public enum currentState
    {
        DialougeOpen,
        DialougeClosed,
        ScenePlaying
    }

    public currentState state;

    [Header("Script refs")]
    public DialougeBox dialougeBox;
    public DialougeTypewriter typewriter;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HandleInput();
	}

    void HandleInput()
    {
        TouchInputCheck();
        MouseInput();
    }

    //handle any touch input based on state
    void TouchInputCheck()
    {
        if(Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    
                    switch (state)
                    {
                        case currentState.DialougeOpen:
                            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                            RaycastHit rayHit;
                            if (Physics.Raycast(ray, out rayHit))
                            {
                                if (rayHit.collider == dialougeBox.GetComponent<Collider>())
                                {
                                    dialougeBox.HandleInput();
                                }
                            }
                            else if (typewriter.finishedPrint)
                            {
                                dialougeBox.HideDialouge();
                            }
                            else
                            {
                                typewriter.PrintAll();
                            }
                            
                            break;
                        case currentState.DialougeClosed:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    //debug
    void MouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            switch (state)
            {
                case currentState.DialougeOpen:
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                    {
                        if (rayHit.collider == dialougeBox.GetComponent<Collider>())
                        {
                            dialougeBox.HandleInput();
                        }
                    }
                    else if (typewriter.finishedPrint)
                    {
                        dialougeBox.HideDialouge();
                    }
                    else
                    {
                        typewriter.PrintAll();
                    }

                    break;
                case currentState.DialougeClosed:
                    break;
                default:
                    break;
            }
        }
    }
}
