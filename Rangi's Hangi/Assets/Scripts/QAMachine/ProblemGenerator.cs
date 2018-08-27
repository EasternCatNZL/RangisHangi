using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemGenerator : MonoBehaviour {

    public int numPossibleSolutions = 2;
    public int correctAnswerScore = 10;
    public Transform stuffHolder; //For parenting use, stop heirachy getting flooded
    public Transform answerSpawnPoint;
    public Transform[] solutionSpawnPoints = new Transform[2];
    //public Text scoreText;

    [Header("Tags")]
    public string answerTag = "Answer";
    public string handlerTag = "Handler";
    //public string solutionTag = "Solution";

    [Header("Lists")]
    //lists <- find a better way to do this
    public List<AnswerObject> answerList = new List<AnswerObject>();
    public List<AnswerObject> wrongAnswerList = new List<AnswerObject>();
    public List<AnswerObject> possibleSolutionList = new List<AnswerObject>();

    //change to private after testing
    public int score = 0;

    public AnswerObject answer;
    //public GameInstanceHandler gameInstance;
    public LevelHandler level;
    public RoundEndHandler endHandle;

    bool isReady = false;

	// Use this for initialization
	void Start () {
        //GenerateProblem();
        //gameInstance = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<GameInstanceHandler>();
        //level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
        //NewProblem();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearProblem();
            GenerateProblem();
        }
	}

    //private void OnEnable()
    //{
    //    GameInstanceHandler.GameStartEvent += RoundStartPreps;
    //}

    //private void OnDisable()
    //{
    //    GameInstanceHandler.GameStartEvent -= RoundStartPreps;
    //}

    void RoundStartPreps()
    {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
        //if (!gameInstance)
        //{
        //    gameInstance = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<GameInstanceHandler>();
        //}
        //isReady = true;
    }

    public void NewProblem()
    {
        ClearProblem();
        GenerateProblem();
    }

    //generate problem and answer
    public void GenerateProblem()
    {
        //get random answer
        int rand = Random.Range(0, answerList.Count - 1);
        //set answer and add answer to possible solution list
        answer = answerList[rand];
        possibleSolutionList.Add(answerList[rand]);
        //fill in other possible solutions with other things
        int solutionsProvided = 1;
        while (solutionsProvided < numPossibleSolutions)
        {
            //get random in possible solutions, and then check if already existing
            rand = Random.Range(0, answerList.Count - 1);
            if (!possibleSolutionList.Contains(answerList[rand]))
            {
                possibleSolutionList.Add(answerList[rand]);
                solutionsProvided++;
            }
        }
        //shuffle possible solutions
        ShuffleSolutions();
        //present to player
        PresentProblemToPlayer();
    }

    //rearrange solutions using fisher yates card shuffler
    void ShuffleSolutions()
    {
        System.Random random = new System.Random();
        for(int i = 0; i < possibleSolutionList.Count; i++)
        {
            int r = i + (int)(random.NextDouble() * (possibleSolutionList.Count - i));
            AnswerObject randAnswer = possibleSolutionList[r];
            possibleSolutionList[r] = possibleSolutionList[i];
            possibleSolutionList[i] = randAnswer;
        }
    }

    void PresentProblemToPlayer()
    {
        for(int i = 0; i < possibleSolutionList.Count; i++)
        {
            GameObject solution = Instantiate(possibleSolutionList[i].gameObject, solutionSpawnPoints[i].position, Quaternion.identity);
            SetupSolutionObject(solution.GetComponent<AnswerObject>(), solutionSpawnPoints[i].position);
        }
        GameObject answerObj = Instantiate(answer.gameObject, answerSpawnPoint.position, Quaternion.identity);
        //reassign answer just in case
        answer = answerObj.GetComponent<AnswerObject>();
        SetupAnswerObject(answerObj.GetComponent<AnswerObject>());
        //answerObj.GetComponent<AnswerObject>().isMovable = false;
    }

    void SetupSolutionObject(AnswerObject solution, Vector3 startPos)
    {
        solution.SetMovable(true);
        solution.SetSprite();
        solution.isAnswer = true;
        solution.problemGenerator = this;
        solution.startPos = startPos;
        solution.transform.SetParent(stuffHolder);
    }

    void SetupAnswerObject(AnswerObject answer)
    {
        answer.SetSprite();
        answer.GetComponent<Collider>().isTrigger = true;
        //answer.GetComponent<Rigidbody>().bodyType = RigidbodyType2D.Dynamic;
        answer.tag = answerTag;
        answer.gameObject.layer = 0;
        answer.problemGenerator = this;
        answer.isMovable = false;
        answer.transform.SetParent(stuffHolder);
    }

    public void ProcessProblem()
    {
        print("Tried processing");
        if (answer.currentLink)
        {
            print("Link found");
            if (answer.details.key == answer.currentLink.GetComponent<AnswerObject>().details.key)
            {
                //is right
                //score += correctAnswerScore;
                //scoreText.text = "Score: " + score;
                level.currentCorrect++;
            }
            else
            {
                //is wrong
            }
            level.currentQuestion++;
            if(level.currentQuestion < level.numQuestionsCurrentRound)
            {
                //next problem
                NewProblem();
            }
            else
            {
                endHandle.EndRound();
            }
        }
    }

    void ClearProblem()
    {
        possibleSolutionList.Clear();
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag(answerTag);
        if(answerObjects.Length > 0)
        {
            for (int i = 0; i < answerObjects.Length; i++)
            {
                Destroy(answerObjects[i]);
            }
        }

        if (answer.gameObject)
        {
            Destroy(answer.gameObject);
        }
        
    }
}
