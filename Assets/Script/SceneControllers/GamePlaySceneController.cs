using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneController : SceneController {
    public GameObject[] decks;
	// Use this for initialization
	void Start () {
        for (int i = 0;i < Game.playerCount;i++)
        {
            decks[i].SetActive(true);
        }
        DeckManager._instance.GenerateDeck();
	}
	 
}
