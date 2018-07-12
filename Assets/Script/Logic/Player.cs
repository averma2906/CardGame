using System;
using UnityEngine;

public class Player: Helpers.Singleton<Player>
{
    public string PlayerName { get; private set; }


    public Player(string playerName)
    {
        PlayerName = playerName;
    }
  
   /* public PlayerAction Action
    {
        get { return PlayerBehavior.CurrentPlayerAction; }
        set { PlayerBehavior.CurrentPlayerAction = value; }
    }*/

    public void IsActive(bool active)
    {
        
    }



    private PlayingCard[] _playingCard1;

    public PlayingCard[] PlayingCard1
    {
        get { return _playingCard1; }
    }

     

    public void SetPlayingCards(PlayingCard[] playingCard1 )
    {
        _playingCard1 = playingCard1;


        //PlayerBehavior.SetPlayerCards(playingCard1, playingCard2);
    }



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
        IsAllIn,
        Folded
    }
}