using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

using UnityEngine.Events;

public class DeckManager : MonoBehaviour {
    
	public List <DeckDetails> deckDetailsList=new List<DeckDetails>();
	public List<GameObject> deckCards = new List<GameObject>();
    public static DeckManager _instance;
    public GameObject cardPrefab;
    int cardCounts = 52;
    public GameObject [] playerDeckMarker;
    void Awake()
	{
		_instance = this;
       // GenerateDeck();
	}
	
   public  void GenerateDeck()
    {
        for (int i = 0; i < cardCounts;i++)
        {
            GameObject go = Instantiate(cardPrefab);
            CardScript cardScript = go.GetComponent<CardScript>();
            cardScript.cardSprite = Sprite.Create(UICardRepository.Instance.Cards[i],new Rect(0,0,80,100),Vector2.zero);
            cardScript.cardName = UICardRepository.Instance.Cards[i].name;
            cardScript.myCard = PlayingCard.Parse(((Game.CardID)(i+1)).ToString());
            deckCards.Add(go);
        }
        Suffle();
        Suffle();
    }

    void Suffle()
    {
        System.Random rand = new System.Random();
      //  Random
        for (int i = 0; i < cardCounts; i++)
        {
            // Random for remaining positions.
            int r = i + rand.Next(cardCounts - i);
           // print("value of r " + r);
            //swapping the elements
            GameObject temp = deckCards[r];
            deckCards[r] = deckCards[i];
            deckCards[i] = temp;

        }
       
        for (int i = 0; i < cardCounts; i++)
        {
            print("GameObject Name : " + deckCards[i].GetComponent<CardScript>().cardName);
        }
        DistributeCard();
    }

    //Distributing the Card evenly in all selected players
    void DistributeCard()
    {
        int i = 0;
        // int cardPerPlayer = cardCounts / Game.playerCount;
        for (int j = 0; j < cardCounts;j++)
        {
            deckCards[j].GetComponent<RectTransform>().SetParent(playerDeckMarker[i].transform, false);
            deckCards[j].GetComponent<CardScript>().tablePanel = deckDetailsList[i].myTablePanel;
            i++;
            if (i == Game.playerCount)
                i = 0;
        }

        
    }

}
