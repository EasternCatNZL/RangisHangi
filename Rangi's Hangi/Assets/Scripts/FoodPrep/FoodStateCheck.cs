using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStateCheck : MonoBehaviour {

    [Header("Tracked objects")]
    public List<SwipeCut> skinPieceList = new List<SwipeCut>();

    [Header("Script refs")]
    public KitchenLevelHandler kitchen;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RemovePiece(SwipeCut piece)
    {
        skinPieceList.Remove(piece);

        if(skinPieceList.Count <= 0)
        {
            //do finish stuff
            kitchen.EndLevel();
        }
    }
}
