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

    private bool isTutorial;

    private void Awake()
    {
        Instance = this;
        LevelManager l = LevelManager.Instance;
        if (l)
        {
            isTutorial = l.IsTutorialLevel();
        }
        else
        { isTutorial = false; }
    }

    private void Start()
    {
        UpdateGameState(GameState.StartingYoungTurn);
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

        if (isTutorial)
        {

            switch (newState)
            {
                case GameState.StartingYoungTurn:
                    UpdateGameState(GameState.YoungPlayerTurn);
                    break;
                case GameState.YoungPlayerTurn:
                    break;
                case GameState.StartingSecondPart:
                    UpdateGameState(GameState.SecondPart);
                    break;
                case GameState.SecondPart:
                    break;
                case GameState.StartingThirdPart:
                    GameObject.Find("Plat_5").SetActive(false);
                    UpdateGameState(GameState.ThirdPart);
                    break;
                case GameState.ThirdPart:
                    break;
                case GameState.StartingOldTurn:
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
                    UpdateGameState(GameState.NextLevel);
                    break;
                case GameState.NextLevel:
                    LevelManager.Instance.PlayNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
        }
        else
        {
            switch (newState)
            {
                case GameState.StartingYoungTurn:
                    UpdateGameState(GameState.YoungPlayerTurn);
                    break;
                case GameState.YoungPlayerTurn:
                    break;
                case GameState.StartingOldTurn:
                    //UpdateGameState(GameState.OldPlayerTurn);
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
                    UpdateGameState(GameState.NextLevel);
                    break;
                case GameState.NextLevel:
                    LevelManager.Instance.PlayNextLevel();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
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
        else if (State == GameState.YoungPlayerTurn || State == GameState.OldPlayerTurn)
        {
            UpdateGameState(GameState.PauseMenu);
            Time.timeScale = 0f;
        }
    }

    public bool IsTutorial(){
        return isTutorial;
    }


}


public enum GameState
{
    StartingYoungTurn,
    StartingSecondPart,
    StartingThirdPart,
    StartingOldTurn,
    YoungPlayerTurn,
    SecondPart,
    ThirdPart,
    OldPlayerTurn,
    Paradox,
    PauseMenu,
    GameOverMenu,
    LevelCompleted,
    NextLevel
}
