using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovable : MonoBehaviour {

    public bool isMovable = true;
    public bool isFollowing = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void TouchInputHandling(Collider2D collider)
    {
        if(Input.touchCount > 0)
        {
            if(collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position)))
            {
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        if(!isFollowing && isMovable)
                        isFollowing = true;
                        break;
                    case TouchPhase.Moved:
                        if (isFollowing)
                        {
                            transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0);
                        }
                        break;
                    case TouchPhase.Ended:
                        isFollowing = false;
                        DoReleaseLogic();
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public virtual void DoReleaseLogic()
    {

    }
}
