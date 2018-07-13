using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GamePlaySceneController gamePlaySceneController;
    int currentTurnIndex = 0;
    public static GameManager instance;
    // Use this for initialization
    int turnCount = 0; 
    void Start () {
        instance = this;
	}
	
	public void StartRound()
    {
        turnCount = Game.playerCount;
    }

    public void NextTurn()
    {
        turnCount--;

        if(turnCount==0)
        {
            //evaluate the round and compair card 

            return;
        }
        Debug.Log("next Turn Called "+currentTurnIndex);
        for (int i = 0; i < Game.playerCount;i++)
        {
            CanvasGroup canvasGroup = gamePlaySceneController.decks[i].GetComponent<CanvasGroup>();
            if(i == currentTurnIndex)
            {
                
                int count = DeckManager._instance.deckDetailsList[i].Cards.Count-1;
                Debug.Log("count " + count);
                DeckManager._instance.deckDetailsList[i].layoutObject.transform.GetChild(count).GetComponent<DragHandler>().isDragable = true;
                           canvasGroup.alpha = 1.0f;
                canvasGroup.interactable = true;
            }
            else 
            {
                canvasGroup.alpha = 0.3f;
                canvasGroup.interactable = false;
            }
        }

        currentTurnIndex++;
        if(currentTurnIndex == Game.playerCount)
        {
            currentTurnIndex = 0;
        }
    }
}
