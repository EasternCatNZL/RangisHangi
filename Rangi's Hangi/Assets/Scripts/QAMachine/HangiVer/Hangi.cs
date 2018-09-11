using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hangi : MonoBehaviour {

    [Header("Round stuff")]
    public int numFoodItems = 4;

    [Header("Positioning transforms")]
    public Transform woodPos;
    public Transform stonePos;
    public Transform firePos;
    public Transform coverPos;
    public Transform[] possibleFoodPos = new Transform[0];

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

    public Contents wood;
    public Contents stone;
    public Contents fire;
    public Contents cover;
    public Contents[] foodContents = new Contents[0];

    [Header("UI Text instructions")]
    public TextMessage stackWoodMessage;
    public TextMessage addStoneMessage;
    public TextMessage lightFireMessage;
    public TextMessage placeFoodMessage;
    public TextMessage placeCoverMessage;

    [Header("UI components")]
    public Text instructionsText;
    public Image nextItemImage;

    [Header("Script refs")]
    public LevelHandler level;
    public ProblemCreator creator;
    public ProblemResolver resolver;

    [Header("Tags")]
    public string handlerTag = "Handler";

    //control vars
    private int currentFoodItem = 1;
    private Phase phase = Phase.StackWood;

	// Use this for initialization
	void Start () {
        if (!level)
        {
            level = GameObject.FindGameObjectWithTag(handlerTag).GetComponent<LevelHandler>();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //set up the level
    public void SetupLevel()
    {
        numFoodItems = level.numQuestionsCurrentRound;
        foodContents = new Contents[numFoodItems];
        creator.GenerateProblems();
    }

    public void CollectItem(AnswerObject item)
    {
        switch (phase)
        {
            case Phase.StackWood:
                wood.itemGiven = item;
                AddStonePrompt();
                break;
            case Phase.AddStone:
                stone.itemGiven = item;
                LightFirePrompt();
                break;
            case Phase.LightFire:
                fire.itemGiven = item;
                break;
            case Phase.PlaceFood:
                foodContents[currentFoodItem].itemGiven = item;
                currentFoodItem++;
                if(currentFoodItem > foodContents.Length)
                {
                    PlaceCoverPrompt();
                }
                else
                {
                    PlaceFoodPrompt();
                }
                break;
            case Phase.PlaceCover:
                cover.itemGiven = item;
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
    }

    void AddStonePrompt()
    {
        phase = Phase.AddStone;
        instructionsText.text = addStoneMessage.maori;
    }

    void LightFirePrompt()
    {
        phase = Phase.LightFire;
        instructionsText.text = lightFireMessage.maori;
    }

    void PlaceFoodPrompt()
    {
        phase = Phase.PlaceFood;
        instructionsText.text = placeFoodMessage.maori;
        nextItemImage.sprite = foodContents[currentFoodItem].itemWanted.details.sprite;
    }

    void PlaceCoverPrompt()
    {
        phase = Phase.PlaceCover;
        instructionsText.text = placeCoverMessage.maori;
    }

    void FinishRound()
    {

    }
}
