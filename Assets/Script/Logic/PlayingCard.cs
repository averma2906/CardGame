using System;
using UnityEngine;

[Serializable]
public class PlayingCard
{
    public CardSuit Suit;
    public CardNominalValue NominalValue;
	public int Index;
	public static PlayingCard Parse(string card)
    {
       // print("Card " + card);
	    //Index = index;
		//card = ((Game.CardID)index).ToString();
		card = card.Remove (0, 5);
		//Debug.Log ("Card " + card);
		var suit =card.Substring(1);
		//Debug.Log ("Suit " + suit);
		var rank =  card.Substring(0, 1);
		//Debug.Log ("rank " + rank);
        CardSuit cardSuit = new CardSuit();
        switch (suit)
        {
            case "C":
                cardSuit = CardSuit.Clubs;
                break;
            case "D":
                cardSuit = CardSuit.Diamonds;
                break;
            case "H":
                cardSuit = CardSuit.Hearts;
                break;
            case "S":
                cardSuit = CardSuit.Spades;
                break;
        }

        CardNominalValue cardNominalValue = new CardNominalValue();
        switch (rank)
        {
            case "A":
                cardNominalValue = CardNominalValue.Ace;
                break;
            case "2":
                cardNominalValue = CardNominalValue.Two;
                break;
            case "3":
                cardNominalValue = CardNominalValue.Three;
                break;
            case "4":
                cardNominalValue = CardNominalValue.Four;
                break;
            case "5":
                cardNominalValue = CardNominalValue.Five;
                break;
            case "6":
                cardNominalValue = CardNominalValue.Six;
                break;
            case "7":
                cardNominalValue = CardNominalValue.Seven;
                break;
            case "8":
                cardNominalValue = CardNominalValue.Eight;
                break;
            case "9":
                cardNominalValue = CardNominalValue.Nine;
                break;
            case "1":
                cardNominalValue = CardNominalValue.Ten;
                break;
            case "J":
                cardNominalValue = CardNominalValue.Jack;
                break;
            case "Q":
                cardNominalValue = CardNominalValue.Queen;
                break;
            case "K":
                cardNominalValue = CardNominalValue.King;
                break;



        }
        return new PlayingCard() {Suit = cardSuit, NominalValue = cardNominalValue};
    }
    public static PlayingCard ParseEvaluator(string card)
    {
        var rank = card.Substring(0, 1);
        var suit = card.Substring(1, 1);

        CardSuit cardSuit = new CardSuit();
        switch (suit)
        {
            case "C":
                cardSuit = CardSuit.Clubs;
                break;
            case "D":
                cardSuit = CardSuit.Diamonds;
                break;
            case "H":
                cardSuit = CardSuit.Hearts;
                break;
            case "S":
                cardSuit = CardSuit.Spades;
                break;
        }

        CardNominalValue cardNominalValue = new CardNominalValue();
        switch (rank)
        {
            case "A":
                cardNominalValue = CardNominalValue.Ace;
                break;
            case "2":
                cardNominalValue = CardNominalValue.Two;
                break;
            case "3":
                cardNominalValue = CardNominalValue.Three;
                break;
            case "4":
                cardNominalValue = CardNominalValue.Four;
                break;
            case "5":
                cardNominalValue = CardNominalValue.Five;
                break;
            case "6":
                cardNominalValue = CardNominalValue.Six;
                break;
            case "7":
                cardNominalValue = CardNominalValue.Seven;
                break;
            case "8":
                cardNominalValue = CardNominalValue.Eight;
                break;
            case "9":
                cardNominalValue = CardNominalValue.Nine;
                break;
            case "1":
                cardNominalValue = CardNominalValue.Ten;
                break;
            case "J":
                cardNominalValue = CardNominalValue.Jack;
                break;
            case "Q":
                cardNominalValue = CardNominalValue.Queen;
                break;
            case "K":
                cardNominalValue = CardNominalValue.King;
                break;
        }
        return new PlayingCard() {Suit = cardSuit, NominalValue = cardNominalValue};
    }

    public static bool Compare(PlayingCard thisCard , PlayingCard otherCard)
    {
        return (((int) thisCard.Suit == (int) otherCard.Suit) &&
                ((int) thisCard.NominalValue == (int) otherCard.NominalValue));
    }
}
