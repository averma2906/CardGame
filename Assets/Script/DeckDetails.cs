using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 


public class DeckDetails :MonoBehaviour {
    public GameObject myTablePanel;
    public GameObject layoutObject;
	// Use this for initialization
	void Start () {
        
	}

	 
    public List<GameObject> cards=new List<GameObject>();

    public List<GameObject> Cards
    {
        get
        {
            return cards;
        }

        set
        {
            cards = value;
        }
    }
}
