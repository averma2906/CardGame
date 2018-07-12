using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UICardRepository:Helpers.Singleton<UICardRepository> 
{


    public Texture2D[] Cards;

    public Texture2D GetCardTexture2D(PlayingCard card)
    {
        if (card == null) return null;
        var cardIndex = (int) (card.NominalValue) + (int) card.Suit*13;
		Debug.Log ("CardIndex " + cardIndex);
		return Cards[cardIndex];
    }
}