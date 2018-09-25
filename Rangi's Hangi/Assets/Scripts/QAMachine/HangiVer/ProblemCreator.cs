using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProblemCreator : MonoBehaviour {

    bool showImage = false; //check if solution objects should be showing image or text
    public int numPossibleSolutions = 4;
    public Transform stuffHolder; //For parenting use, stop heirachy getting flooded
    public Transform outOfScreenHolder; //Need to hold objects to evaluate later
    public Transform[] foodChoicesSpawnPos = new Transform[4];
    public Transform[] toolSpawnPos = new Transform[4];

    [Header("Tags")]
    public string answerTag = "Answer";

    [Header("Lists")]
    //lists <- find a better way to do this
    public List<AnswerObject> answerList = new List<AnswerObject>();
    public List<AnswerObject> wrongAnswerList = new List<AnswerObject>();
    public List<AnswerObject> toolList = new List<AnswerObject>();

    [Header("UI things")]
    public Button switchVisualButton;

    //public LevelHandler level;
    public Hangi hangi;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //void RoundStartPreps()
    //{
    //    if (!level)
    //    {
    //        level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
    //    }
    //}

    public void GenerateProblems()
    {
        for (int i = 0; i < hangi.foodContents.Length; i++)
        {
            //get random answer
            int rand = Random.Range(0, answerList.Count - 1);
            //set answer and add answer to possible solution list
            hangi.foodContents[i].itemWanted = answerList[rand];
            hangi.foodContents[i].possibleSolutions = new List<AnswerObject>();
            hangi.foodContents[i].possibleSolutions.Add(answerList[rand]);

            //fill in other possible solutions with other things
            int solutionsProvided = 1;
            while (solutionsProvided < numPossibleSolutions)
            {
                rand = Random.Range(0, 2);
                List<AnswerObject> listRef;
                if (rand == 0)
                {
                    listRef = answerList;
                }
                else
                {
                    listRef = wrongAnswerList;
                }
                //get random in possible solutions, and then check if already existing
                rand = Random.Range(0, listRef.Count - 1);
                if (!hangi.foodContents[i].possibleSolutions.Contains(listRef[rand]))
                {
                    hangi.foodContents[i].possibleSolutions.Add(listRef[rand]);
                    solutionsProvided++;
                }
            }

            //shuffle possible solutions
            ShuffleSolutions(hangi.foodContents[i].possibleSolutions);
        }
    }

    //rearrange solutions using fisher yates card shuffler
    void ShuffleSolutions(List<AnswerObject> list)
    {
        System.Random random = new System.Random();
        for (int i = 0; i < list.Count; i++)
        {
            int r = i + (int)(random.NextDouble() * (list.Count - i));
            AnswerObject randAnswer = list[r];
            list[r] = list[i];
            list[i] = randAnswer;
        }
    }

    public void ClearProblem()
    {
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag(answerTag);
        if (answerObjects.Length > 0)
        {
            for (int i = 0; i < answerObjects.Length; i++)
            {
                answerObjects[i].GetComponent<AnswerObject>().moveBack = false;
                answerObjects[i].gameObject.transform.position = outOfScreenHolder.position;
                answerObjects[i].gameObject.transform.SetParent(outOfScreenHolder);
                //Destroy(answerObjects[i]);
            }
        }
    }

    public void SpawnTools()
    {
        for(int i = 0; i < toolList.Count; i++)
        {
            GameObject toolClone = Instantiate(toolList[i].gameObject, toolSpawnPos[i].position, Quaternion.identity);
            toolClone.name = toolList[i].gameObject.name;
            SetupSolutionObject(toolClone.GetComponent<AnswerObject>(), toolSpawnPos[i].position);
            SetVisual(toolClone.GetComponent<AnswerObject>());
        }
    }

    public void SpawnChoices(int currentIndex)
    {
        for(int i = 0; i < hangi.foodContents[currentIndex].possibleSolutions.Count; i++)
        {
            GameObject choiceClone = Instantiate(hangi.foodContents[currentIndex].possibleSolutions[i].gameObject, foodChoicesSpawnPos[i].position, Quaternion.identity);
            choiceClone.name = hangi.foodContents[currentIndex].possibleSolutions[i].gameObject.name;
            SetupSolutionObject(choiceClone.GetComponent<AnswerObject>(), foodChoicesSpawnPos[i].position);
            SetVisual(choiceClone.GetComponent<AnswerObject>());
        }
    }

    void SetupSolutionObject(AnswerObject solution, Vector3 startPos)
    {
        solution.SetMovable(true);
        //solution.SetTextMesh();
        solution.isAnswer = true;
        //solution.problemGenerator = this;
        solution.startPos = startPos;
        //solution.hangi = GetComponent<Hangi>();
        solution.transform.SetParent(stuffHolder);
    }

    void SetVisual(AnswerObject solution)
    {
        if (showImage)
        {
            solution.SetSprite();            
        }
        else
        {
            solution.SetTextMesh();
        }
    }

    public void ChangeVisuals()
    {
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag(answerTag);
        if (answerObjects.Length > 0)
        {
            for (int i = 0; i < answerObjects.Length; i++)
            {
                SetVisual(answerObjects[i].GetComponent<AnswerObject>());
                //Destroy(answerObjects[i]);
            }
        }
    }

    public void ChangeVisualButtonLogic()
    {
        if (showImage)
        {
            showImage = false;
            switchVisualButton.gameObject.GetComponentInChildren<Text>().text = "Show Image";
        }
        else
        {
            showImage = true;
            switchVisualButton.gameObject.GetComponentInChildren<Text>().text = "Show Text";
        }
        ChangeVisuals();
    }
}
