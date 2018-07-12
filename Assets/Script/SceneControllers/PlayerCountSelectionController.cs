using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCountSelectionController : SceneController {

	// Use this for initialization
	 
    public void PlayerCount(int num)
    {
        Game.playerCount = num;
        NextButton();
    }
	 
}
