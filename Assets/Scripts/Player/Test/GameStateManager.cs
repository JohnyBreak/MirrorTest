using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public enum GameState
    {
        None = 0,
        Paused = 1,
        GamePlay = 2,
    }

    public GameState CurrentGameState { get; private set; }
    //public delegate void GameStateChangeHandler(GameState newGameState);
    //public event GameStateChangeHandler GameStateChangedEvent;

    public Action<GameState> GameStateChangedEvent;

    private GameStateManager()
    {

    }

    public void SetState(GameState newState)
    {
        if (newState == CurrentGameState) return;

        CurrentGameState = newState;
        GameStateChangedEvent?.Invoke(newState);
        switch (newState)
        {
            case GameState.Paused:
                break;
            case GameState.GamePlay:
                break;
        }
    }
}