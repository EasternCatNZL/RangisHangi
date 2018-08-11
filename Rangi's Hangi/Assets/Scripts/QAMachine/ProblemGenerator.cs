using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemGenerator : MonoBehaviour {

    public int numPossibleSolutions = 2;
    public Transform stuffHolder; //For parenting use, stop heirachy getting flooded
    public Transform answerSpawnPoint;
    public Transform[] solutionSpawnPoints = new Transform[2];

    [Header("Tags")]
    public string answerObjectTag = "Answer";

    [Header("Lists")]
    //lists <- find a better way to do this
    public List<AnswerObject> answerList = new List<AnswerObject>();
    public List<AnswerObject> wrongAnswerList = new List<AnswerObject>();
    public List<AnswerObject> possibleSolutionList = new List<AnswerObject>();

    //change to private after testing
    public AnswerObject answer; 

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ClearProblem();
            GenerateProblem();
        }
	}

    //generate problem and answer
    void GenerateProblem()
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
            solution.GetComponent<AnswerObject>().SetMovable(true);
            solution.GetComponent<AnswerObject>().SetSprite();
            solution.transform.SetParent(stuffHolder);
        }
        GameObject answerObj = Instantiate(answer.gameObject, answerSpawnPoint.position, Quaternion.identity);
        //reassign answer just in case
        answer = answerObj.GetComponent<AnswerObject>();
        answer.GetComponent<AnswerObject>().SetSprite();
        answer.transform.SetParent(stuffHolder);
    }

    void ClearProblem()
    {
        possibleSolutionList.Clear();
        GameObject[] answerObjects = GameObject.FindGameObjectsWithTag(answerObjectTag);
        for(int i = 0; i < answerObjects.Length; i++)
        {
            Destroy(answerObjects[i]);
        }
    }
}
