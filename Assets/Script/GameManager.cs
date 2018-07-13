using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public GamePlaySceneController gamePlaySceneController;
    int currentTurnIndex = 0;
    public static GameManager instance;
    List<CardScript> playedCards = new List<CardScript>();
    // Use this for initialization
    int turnCount = 0; 
    void Start () {
        instance = this;

	}
	
	public void StartRound()
    {
        playedCards = new List<CardScript>();
        turnCount = Game.playerCount+1;
    }

    public void AddCardToPlayedCards(GameObject go)
    {
        playedCards.Add(go.GetComponent<CardScript>());
    }

    public void CompareCards()
    {
        CardScript winnerCard = playedCards[0];
        bool isWardNeedToContinue = true;
        for (int i = 0; i < playedCards.Count-1;i++)
        {
            //add enum for this
            Debug.Log(winnerCard.cardName+" Card : " + playedCards[i + 1].cardName);
            int value = PlayingCard.Compare(winnerCard.myCard, playedCards[i + 1].myCard);

            if(value == 0 )
            {
                isWardNeedToContinue = false;
                winnerCard = playedCards[i + 1];
            }
            else if(value ==2)
            {
                
                isWardNeedToContinue = true;
            }
            Debug.Log(isWardNeedToContinue+ " Winner Card " + winnerCard.cardName);
        }

        if(!isWardNeedToContinue)
        {
            //remove them from the table and add to the winner

            for (int i = 0; i<playedCards.Count;i++)
            {
                if( playedCards[i].Owner.playerID != winnerCard.Owner.playerID)
                {
                    playedCards[i].Owner.GetComponent<DeckDetails>().cards.Remove(playedCards[i].gameObject);
                    playedCards[i].Owner = winnerCard.Owner;
                 }
                    playedCards[i].GetComponent<DragHandler>().isDragable = false;
                    playedCards[i].Owner.GetComponent<DeckDetails>().cards.Add(playedCards[i].gameObject);
                    playedCards[i].HideCard();
                playedCards[i].GetComponent<RectTransform>().SetParent(winnerCard.Owner.gameObject.GetComponent<DeckDetails>().layoutObject.transform,false);


            }
        }
        StartRound();
        NextTurn();
    }
    public void NextTurn()
    {
        turnCount--;
        Debug.Log("Compare is called "+turnCount);
        if(turnCount==0)
        {
            
            //evaluate the round and compair card 
            CompareCards();
            //return;
        }
        else {

            Debug.Log("next Turn Called " + currentTurnIndex);
            for (int i = 0; i < Game.playerCount; i++)
            {
                CanvasGroup canvasGroup = gamePlaySceneController.decks[i].GetComponent<CanvasGroup>();
                if (i == currentTurnIndex)
                {

                    int count = DeckManager._instance.deckDetailsList[i].layoutObject.transform.childCount - 1;
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
            if (currentTurnIndex == Game.playerCount)
            {
                currentTurnIndex = 0;
            }
        }

    }
}
