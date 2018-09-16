using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnswerObject : TouchMovable {

    [System.Serializable]
    public struct AnswerDetails{
        public int key;
        public string name;
        public string maoriName;
        public Sprite sprite;
    }

    public float moveBackTime = 1.0f;

    public SpriteRenderer spriteRend;
    public TextMesh textMesh;
    public AnswerDetails details;

    [Header("Tags")]
    public string myTag = "Answer";
    public string hangiTag = "Hangi";

    //set private once tested
    //public bool canBeMoved = false;
    public bool moveBack = true;
    public bool isAnswer = false;
    public Vector3 startPos = Vector3.zero;

    public AnswerObject currentLink;
    public ProblemGenerator problemGenerator;
    public Hangi hangi;

    // Use this for initialization
    void Start () {
        gameObject.tag = myTag;
        myRigid = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        //TouchInputHandling(GetComponent<Collider2D>());
        MouseInputHandling(GetComponent<Collider>());
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
        spriteRend = gameObject.AddComponent<SpriteRenderer>();
        spriteRend.sprite = details.sprite;
    }

    public void SetTextMesh()
    {
        textMesh = gameObject.AddComponent<TextMesh>();
        textMesh.text = details.maoriName;
        textMesh.characterSize = 0.5f;
        textMesh.anchor = TextAnchor.MiddleCenter;
    }

    public void SetMovable(bool movable)
    {
        isMovable = movable;
    }

    /*
    public override void MouseInputHandling(Collider collider){
        if (Input.GetMouseButtonDown(0) && !mouseDown)
        {
            print("Mouse pressed");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            if (Physics.Raycast(ray, out rayHit))
            {
                print("Ray shot");
                if (rayHit.collider == collider)
                {
                    print("Hit " + gameObject.name);
                    if (!isFollowing && isMovable)
                    {
                        print("Doing things");
                        isFollowing = true;
                        Vector2 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
                        myRigid.MovePosition(touchPos);
                    }
                }
            }

            //if(collider == Physics2D.OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition))){
            //    if (!isFollowing && isMovable)
            //    {
            //        isFollowing = true;
            //    }
            //}
        }
        else if (Input.GetMouseButton(0) && mouseDown && isFollowing)
        {
            Vector2 touchPos = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            myRigid.MovePosition(touchPos);
        }
        else if (Input.GetMouseButtonUp(0) && mouseDown && isFollowing)
        {
            isFollowing = false;
            mouseDown = false;
            DoReleaseLogic();
        }
    }
    */

    //checks to see if problem is above answer, otherwise move back to original pos
    public override void DoReleaseLogic()
    {
        if (hangi)
        {
            hangi.CollectItem(this);
        }
        else
        {
            problemGenerator.ProcessProblem();            
        }
        if (moveBack)
        {
            transform.DOMove(startPos, moveBackTime);
        }
        
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = collision.gameObject.GetComponent<AnswerObject>();
            Debug.Log("Bruh");
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = null;
        }
    }
    */

    private void OnTriggerEnter(Collider collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = collision.gameObject.GetComponent<AnswerObject>();
            Debug.Log("Bruh");
        }
        else if (collision.gameObject.GetComponent<Hangi>())
        {
            hangi = collision.gameObject.GetComponent<Hangi>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //check if other is also answer object only
        if (collision.gameObject.GetComponent<AnswerObject>())
        {
            currentLink = null;
        }
        else if (collision.gameObject.GetComponent<Hangi>())
        {
            hangi = null;
        }
    }
}
