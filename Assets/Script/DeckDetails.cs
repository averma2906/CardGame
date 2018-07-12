using UnityEngine;
using System.Collections;
using System.Collections.Generic;

 


public class DeckDetails :MonoBehaviour {
    public GameObject myTablePanel;
	// Use this for initialization
	void Start () {
	
	}

	public int _PresentCards;
	public List<string> cards;
	
	public int PresentCards{
		get { return _PresentCards; }
		set { _PresentCards = value; }
	}


}
