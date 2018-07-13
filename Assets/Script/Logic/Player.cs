using System;
using UnityEngine;

public class Player: MonoBehaviour
{
    public string playerName;
    public int playerID;

     
  
   /* public PlayerAction Action
    {
        get { return PlayerBehavior.CurrentPlayerAction; }
        set { PlayerBehavior.CurrentPlayerAction = value; }
    }*/
     



     
     

    



    private PlayerState _currentPlayerState;

    public PlayerState CurrentPlayerState
    {
        get { return _currentPlayerState; }
        set
        {
            _currentPlayerState = value;
            if (value == PlayerState.WaitingForAction)
            {
               // PlayerBehavior.CurrentPlayerAction = PlayerAction.None;
               // PlayerBehavior.StartTimer();
            }
            if (value == PlayerState.Played)
            {
               // PlayerBehavior.CurrentPlayerAction = PlayerAction.None;
            }
        }
    }




    public enum PlayerState
    {
        WaitingForTurn,
        WaitingForAction,
        Played,
        IsAllIn
    }
}