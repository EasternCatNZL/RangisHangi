using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemCreator : MonoBehaviour {

    public int numPossibleSolutions = 4;
    public Transform stuffHolder; //For parenting use, stop heirachy getting flooded
    public Transform[] solutionSpawnPoints = new Transform[4];

    [Header("Tags")]
    public string handlerTag = "Handler";

    [Header("Lists")]
    //lists <- find a better way to do this
    public List<AnswerObject> answerList = new List<AnswerObject>();
    public List<AnswerObject> wrongAnswerList = new List<AnswerObject>();

    public LevelHandler level;
    public Hangi hangi;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void RoundStartPreps()
    {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
    }

    public void GenerateProblems()
    {
        for (int i = 0; i < hangi.contents.Length; i++)
        {
            //get random answer
            int rand = Random.Range(0, answerList.Count - 1);
            //set answer and add answer to possible solution list
            hangi.contents[i].item = answerList[rand];
            hangi.contents[i].possibleSolutions.Add(answerList[rand]);

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
                if (!hangi.contents[i].possibleSolutions.Contains(listRef[rand]))
                {
                    hangi.contents[i].possibleSolutions.Add(listRef[rand]);
                    solutionsProvided++;
                }
            }

            //shuffle possible solutions
            ShuffleSolutions(hangi.contents[i].possibleSolutions);
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
}
