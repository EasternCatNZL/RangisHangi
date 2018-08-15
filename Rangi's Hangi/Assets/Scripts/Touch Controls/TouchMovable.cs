using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchMovable : MonoBehaviour {

    public bool isMovable = true;
    public bool isFollowing = false;

    protected bool mouseDown = false;
    public Rigidbody myRigid;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    protected void TouchInputHandling(Collider collider)
    {
        if(Input.touchCount > 0)
        {
            
            
                switch (Input.GetTouch(0).phase)
                {
                    case TouchPhase.Began:
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                        RaycastHit rayHit;
                        if (Physics.Raycast(ray, out rayHit))
                        {
                            if (!isFollowing && isMovable)
                            isFollowing = true;
                        }
                        break;
                    case TouchPhase.Moved:
                        if (isFollowing)
                        {
                            Vector3 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).x, Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position).y, 0);
                            myRigid.MovePosition(touchPos);
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

    public virtual void MouseInputHandling(Collider collider)
    {
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            //print("Mouse pressed");
            mouseDown = true;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if(Physics.Raycast(ray, out rayHit))
            {
                //print("Ray shot");
                if(rayHit.collider == collider)
                {
                    //print("Hit " + gameObject.name);
                    if (!isFollowing && isMovable)
                    {
                        //print("Doing things");
                        isFollowing = true;
                        Vector3 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
                        myRigid.MovePosition(touchPos);
                    }
                }
            }
        }
        else if(Input.GetMouseButton(0) && mouseDown && isFollowing)
        {
            Vector2 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            myRigid.MovePosition(touchPos);
        }
        else if(Input.GetMouseButtonUp(0) && mouseDown)
        {
            mouseDown = false;
            if (isFollowing)
            {
                isFollowing = false;

                DoReleaseLogic();
            }
            
        }
    }

    public virtual void DoReleaseLogic()
    {
        print("Doin this shit instead");
    }
}
