using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameState State;
    public GameState PreviousGameState;

    public static event Action<GameState> OnGameStateChanged;
    
    

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateGameState(GameState.YoungPlayerTurn);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    /*
     * Call this function with:
     * GameManager.Instance.UpdateGameState(GameState.YoungPlayerTurn);
     */
    public void UpdateGameState(GameState newState)
    {
        Time.timeScale = 1f;
        PreviousGameState = State;
        State = newState;

        switch (newState)
        {
            case GameState.YoungPlayerTurn:
                break;
            case GameState.SwitchingPlayerTurn:
                UpdateGameState(GameState.OldPlayerTurn);
                break;
            case GameState.OldPlayerTurn:
                break;
            case GameState.Paradox:
                break;
            case GameState.PauseMenu:
                break;
            case GameState.GameOverMenu:
                break;
            case GameState.LevelCompleted:
                LevelManager.Instance.PlayNextLevel();
                UpdateGameState(GameState.NextLevel);
                break;
            case GameState.NextLevel:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        OnGameStateChanged?.Invoke(newState);
    }

    public void TriggerMenu()
    {
        if (State == GameState.PauseMenu)
        {
            UpdateGameState(PreviousGameState);
            Time.timeScale = 1f;
        }
        else if(State == GameState.YoungPlayerTurn||State==GameState.OldPlayerTurn)
        {
            UpdateGameState(GameState.PauseMenu);
            Time.timeScale = 0f;
        }
    }
    
    
}

public enum GameState
{
    YoungPlayerTurn,
    SwitchingPlayerTurn,
    OldPlayerTurn,
    Paradox,
    PauseMenu,
    GameOverMenu,
    LevelCompleted,
    NextLevel
}
