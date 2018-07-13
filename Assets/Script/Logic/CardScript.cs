using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardScript : MonoBehaviour {
    
    public Sprite cardSprite;
    public string cardName;
    public int cardID;
    public PlayingCard myCard;
    Image cardImage;
    private bool isSelected= false;
    public GameObject tablePanel;
    private  Player owner;
    public bool IsSelected
    {
        get
        {
            return isSelected;
        }

    }

    public Player Owner
    {
        get
        {
            return owner;
        }

        set
        {
            owner = value;
        }
    }

    // Use this for initialization
    void Start () {
        cardImage = GetComponent<Image>();
	}


    public void CardSelected()
    {
        isSelected = true; 
    }

    public void CardDeselected()
    {
        isSelected = false;
    }



    public void ShowCard()
    {
        cardImage.sprite = cardSprite;
    }

    public void HideCard()
    {
        cardImage.sprite = null;
    }
	 

    public void MoveToCenterTable()
    {
        
    }
}
