using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeCut : MonoBehaviour {

    public enum SwipeDirection
    {
        Up,
        Down,
        Left,
        Right
    }

    public float alpha = 1;
    public float alphaDecayRate = 0.1f;
    public float requiredSwipeDistance = 1.0f;
    public float timeToSelfDestroy = 0.5f;
    public SwipeDirection swipeDir = SwipeDirection.Down;

    bool isTracking = false;
    bool isPeeling = false;
    float moveStartTime = 0.0f;

    Vector3 startPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;
    Vector3 moveDir = Vector3.zero;

    Rigidbody rigid;
    SpriteRenderer sprite;

	// Use this for initialization
	void Start () {
        rigid = GetComponent<Rigidbody>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {        
        if (isPeeling)
        {
            if(Time.time > moveStartTime + timeToSelfDestroy)
            {
                Destroy(gameObject);
            }
            else
            {
                PeelingLogic();
            }
        }
        else
        {
            CheckInput();
        }
	}

    void TouchSwipe()
    {
        if (Input.touchCount > 0)
        {
            switch (Input.GetTouch(0).phase)
            {
                case TouchPhase.Began:
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                    RaycastHit rayHit;
                    if (Physics.Raycast(ray, out rayHit))
                    {
                        //check ray hit self
                        if(rayHit.collider == gameObject.GetComponent<Collider>())
                        {
                            isTracking = true;
                            //dont get the location on the object, just get screen to world
                            startPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        }                        
                    }
                    break;
                case TouchPhase.Moved:

                    break;
                case TouchPhase.Ended:
                    if (isTracking)                        
                    {
                        
                        isTracking = false;
                        //get end location screen to world for compare
                        endPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        //check swipe distance
                        if (Vector3.Distance(startPos, endPos) > requiredSwipeDistance)
                        {
                            if (CheckSwipe())
                            {
                                PeelAway();
                            }
                        }                        
                    }
                    break;
                default:
                    break;
            }
        }
    }

    void MouseSwipe()
    {
        if (!isTracking)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit rayHit;
                print("Ray shot");
                if (Physics.Raycast(ray, out rayHit))
                {
                    //check ray hit self
                    if (rayHit.collider == gameObject.GetComponent<Collider>())
                    {
                        print("Ray hit");
                        isTracking = true;
                        //dont get the location on the object, just get screen to world
                        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                isTracking = false;
                //get end location screen to world for compare
                endPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                print("Released");
                print(startPos);
                print(endPos);
                if (CheckSwipe())
                {
                    if (Vector3.Distance(startPos, endPos) > requiredSwipeDistance)
                    {
                        if (CheckSwipe())
                        {
                            PeelAway();
                        }
                    }
                }
            }
        }
    }

    void CheckInput()
    {
        MouseSwipe();
        TouchSwipe();
    }

    bool CheckSwipe()
    {
        bool valid = false;

        switch (swipeDir)
        {
            case SwipeDirection.Down:
                if(startPos.y > endPos.y)
                {
                    valid = true;
                }
                break;
            case SwipeDirection.Left:
                if (startPos.x < endPos.x)
                {
                    valid = true;
                }
                break;
            case SwipeDirection.Right:
                if (startPos.x > endPos.x)
                {
                    valid = true;
                }
                break;
            case SwipeDirection.Up:
                if (startPos.y < endPos.y)
                {
                    valid = true;
                }
                break;

            default:
                break;
        }

        return valid;
    }

    void PeelAway()
    {
        //get the direction of the swipe
        moveDir = endPos - startPos;
        isPeeling = true;
        moveStartTime = Time.time;
    }

    void PeelingLogic()
    {
        //lower alpha by set amount per 
        alpha -= alphaDecayRate * Time.deltaTime;
        if(alpha < 0)
        {
            alpha = 0;
        }
        sprite.color = new Color(1, 1, 1, alpha);

        //move in set direction
        rigid.velocity = moveDir/* * Time.deltaTime*/;
    }
}
