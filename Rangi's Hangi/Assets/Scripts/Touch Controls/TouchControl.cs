using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchControl : MonoBehaviour {

    bool isTouching = false;
    bool isHolding = false;

    int heldFinger = 0;

    //Touch touch;
    GameObject heldObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //TouchAndRelease();
        //PickUpMovable();
        //TouchHandling();
	}

    /*
    //Handle touch and release
    private void TouchAndRelease()
    {
        if (!isTouching)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                isTouching = true;
                //attempt pick up
                PickUpMovable();
            }
        }
        else if (isTouching)
        {
            if(touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
    }
    */

        /*
    //touch handling
    private void TouchHandling()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 touchPos = Camera.main.ScreenToWorldPoint(touch.rawPosition);
            touchPos.z = 0;
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    if (!isHolding)
                    {
                        PickUpMovable(touch);
                    }
                    break;
                case TouchPhase.Moved:
                    if (heldFinger == touch.fingerId)
                    {
                        MovePickedupObject(touch);
                    }
                    break;
                case TouchPhase.Ended:
                    if (heldFinger == touch.fingerId)
                    {
                        ReleaseMovable();
                    }
                    break;
                default:
                    break;

            }
        }
    }
    */

    void PickUpMovable(Touch touch)
    {
        Ray ray = Camera.main.ScreenPointToRay(touch.rawPosition);
        RaycastHit hitInfo;
        if(Physics.Raycast(ray, out hitInfo))
        {
            if (hitInfo.collider.gameObject.GetComponent<TouchMovable>())
            {
                if (hitInfo.collider.gameObject.GetComponent<TouchMovable>().isMovable)
                {
                    heldObject = hitInfo.collider.gameObject;
                    heldObject.GetComponent<TouchMovable>().isFollowing = true;
                    isHolding = true;
                    heldFinger = touch.fingerId;
                }
            }
        }
    }

    void MovePickedupObject(Touch touch)
    {
        heldObject.transform.position = touch.rawPosition;
    }

    void ReleaseMovable()
    {
        heldObject.GetComponent<TouchMovable>().DoReleaseLogic();
        heldObject = null;
        isHolding = false;
    }
}
