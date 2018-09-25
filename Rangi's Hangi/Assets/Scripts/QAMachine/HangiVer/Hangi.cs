using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hangi : MonoBehaviour {

    [Header("Round stuff")]
    public int numFoodItems = 4;

    public enum Phase
    {
        StackWood,
        AddStone,
        LightFire,
        PlaceFood,
        PlaceCover
    }

    [System.Serializable]
    public struct Contents
    {
        public AnswerObject itemWanted;
        public AnswerObject itemGiven;
        public List<AnswerObject> possibleSolutions;
    }

    [System.Serializable]
    public struct TextMessage
    {
        public string english;
        public string maori;
    }

    [Header("Hangi contents")]
    public Contents wood;
    public Contents stone;
    public Contents fire;
    public Contents cover;
    public Contents[] foodContents = new Contents[0];

    [Header("Pit positioning")]
    public GameObject pitObject;
    public Transform[] objectPos = new Transform[6];

    [Header("UI Text instructions")]
    public TextMessage stackWoodMessage;
    public TextMessage addStoneMessage;
    public TextMessage lightFireMessage;
    public TextMessage placeFoodMessage;
    public TextMessage placeCoverMessage;

    [Header("UI game components")]
    public Text instructionsText;
    public Image nextItemImage;

    [Header("UI results components")]
    public GameObject resultsPanel;
    public Image woodWanted;
    public Image woodGiven;
    public Image stoneWanted;
    public Image stoneGiven;
    public Image fireWanted;
    public Image fireGiven;
    public Image food1Wanted;
    public Image food1Given;
    public Image food2Wanted;
    public Image food2Given;
    public Image food3Wanted;
    public Image food3Given;
    public Image food4Wanted;
    public Image food4Given;
    public Image coverWanted;
    public Image coverGiven;

    [Header("Script refs")]
    public LevelHandler level;
    public ProblemCreator creator;

    [Header("Tags")]
    public string handlerTag = "Handler";

    //control vars
    private int currentFoodItem = 0;
    private Phase phase = Phase.StackWood;

	// Use this for initialization
	void Start () {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
        SetupLevel();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //set up the level
    public void SetupLevel()
    {
        resultsPanel.SetActive(false);
        numFoodItems = level.numQuestionsCurrentRound;
        foodContents = new Contents[numFoodItems];
        creator.GenerateProblems();

        //begin
        StackWoodPrompt();
    }

    public void CollectItem(AnswerObject item)
    {

        switch (phase)
        {
            case Phase.StackWood:
                wood.itemGiven = item;
                creator.ClearProblem();
                AddStonePrompt();
                break;
            case Phase.AddStone:
                stone.itemGiven = item;
                creator.ClearProblem();
                LightFirePrompt();
                break;
            case Phase.LightFire:
                fire.itemGiven = item;
                creator.ClearProblem();
                PlaceFoodPrompt();
                break;
            case Phase.PlaceFood:
                foodContents[currentFoodItem].itemGiven = item;
                currentFoodItem++;
                if(currentFoodItem >= foodContents.Length)
                {
                    creator.ClearProblem();
                    PlaceCoverPrompt();
                }
                else
                {
                    creator.ClearProblem();
                    PlaceFoodPrompt();
                }
                break;
            case Phase.PlaceCover:
                cover.itemGiven = item;
                creator.ClearProblem();
                FinishRound();
                break;
            default:
                break;
        }
    }

    //prompts for each point
    public void StackWoodPrompt()
    {
        phase = Phase.StackWood;
        instructionsText.text = stackWoodMessage.maori;
        creator.SpawnTools();
    }

    void AddStonePrompt()
    {
        
        phase = Phase.AddStone;
        instructionsText.text = addStoneMessage.maori;
        creator.SpawnTools();
    }

    void LightFirePrompt()
    {
        phase = Phase.LightFire;
        instructionsText.text = lightFireMessage.maori;
        creator.SpawnTools();
    }

    void PlaceFoodPrompt()
    {
        phase = Phase.PlaceFood;
        instructionsText.text = placeFoodMessage.maori;
        nextItemImage.sprite = foodContents[currentFoodItem].itemWanted.details.sprite;
        creator.SpawnTools();
        creator.SpawnChoices(currentFoodItem);
    }

    void PlaceCoverPrompt()
    {
        phase = Phase.PlaceCover;
        instructionsText.text = placeCoverMessage.maori;
        creator.SpawnTools();
    }

    void FinishRound()
    {
        resultsPanel.SetActive(true);

        //set images
        woodWanted.sprite = wood.itemWanted.details.sprite;
        woodGiven.sprite = wood.itemGiven.details.sprite;
        stoneWanted.sprite = stone.itemWanted.details.sprite;
        stoneGiven.sprite = stone.itemGiven.details.sprite;
        fireWanted.sprite = fire.itemWanted.details.sprite;
        fireGiven.sprite = fire.itemGiven.details.sprite;

        food1Wanted.sprite = foodContents[0].itemWanted.details.sprite;
        food1Given.sprite = foodContents[0].itemGiven.details.sprite;
        food2Wanted.sprite = foodContents[1].itemWanted.details.sprite;
        food2Given.sprite = foodContents[1].itemGiven.details.sprite;
        food3Wanted.sprite = foodContents[2].itemWanted.details.sprite;
        food3Given.sprite = foodContents[2].itemGiven.details.sprite;
        food4Wanted.sprite = foodContents[3].itemWanted.details.sprite;
        food4Given.sprite = foodContents[3].itemGiven.details.sprite;

        coverWanted.sprite = cover.itemWanted.details.sprite;
        coverGiven.sprite = cover.itemGiven.details.sprite;
    }

    void CreateInPit(Contents thing, int index)
    {
        GameObject pitObjectClone = Instantiate(pitObject, objectPos[index].position, Quaternion.identity);
    }
}
