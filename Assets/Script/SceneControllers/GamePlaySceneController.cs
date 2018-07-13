using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlaySceneController : SceneController 
{
    public GameObject[] decks;
    public GameObject[] tables;
	// Use this for initialization
	void Start () {
        
        for (int i = 0;i < Game.playerCount;i++)
        {
            decks[i].SetActive(true);
            tables[i].SetActive(true);
        }
        DeckManager._instance.GenerateDeck();
        Invoke("StartWithDelay", 5.0f);
	}

    void StartWithDelay()
    {
        GameManager.instance.NextTurn();
    }
	 
}
